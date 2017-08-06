﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.PanAndZoom;
using Avalonia.Input;
using Core2D.Editor;
using Core2D.ViewModels.Containers;

namespace Core2D.Avalonia.Controls
{
    public class LayerContainerInputView : Border
    {
        public LayerContainerInputView()
        {
            PointerPressed += (sender, e) => HandlePointerPressed(e);
            PointerReleased += (sender, e) => HandlePointerReleased(e);
            PointerMoved += (sender, e) => HandlePointerMoved(e);
        }

        private Modifier GetModifier(InputModifiers inputModifiers)
        {
            Modifier modifier = Modifier.None;

            if (inputModifiers.HasFlag(InputModifiers.Alt))
            {
                modifier |= Modifier.Alt;
            }

            if (inputModifiers.HasFlag(InputModifiers.Control))
            {
                modifier |= Modifier.Control;
            }

            if (inputModifiers.HasFlag(InputModifiers.Shift))
            {
                modifier |= Modifier.Shift;
            }

            return modifier;
        }

        private Point FixInvalidPointPosition(Point point)
        {
            if (this?.RenderTransform != null)
            {
                return MatrixHelper.TransformPoint(this.RenderTransform.Value.Invert(), point);
            }
            return point;
        }

        private void HandlePointerPressed(PointerPressedEventArgs e)
        {
            if (e.MouseButton == MouseButton.Left)
            {
                if (this.DataContext is LayerContainerViewModel vm)
                {
                    var point = FixInvalidPointPosition(e.GetPosition(Child));
                    vm.CurrentTool.LeftDown(vm, point.X, point.Y, GetModifier(e.InputModifiers));
                }
            }
            else if (e.MouseButton == MouseButton.Right)
            {
                if (this.DataContext is LayerContainerViewModel vm)
                {
                    var point = FixInvalidPointPosition(e.GetPosition(Child));
                    vm.CurrentTool.RightDown(vm, point.X, point.Y, GetModifier(e.InputModifiers));
                }
            }
        }

        private void HandlePointerReleased(PointerReleasedEventArgs e)
        {
            if (e.MouseButton == MouseButton.Left)
            {
                if (this.DataContext is LayerContainerViewModel vm)
                {
                    var point = FixInvalidPointPosition(e.GetPosition(Child));
                    vm.CurrentTool.LeftUp(vm, point.X, point.Y, GetModifier(e.InputModifiers));
                }
            }
            else if (e.MouseButton == MouseButton.Right)
            {
                if (this.DataContext is LayerContainerViewModel vm)
                {
                    var point = FixInvalidPointPosition(e.GetPosition(Child));
                    vm.CurrentTool.RightUp(vm, point.X, point.Y, GetModifier(e.InputModifiers));
                }
            }
        }

        private void HandlePointerMoved(PointerEventArgs e)
        {
            if (this.DataContext is LayerContainerViewModel vm)
            {
                var point = FixInvalidPointPosition(e.GetPosition(Child));
                vm.CurrentTool.Move(vm, point.X, point.Y, GetModifier(e.InputModifiers));
            }
        }
    }
}
