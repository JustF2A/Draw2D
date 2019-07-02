﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Draw2D.ViewModels.Style
{
    [DataContract(IsReference = true)]
    public class ShapeStyle : ViewModelBase, ICopyable
    {
        private ArgbColor _stroke;
        private ArgbColor _fill;
        private bool _isStroked;
        private bool _isFilled;
        private bool _isScaled;
        private double _strokeWidth;
        private StrokeCap _strokeCap;
        private StrokeJoin _strokeJoin;
        private double _strokeMiter;
        private TextStyle _textStyle;

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public ArgbColor Stroke
        {
            get => _stroke;
            set => Update(ref _stroke, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public ArgbColor Fill
        {
            get => _fill;
            set => Update(ref _fill, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public bool IsStroked
        {
            get => _isStroked;
            set => Update(ref _isStroked, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public bool IsFilled
        {
            get => _isFilled;
            set => Update(ref _isFilled, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public bool IsScaled
        {
            get => _isScaled;
            set => Update(ref _isScaled, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public double StrokeWidth
        {
            get => _strokeWidth;
            set => Update(ref _strokeWidth, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public StrokeCap StrokeCap
        {
            get => _strokeCap;
            set => Update(ref _strokeCap, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public StrokeJoin StrokeJoin
        {
            get => _strokeJoin;
            set => Update(ref _strokeJoin, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public double StrokeMiter
        {
            get => _strokeMiter;
            set => Update(ref _strokeMiter, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public TextStyle TextStyle
        {
            get => _textStyle;
            set => Update(ref _textStyle, value);
        }

        public ShapeStyle()
        {
        }

        public ShapeStyle(string title, ArgbColor stroke, ArgbColor fill, bool isStroked, bool isFilled, bool isScaled, double strokeWidth, TextStyle textStyle)
        {
            this.Title = title;
            this.Stroke = stroke;
            this.Fill = fill;
            this.IsStroked = isStroked;
            this.IsFilled = isFilled;
            this.IsScaled = isScaled;
            this.StrokeWidth = strokeWidth;
            this.StrokeCap = StrokeCap.Butt;
            this.StrokeJoin = StrokeJoin.Miter;
            this.StrokeMiter = 4.0;
            this.TextStyle = textStyle;
        }

        public object Copy(Dictionary<object, object> shared)
        {
            return new ShapeStyle()
            {
                Name = this.Name,
                Title = this.Title + "_copy",
                Stroke = (ArgbColor)(this.Stroke.Copy(shared)),
                Fill = (ArgbColor)(this.Fill.Copy(shared)),
                IsStroked = this.IsStroked,
                IsFilled = this.IsFilled,
                StrokeWidth = this.StrokeWidth,
                StrokeCap = this.StrokeCap,
                StrokeJoin = this.StrokeJoin,
                StrokeMiter = this.StrokeMiter,
                TextStyle = (TextStyle)(this.TextStyle.Copy(shared))
            };
        }
    }
}
