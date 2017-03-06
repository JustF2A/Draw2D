﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Draw2D.Models.Renderers;

namespace Draw2D.Models.Shapes
{
    public abstract class ConnectableShape : BaseShape
    {
        private ObservableCollection<PointShape> _points;

        public ObservableCollection<PointShape> Points
        {
            get { return _points; }
            set
            {
                if (value != _points)
                {
                    _points = value;
                    Notify("Points");
                }
            }
        }

        public ConnectableShape()
        {
            _points = new ObservableCollection<PointShape>();
        }

        public ConnectableShape(ObservableCollection<PointShape> points)
        {
            this.Points = points;
        }

        public override void Draw(object dc, ShapeRenderer r, double dx, double dy)
        {
            foreach (var point in Points)
            {
                point.Draw(dc, r, dx, dy);
            }
        }

        public override void Move(ISet<BaseShape> selected, double dx, double dy)
        {
            foreach (var point in Points)
            {
                if (!selected.Contains(point))
                {
                    point.Move(selected, dx, dy);
                }
            }
        }

        public override void Select(ISet<BaseShape> selected)
        {
            base.Select(selected);

            foreach (var point in Points)
            {
                point.Select(selected);
            }
        }

        public override void Deselect(ISet<BaseShape> selected)
        {
            base.Deselect(selected);

            foreach (var point in Points)
            {
                point.Deselect(selected);
            }
        }
    }
}