﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using Draw2D.Core.Shapes;
using Draw2D.Spatial;

namespace Draw2D.Core.Editor.Bounds.Shapes
{
    public class GroupHitTest : HitTestBase
    {
        public override Type TargetType { get { return typeof(GroupShape); } }
        
        public override PointShape TryToGetPoint(ShapeObject shape, Point2 target, double radius, IDictionary<Type, HitTestBase> registered)
        {
            var group = shape as GroupShape;
            if (group == null)
                throw new ArgumentNullException("shape");

            var pointHitTest = registered[typeof(PointShape)];
            
            foreach (var groupPoint in group.Points)
            {
                if (pointHitTest.TryToGetPoint(groupPoint, target, radius, registered) != null)
                {
                    return groupPoint;
                }
            }

            foreach (var groupShape in group.Shapes)
            {
                var hitTest = registered[groupShape.GetType()];
                var result = hitTest.TryToGetPoint(groupShape, target, radius, registered);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public override bool Contains(ShapeObject shape, Point2 target, double radius, IDictionary<Type, HitTestBase> registered)
        {
            var group = shape as GroupShape;
            if (group == null)
                throw new ArgumentNullException("shape");

            foreach (var groupShape in group.Shapes)
            {
                var hitTest = registered[groupShape.GetType()];
                var result = hitTest.Contains(groupShape, target, radius, registered);
                if (result == true)
                {
                    return true;
                }
            }
            return false;
        }
        
        public override bool Overlaps(ShapeObject shape, Rect2 target, double radius, IDictionary<Type, HitTestBase> registered)
        {
            var group = shape as GroupShape;
            if (group == null)
                throw new ArgumentNullException("shape");

            foreach (var groupShape in group.Shapes)
            {
                var hitTest = registered[groupShape.GetType()];
                var result = hitTest.Overlaps(groupShape, target, radius, registered);
                if (result == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
