﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Linq;
using Draw2D.Core;
using Draw2D.Core.Containers;
using Draw2D.Core.Shapes;

namespace Draw2D.Editor.Selection.Helpers
{
    public static class CopyHelper
    {
        public static IEnumerable<PointShape> GetPoints(IEnumerable<ShapeObject> shapes)
        {
            foreach (var shape in shapes)
            {
                foreach (var point in shape.GetPoints())
                {
                    yield return point;
                }
            }
        }

        public static IDictionary<object, object> GetPointsCopyDict(IEnumerable<ShapeObject> shapes)
        {
            var copy = new Dictionary<object, object>();

            foreach (var point in GetPoints(shapes).Distinct())
            {
                copy[point] = point.Copy(null);
            }

            return copy;
        }

        public static void Copy(IShapeContainer container, IEnumerable<ShapeObject> shapes, ISet<ShapeObject> selected)
        {
            var shared = GetPointsCopyDict(shapes);

            foreach (var shape in shapes)
            {
                if (shape is ICopyable copyable)
                {
                    var copy = (ShapeObject)copyable.Copy(shared);
                    if (copy != null && !(copy is PointShape))
                    {
                        copy.Select(selected);
                        container.Shapes.Add(copy);
                    }
                }
            }
        }
    }
}
