﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Core2D.Shapes;

namespace Core2D.Renderer
{
    public abstract class ShapeHelper
    {
        public abstract void Draw(object dc, ShapeRenderer renderer, BaseShape shape, ISelection selected, double dx, double dy);
    }
}
