﻿using System;
using System.Collections.Generic;
using Draw2D.Models.Renderers;

namespace Draw2D.Models.Shapes
{
    public class LineShape : ConnectableShape
    {
        private PointShape _startPoint;
        private PointShape _point;

        public PointShape StartPoint
        {
            get { return _startPoint; }
            set
            {
                if (value != _startPoint)
                {
                    _startPoint = value;
                    Notify("StartPoint");
                }
            }
        }

        public PointShape Point
        {
            get { return _point; }
            set
            {
                if (value != _point)
                {
                    _point = value;
                    Notify("Point");
                }
            }
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

        public override void Draw(object dc, ShapeRenderer r, double dx, double dy)
        {
            base.BeginTransform(dc, r);

            r.DrawLine(dc, this, Style, dx, dy);

            _startPoint.Draw(dc, r, dx, dy); 
            _point.Draw(dc, r, dx, dy);

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
    }
}