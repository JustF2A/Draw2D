﻿using System;

namespace Draw2D.Spatial.Sat
{
    public struct MinimumTranslationVector
    {
        public readonly Vector2 Smallest;
        public readonly double Overlap;

        public MinimumTranslationVector(Vector2 smallest, double overlap)
        {
            this.Smallest = smallest;
            this.Overlap = overlap;
        }
    }
}
