﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Diagnostics;
using Draw2D.Core;

namespace Draw2D.Editor
{
    public abstract class ToolBase : ObservableObject
    {
        public abstract string Title { get; }

        public List<PointIntersection> Intersections { get; set; }

        public List<PointFilter> Filters { get; set; }

        public virtual void LeftDown(IToolContext context, double x, double y, Modifier modifier)
        {
            Debug.WriteLine($"[{Title}] LeftDown X={x} Y={y}, Modifier {modifier}");
        }

        public virtual void LeftUp(IToolContext context, double x, double y, Modifier modifier)
        {
            Debug.WriteLine($"[{Title}] LeftUp X={x} Y={y}, Modifier {modifier}");
        }

        public virtual void RightDown(IToolContext context, double x, double y, Modifier modifier)
        {
            Debug.WriteLine($"[{Title}] RightDown X={x} Y={y}, Modifier {modifier}");
        }

        public virtual void RightUp(IToolContext context, double x, double y, Modifier modifier)
        {
            Debug.WriteLine($"[{Title}] RightUp X={x} Y={y}, Modifier {modifier}");
        }

        public virtual void Move(IToolContext context, double x, double y, Modifier modifier)
        {
            Debug.WriteLine($"[{Title}] Move X={x} Y={y}, Modifier {modifier}");
        }

        public virtual void Clean(IToolContext context)
        {
            Debug.WriteLine($"[{Title}] Clean");
        }
    }
}
