﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Diagnostics;
using Draw2D.Renderer;

namespace Draw2D.Shapes
{
    public class CubicBezierShape : ConnectableShape, ICopyable
    {
        private PointShape _startPoint;
        private PointShape _point1;
        private PointShape _point2;
        private PointShape _point3;

        public PointShape StartPoint
        {
            get => _startPoint;
            set => Update(ref _startPoint, value);
        }

        public PointShape Point1
        {
            get => _point1;
            set => Update(ref _point1, value);
        }

        public PointShape Point2
        {
            get => _point2;
            set => Update(ref _point2, value);
        }

        public PointShape Point3
        {
            get => _point3;
            set => Update(ref _point3, value);
        }

        public CubicBezierShape()
            : base()
        {
        }

        public CubicBezierShape(PointShape startPoint, PointShape point1, PointShape point2, PointShape point3)
            : base()
        {
            this.StartPoint = startPoint;
            this.Point1 = point1;
            this.Point2 = point2;
            this.Point3 = point3;
        }

        public override IEnumerable<PointShape> GetPoints()
        {
            yield return StartPoint;
            yield return Point1;
            yield return Point2;
            yield return Point3;
            foreach (var point in Points)
            {
                yield return point;
            }
        }

        public override bool Invalidate(IShapeRenderer renderer, double dx, double dy)
        {
            bool result = base.Invalidate(renderer, dx, dy);

            result |= _startPoint?.Invalidate(renderer, dx, dy) ?? false;
            result |= _point1?.Invalidate(renderer, dx, dy) ?? false;
            result |= _point2?.Invalidate(renderer, dx, dy) ?? false;
            result |= _point3?.Invalidate(renderer, dx, dy) ?? false;

            if (this.IsDirty || result == true)
            {
                renderer.InvalidateCache(this, Style, dx, dy);
                this.IsDirty = false;
                result |= true;
            }

            return result;
        }

        public override void Draw(object dc, IShapeRenderer renderer, double dx, double dy, object db, object r)
        {
            var state = base.BeginTransform(dc, renderer);

            if (Style != null)
            {
                renderer.DrawCubicBezier(dc, this, Style, dx, dy);
            }

            if (renderer.Selected.Contains(_startPoint))
            {
                _startPoint.Draw(dc, renderer, dx, dy, db, r);
            }

            if (renderer.Selected.Contains(_point1))
            {
                _point1.Draw(dc, renderer, dx, dy, db, r);
            }

            if (renderer.Selected.Contains(_point2))
            {
                _point2.Draw(dc, renderer, dx, dy, db, r);
            }

            if (renderer.Selected.Contains(_point3))
            {
                _point3.Draw(dc, renderer, dx, dy, db, r);
            }

            base.Draw(dc, renderer, dx, dy, db, r);
            base.EndTransform(dc, renderer, state);
        }

        public override void Move(ISelection selection, double dx, double dy)
        {
            if (!selection.Selected.Contains(_startPoint))
            {
                _startPoint.Move(selection, dx, dy);
            }

            if (!selection.Selected.Contains(_point1))
            {
                _point1.Move(selection, dx, dy);
            }

            if (!selection.Selected.Contains(_point2))
            {
                _point2.Move(selection, dx, dy);
            }

            if (!selection.Selected.Contains(_point3))
            {
                _point3.Move(selection, dx, dy);
            }

            base.Move(selection, dx, dy);
        }

        public override void Select(ISelection selection)
        {
            base.Select(selection);
            StartPoint.Select(selection);
            Point1.Select(selection);
            Point2.Select(selection);
            Point3.Select(selection);
        }

        public override void Deselect(ISelection selection)
        {
            base.Deselect(selection);
            StartPoint.Deselect(selection);
            Point1.Deselect(selection);
            Point2.Deselect(selection);
            Point3.Deselect(selection);
        }

        private bool CanConnect(PointShape point)
        {
            return StartPoint != point
                && Point1 != point
                && Point2 != point
                && Point3 != point;
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
                    Debug.WriteLine($"{nameof(CubicBezierShape)}: Connected to {nameof(StartPoint)}");
                    this.StartPoint = point;
                    return true;
                }
                else if (Point1 == target)
                {
                    Debug.WriteLine($"{nameof(CubicBezierShape)}: Connected to {nameof(Point1)}");
                    this.Point1 = point;
                    return true;
                }
                else if (Point2 == target)
                {
                    Debug.WriteLine($"{nameof(CubicBezierShape)}: Connected to {nameof(Point2)}");
                    this.Point2 = point;
                    return true;
                }
                else if (Point3 == target)
                {
                    Debug.WriteLine($"{nameof(CubicBezierShape)}: Connected to {nameof(Point3)}");
                    this.Point3 = point;
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
                Debug.WriteLine($"{nameof(CubicBezierShape)}: Disconnected from {nameof(StartPoint)}");
                result = (PointShape)point.Copy(null);
                this.StartPoint = result;
                return true;
            }
            else if (Point1 == point)
            {
                Debug.WriteLine($"{nameof(CubicBezierShape)}: Disconnected from {nameof(Point1)}");
                result = (PointShape)point.Copy(null);
                this.Point1 = result;
                return true;
            }
            else if (Point2 == point)
            {
                Debug.WriteLine($"{nameof(CubicBezierShape)}: Disconnected from {nameof(Point2)}");
                result = (PointShape)point.Copy(null);
                this.Point2 = result;
                return true;
            }
            else if (Point3 == point)
            {
                Debug.WriteLine($"{nameof(CubicBezierShape)}: Disconnected from {nameof(Point3)}");
                result = (PointShape)point.Copy(null);
                this.Point3 = result;
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
                Debug.WriteLine($"{nameof(CubicBezierShape)}: Disconnected from {nameof(StartPoint)}");
                this.StartPoint = (PointShape)this.StartPoint.Copy(null);
                result = true;
            }

            if (this.Point1 != null)
            {
                Debug.WriteLine($"{nameof(CubicBezierShape)}: Disconnected from {nameof(Point1)}");
                this.Point1 = (PointShape)this.Point1.Copy(null);
                result = true;
            }

            if (this.Point2 != null)
            {
                Debug.WriteLine($"{nameof(CubicBezierShape)}: Disconnected from {nameof(Point2)}");
                this.Point2 = (PointShape)this.Point2.Copy(null);
                result = true;
            }

            if (this.Point3 != null)
            {
                Debug.WriteLine($"{nameof(CubicBezierShape)}: Disconnected from {nameof(Point3)}");
                this.Point3 = (PointShape)this.Point3.Copy(null);
                result = true;
            }

            return result;
        }

        public object Copy(IDictionary<object, object> shared)
        {
            var copy = new CubicBezierShape()
            {
                Style = this.Style,
                Transform = (MatrixObject)this.Transform?.Copy(shared)
            };

            if (shared != null)
            {
                copy.StartPoint = (PointShape)shared[this.StartPoint];
                copy.Point1 = (PointShape)shared[this.Point1];
                copy.Point2 = (PointShape)shared[this.Point2];
                copy.Point3 = (PointShape)shared[this.Point3];

                foreach (var point in this.Points)
                {
                    copy.Points.Add((PointShape)shared[point]);
                }
            }

            return copy;
        }
    }
}
