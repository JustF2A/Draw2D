﻿using System;
using System.Collections.Generic;
using Draw2D.Models;
using Draw2D.Models.Containers;
using Draw2D.Models.Shapes;
using Draw2D.Models.Style;

namespace Draw2D.Wpf.Utilities
{
    public class YamlDraw2DTagMappings
    {
        public static IDictionary<Type, string> TagMappings = new Dictionary<Type, string>
        {
            // Renderer
            { typeof(MatrixObject), "tag:yaml.org,2002:matrix" },
            // Style
            { typeof(DrawColor), "tag:yaml.org,2002:color" },
            { typeof(DrawStyle), "tag:yaml.org,2002:style" },
            // Shapes
            { typeof(PointShape), "tag:yaml.org,2002:point" },
            { typeof(LineShape), "tag:yaml.org,2002:line" },
            { typeof(CubicBezierShape), "tag:yaml.org,2002:cubic" },
            { typeof(QuadraticBezierShape), "tag:yaml.org,2002:quad" },
            { typeof(PathShape), "tag:yaml.org,2002:path" },
            { typeof(FigureShape), "tag:yaml.org,2002:figure" },
            { typeof(ScribbleShape), "tag:yaml.org,2002:scribble" },
            { typeof(RectangleShape), "tag:yaml.org,2002:rect" },
            { typeof(EllipseShape), "tag:yaml.org,2002:ellipse" },
            { typeof(GroupShape), "tag:yaml.org,2002:group" },
            // Container
            { typeof(ShapesContainer), "tag:yaml.org,2002:container" }
        };
    }
}