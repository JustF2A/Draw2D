﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Diagnostics;
using Draw2D.Core.Renderers;

namespace Draw2D.Core.Shapes
{
    public class TextShape : BoxShape, ICopyable
    {
        private TextObject _text;

        public TextObject Text
        {
            get => _text;
            set => Update(ref _text, value);
        }

        public TextShape()
            : base()
        {
        }

        public TextShape(PointShape topLeft, PointShape bottomRight)
            : base(topLeft, bottomRight)
        {
        }

        public TextShape(TextObject text, PointShape topLeft, PointShape bottomRight)
            : base(topLeft, bottomRight)
        {
            this.Text = text;
        }

        public override bool Invalidate(ShapeRenderer r, double dx, double dy)
        {
            bool result = base.Invalidate(r, dx, dy);

            if (_text?.IsDirty ?? false)
            {
                _text.IsDirty = false;
                result |= true;
            }

            if (this.IsDirty || result == true)
            {
                r.InvalidateCache(this, Style, dx, dy);
                this.IsDirty = false;
                result |= true;
            }

            return result;
        }

        public override void Draw(object dc, ShapeRenderer r, double dx, double dy)
        {
            base.BeginTransform(dc, r);

            if (Style != null)
            {
                r.DrawText(dc, this, Style, dx, dy);
            }

            if (r.Selected.Contains(_topLeft))
            {
                _topLeft.Draw(dc, r, dx, dy);
            }

            if (r.Selected.Contains(_bottomRight))
            {
                _bottomRight.Draw(dc, r, dx, dy);
            }

            base.Draw(dc, r, dx, dy);
            base.EndTransform(dc, r);
        }

        public object Copy(IDictionary<object, object> shared)
        {
            var copy = new TextShape()
            {
                Style = this.Style,
                Transform = (MatrixObject)this.Transform?.Copy(shared),
                Text = (TextObject)this.Text?.Copy(shared)
            };

            if (shared != null)
            {
                copy.TopLeft = (PointShape)shared[this.TopLeft];
                copy.BottomRight = (PointShape)shared[this.BottomRight];

                foreach (var point in this.Points)
                {
                    copy.Points.Add((PointShape)shared[point]);
                }
            }

            return copy;
        }
    }
}
