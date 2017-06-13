﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using Draw2D.Containers;
using Draw2D.Renderer;

namespace Draw2D.Presenters
{
    public abstract class ShapePresenter
    {
        public IDictionary<Type, ShapeHelper> Helpers { get; set; }
        public abstract void DrawContainer(object dc, IShapeContainer container, ShapeRenderer renderer, double dx, double dy, object db, object r);
        public abstract void DrawHelpers(object dc, IShapeContainer container, ShapeRenderer renderer, double dx, double dy);
    }
}
