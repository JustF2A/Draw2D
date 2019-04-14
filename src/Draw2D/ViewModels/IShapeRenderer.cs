﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Draw2D.ViewModels.Shapes;
using Draw2D.ViewModels.Style;

namespace Draw2D.ViewModels
{
    public interface IShapeRenderer
    {
        ISelection Selection { get; set; }
        void InvalidateCache(ShapeStyle style);
        void InvalidateCache(MatrixObject matrix);
        void InvalidateCache(BaseShape shape, ShapeStyle style, double dx, double dy);
        object PushMatrix(object dc, MatrixObject matrix);
        void PopMatrix(object dc, object state);
        void DrawLine(object dc, LineShape line, ShapeStyle style, double dx, double dy);
        void DrawCubicBezier(object dc, CubicBezierShape cubicBezier, ShapeStyle style, double dx, double dy);
        void DrawQuadraticBezier(object dc, QuadraticBezierShape quadraticBezier, ShapeStyle style, double dx, double dy);
        void DrawPath(object dc, PathShape path, ShapeStyle style, double dx, double dy);
        void DrawRectangle(object dc, RectangleShape rectangle, ShapeStyle style, double dx, double dy);
        void DrawEllipse(object dc, EllipseShape ellipse, ShapeStyle style, double dx, double dy);
        void DrawText(object dc, TextShape text, ShapeStyle style, double dx, double dy);
    }
}