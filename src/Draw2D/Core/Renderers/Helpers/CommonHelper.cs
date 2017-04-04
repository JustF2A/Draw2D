﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Draw2D.Core.Shapes;
using Draw2D.Core.Style;

namespace Draw2D.Core.Renderers.Helpers
{
    public abstract class CommonHelper : ShapeHelper
    {
        private DrawColor _stroke;
        private DrawColor _fill;
        private DrawStyle _strokeStyle;
        private DrawStyle _fillStyle;
        private LineShape _line;
        private EllipseShape _ellipse;

        public CommonHelper()
        {
            _stroke = new DrawColor(255, 0, 255, 255);
            _fill = new DrawColor(255, 0, 255, 255);
            _strokeStyle = new DrawStyle(_stroke, _fill, 2.0, true, false);
            _fillStyle = new DrawStyle(_stroke, _fill, 2.0, false, true);
            _line = new LineShape(new PointShape(0, 0, null), new PointShape(0, 0, null));
            _ellipse = new EllipseShape(new PointShape(0, 0, null), new PointShape(0, 0, null));
        }

        public void DrawLine(object dc, ShapeRenderer r, PointShape a, PointShape b)
        {
            _line.Style = _strokeStyle;
            _line.StartPoint.X = a.X;
            _line.StartPoint.Y = a.Y;
            _line.Point.X = b.X;
            _line.Point.Y = b.Y;
            _line.Draw(dc, r, 0.0, 0.0);
        }

        public void DrawEllipse(object dc, ShapeRenderer r, PointShape s, double radius)
        {
            _ellipse.Style = _fillStyle;
            _ellipse.TopLeft.X = s.X - radius;
            _ellipse.TopLeft.Y = s.Y - radius;
            _ellipse.BottomRight.X = s.X + radius;
            _ellipse.BottomRight.Y = s.Y + radius;
            _ellipse.Draw(dc, r, 0.0, 0.0);
        }
    }
}