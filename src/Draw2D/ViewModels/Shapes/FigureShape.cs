﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Draw2D.ViewModels.Containers;

namespace Draw2D.ViewModels.Shapes
{
    public class FigureShape : CanvasContainer, ICopyable
    {
        private bool _isFilled;
        private bool _isClosed;

        public bool IsFilled
        {
            get => _isFilled;
            set => Update(ref _isFilled, value);
        }

        public bool IsClosed
        {
            get => _isClosed;
            set => Update(ref _isClosed, value);
        }

        public FigureShape()
            : base()
        {
        }

        public FigureShape(ObservableCollection<BaseShape> shapes)
            : base()
        {
            this.Shapes = shapes;
        }

        public FigureShape(string name)
            : this()
        {
            this.Name = name;
        }

        public FigureShape(string name, ObservableCollection<BaseShape> shapes)
            : base()
        {
            this.Name = name;
            this.Shapes = shapes;
        }

        public override bool Invalidate(IShapeRenderer renderer, double dx, double dy, double zx, double zy)
        {
            bool result = base.Invalidate(renderer, dx, dy, zx, zy);

            if (this.IsDirty || result == true)
            {
                renderer.InvalidateCache(this, Style, dx, dy, zx, zy);
                this.IsDirty = false;
                result |= true;
            }

            return result;
        }

        public override void Draw(object dc, IShapeRenderer renderer, double dx, double dy, double zx, double zy, DrawMode mode, object db, object r)
        {
            var state = base.BeginTransform(dc, renderer);

            if (Guides != null)
            {
                foreach (var guide in Guides)
                {
                    guide.Draw(dc, renderer, dx, dy, zx, zy, mode, db, r);
                }
            }

            foreach (var shape in Shapes)
            {
                shape.Draw(dc, renderer, dx, dy, zx, zy, mode, db ,r);
            }

            base.EndTransform(dc, renderer, state);
        }

        public override void Move(ISelection selection, double dx, double dy)
        {
            var points = GetPoints().Distinct();

            foreach (var point in points)
            {
                if (!selection.Selected.Contains(point))
                {
                    point.Move(selection, dx, dy);
                }
            }
        }

        public override object Copy(IDictionary<object, object> shared)
        {
            var copy = new FigureShape()
            {
                Name = this.Name,
                Style = this.Style,
                Transform = (MatrixObject)this.Transform?.Copy(shared),
                Width = this.Width,
                Height = this.Height,
                IsFilled = this.IsFilled,
                IsClosed = this.IsClosed
            };

            if (shared != null)
            {
                foreach (var guide in this.Guides)
                {
                    copy.Guides.Add((LineShape)guide.Copy(shared));
                }

                foreach (var shape in this.Shapes)
                {
                    if (shape is ICopyable copyable)
                    {
                        copy.Shapes.Add((BaseShape)copyable.Copy(shared));
                    }
                }
            }

            return copy;
        }
    }
}
