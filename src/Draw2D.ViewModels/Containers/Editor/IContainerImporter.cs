﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Draw2D.ViewModels.Tools;

namespace Draw2D.ViewModels.Containers
{
    public interface IContainerImporter
    {
        void Import(IToolContext context, string path, IContainerView containerView);
    }
}
