﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;

namespace Draw2D.ViewModels
{
    public class TextObject : ViewModelBase, ICopyable
    {
        private string _value;

        public string Value
        {
            get => _value;
            set => Update(ref _value, value);
        }

        public object Copy(IDictionary<object, object> shared)
        {
            return new TextObject()
            {
                Value = this.Value
            };
        }
    }
}
