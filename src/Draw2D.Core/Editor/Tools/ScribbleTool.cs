﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Draw2D.Core.Shapes;
using Draw2D.Spatial;
using Draw2D.Spatial.DouglasPeucker;

namespace Draw2D.Core.Editor.Tools
{
    public class ScribbleTool : ToolBase
    {
        private ScribbleShape _scribble = null;

        public enum State { Start, Points };
        public State CurrentState = State.Start;

        public override string Name { get { return "Scribble"; } }

        public ScribbleToolSettings Settings { get; set; }

        private void StartInternal(IToolContext context, double x, double y, Modifier modifier)
        {
            Filters?.Any(f => f.Process(context, ref x, ref y));

            _scribble = new ScribbleShape()
            {
                Style = context.CurrentStyle
            };
            _scribble.Points.Add(new PointShape(x, y, context.PointShape));
            context.WorkingContainer.Shapes.Add(_scribble);

            context.Capture();
            context.Invalidate();

            CurrentState = State.Points;
        }

        private void PointsInternal(IToolContext context, double x, double y, Modifier modifier)
        {
            CurrentState = State.Start;

            if (Settings?.Simplify ?? true)
            {
                List<Vector2> points = _scribble.Points.Select(p => new Vector2((float)p.X, (float)p.Y)).ToList();
                int count = _scribble.Points.Count;
                RDP rdp = new RDP();
                BitArray accepted = rdp.DouglasPeucker(points, 0, count - 1, Settings?.Epsilon ?? 1.0);
                int removed = 0;
                for (int i = 0; i <= count - 1; ++i)
                {
                    if (!accepted[i])
                    {
                        _scribble.Points.RemoveAt(i - removed);
                        ++removed;
                    }
                }
            }
            context.WorkingContainer.Shapes.Remove(_scribble);
            context.CurrentContainer.Shapes.Add(_scribble);

            _scribble = null;

            Filters?.ForEach(f => f.Clear(context));

            context.Release();
            context.Invalidate();
        }

        private void MoveStartInternal(IToolContext context, double x, double y, Modifier modifier)
        {
            Filters?.ForEach(f => f.Clear(context));
            Filters?.Any(f => f.Process(context, ref x, ref y));

            context.Invalidate();
        }

        private void MovePointsInternal(IToolContext context, double x, double y, Modifier modifier)
        {
            Filters?.ForEach(f => f.Clear(context));
            Filters?.Any(f => f.Process(context, ref x, ref y));

            _scribble.Points.Add(new PointShape(x, y, context.PointShape));

            context.Invalidate();
        }

        private void CleanInternal(IToolContext context)
        {
            CurrentState = State.Start;

            Filters?.ForEach(f => f.Clear(context));

            if (_scribble != null)
            {
                context.WorkingContainer.Shapes.Remove(_scribble);
                _scribble = null;
            }

            context.Release();
            context.Invalidate();
        }

        public override void LeftDown(IToolContext context, double x, double y, Modifier modifier)
        {
            base.LeftDown(context, x, y, modifier);

            switch (CurrentState)
            {
                case State.Start:
                    {
                        StartInternal(context, x, y, modifier);
                    }
                    break;
            }
        }

        public override void LeftUp(IToolContext context, double x, double y, Modifier modifier)
        {
            base.LeftDown(context, x, y, modifier);

            switch (CurrentState)
            {
                case State.Points:
                    {
                        PointsInternal(context, x, y, modifier);
                    }
                    break;
            }
        }

        public override void RightDown(IToolContext context, double x, double y, Modifier modifier)
        {
            base.RightDown(context, x, y, modifier);

            switch (CurrentState)
            {
                case State.Points:
                    {
                        this.Clean(context);
                    }
                    break;
            }
        }

        public override void Move(IToolContext context, double x, double y, Modifier modifier)
        {
            base.Move(context, x, y, modifier);

            switch (CurrentState)
            {
                case State.Start:
                    {
                        MoveStartInternal(context, x, y, modifier);
                    }
                    break;
                case State.Points:
                    {
                        MovePointsInternal(context, x, y, modifier);
                    }
                    break;
            }
        }

        public override void Clean(IToolContext context)
        {
            base.Clean(context);

            CleanInternal(context);
        }
    }
}
