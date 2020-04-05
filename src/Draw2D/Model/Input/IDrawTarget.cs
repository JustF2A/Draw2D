﻿
namespace Draw2D.Input
{
    public interface IDrawTarget
    {
        IInputService InputService { get; set; }
        IZoomService ZoomService { get; set; }
        void Draw(object context, double width, double height, double dx, double dy, double zx, double zy);
    }
}
