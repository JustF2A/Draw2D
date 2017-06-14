﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using Core2D.Renderer;
using Core2D.Shapes;
using Core2D.Style;

namespace Core2D.Shape
{
    public abstract class BaseShape : ObservableObject, IDrawable, ISelectable
    {
        private ShapeStyle _style;
        private MatrixObject _transform;

        public ShapeStyle Style
        {
            get => _style;
            set => Update(ref _style, value);
        }

        public MatrixObject Transform
        {
            get => _transform;
            set => Update(ref _transform, value);
        }

        public abstract IEnumerable<PointShape> GetPoints();

        public virtual bool Invalidate(ShapeRenderer renderer, double dx, double dy)
        {
            bool result = false;
            result |= _style?.Invalidate(renderer) ?? false;
            result |= _transform?.Invalidate(renderer) ?? false;
            return result;
        }

        public abstract void Draw(object dc, ShapeRenderer renderer, double dx, double dy, object db, object r);

        public virtual object BeginTransform(object dc, ShapeRenderer renderer)
        {
            if (Transform != null)
            {
                return renderer.PushMatrix(dc, Transform);
            }
            return null;
        }

        public virtual void EndTransform(object dc, ShapeRenderer renderer, object state)
        {
            if (Transform != null)
            {
                renderer.PopMatrix(dc, state);
            }
        }

        public abstract void Move(ISet<BaseShape> selected, double dx, double dy);

        public virtual void Select(ISet<BaseShape> selected)
        {
            if (!selected.Contains(this))
            {
                selected.Add(this);
            }
        }

        public virtual void Deselect(ISet<BaseShape> selected)
        {
            if (selected.Contains(this))
            {
                selected.Remove(this);
            }
        }
    }
}
