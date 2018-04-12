﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using Core2D.Shapes;

namespace Core2D.Editor.Bounds.Shapes
{
    public class EllipseHitTest : BoxHitTest
    {
        public override Type TargetType { get; set; } => typeof(EllipseShape);
    }
}
