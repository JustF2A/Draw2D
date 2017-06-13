﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Diagnostics;
using Draw2D.Core.Renderer;
using Draw2D.Core.Shape;

namespace Draw2D.Core.Shapes
{
    public class LineShape : ConnectableShape, ICopyable
    {
        private PointShape _startPoint;
        private PointShape _point;

        public PointShape StartPoint
        {
            get => _startPoint;
            set => Update(ref _startPoint, value);
        }

        public PointShape Point
        {
            get => _point;
            set => Update(ref _point, value);
        }

        public LineShape()
            : base()
        {
        }

        public LineShape(PointShape startPoint, PointShape point)
            : base()
        {
            this.StartPoint = startPoint;
            this.Point = point;
        }

        public override IEnumerable<PointShape> GetPoints()
        {
            yield return StartPoint;
            yield return Point;
            foreach (var point in Points)
            {
                yield return point;
            }
        }

        public override bool Invalidate(ShapeRenderer r, double dx, double dy)
        {
            bool result = base.Invalidate(r, dx, dy);

            result |= _startPoint?.Invalidate(r, dx, dy) ?? false;
            result |= _point?.Invalidate(r, dx, dy) ?? false;

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
                r.DrawLine(dc, this, Style, dx, dy);
            }

            if (r.Selected.Contains(_startPoint))
            {
                _startPoint.Draw(dc, r, dx, dy);
            }

            if (r.Selected.Contains(_point))
            {
                _point.Draw(dc, r, dx, dy);
            }

            base.Draw(dc, r, dx, dy);
            base.EndTransform(dc, r);
        }

        public override void Move(ISet<BaseShape> selected, double dx, double dy)
        {
            if (!selected.Contains(_startPoint))
            {
                _startPoint.Move(selected, dx, dy);
            }

            if (!selected.Contains(_point))
            {
                _point.Move(selected, dx, dy);
            }

            base.Move(selected, dx, dy);
        }

        public override void Select(ISet<BaseShape> selected)
        {
            base.Select(selected);
            StartPoint.Select(selected);
            Point.Select(selected);
        }

        public override void Deselect(ISet<BaseShape> selected)
        {
            base.Deselect(selected);
            StartPoint.Deselect(selected);
            Point.Deselect(selected);
        }

        private bool CanConnect(PointShape point)
        {
            return StartPoint != point
                && Point != point;
        }

        public override bool Connect(PointShape point, PointShape target)
        {
            if (base.Connect(point, target))
            {
                return true;
            }
            else if (CanConnect(point))
            {
                if (StartPoint == target)
                {
                    Debug.WriteLine($"{nameof(LineShape)}: Connected to {nameof(StartPoint)}");
                    this.StartPoint = point;
                    return true;
                }
                else if (Point == target)
                {
                    Debug.WriteLine($"{nameof(LineShape)}: Connected to {nameof(Point)}");
                    this.Point = point;
                    return true;
                }
            }
            return false;
        }

        public override bool Disconnect(PointShape point, out PointShape result)
        {
            if (base.Disconnect(point, out result))
            {
                return true;
            }
            else if (StartPoint == point)
            {
                Debug.WriteLine($"{nameof(LineShape)}: Disconnected from {nameof(StartPoint)}");
                result = (PointShape)point.Copy(null);
                this.StartPoint = result;
                return true;
            }
            else if (Point == point)
            {
                Debug.WriteLine($"{nameof(LineShape)}: Disconnected from {nameof(Point)}");
                result = (PointShape)point.Copy(null);
                this.Point = result;
                return true;
            }
            result = null;
            return false;
        }

        public override bool Disconnect()
        {
            bool result = base.Disconnect();

            if (this.StartPoint != null)
            {
                Debug.WriteLine($"{nameof(LineShape)}: Disconnected from {nameof(StartPoint)}");
                this.StartPoint = (PointShape)this.StartPoint.Copy(null);
                result = true;
            }

            if (this.Point != null)
            {
                Debug.WriteLine($"{nameof(LineShape)}: Disconnected from {nameof(Point)}");
                this.Point = (PointShape)this.Point.Copy(null);
                result = true;
            }

            return result;
        }

        public object Copy(IDictionary<object, object> shared)
        {
            var copy = new LineShape()
            {
                Style = this.Style,
                Transform = (MatrixObject)this.Transform?.Copy(shared)
            };

            if (shared != null)
            {
                copy.StartPoint = (PointShape)shared[this.StartPoint];
                copy.Point = (PointShape)shared[this.Point];

                foreach (var point in this.Points)
                {
                    copy.Points.Add((PointShape)shared[point]);
                }
            }

            return copy;
        }
    }
}
