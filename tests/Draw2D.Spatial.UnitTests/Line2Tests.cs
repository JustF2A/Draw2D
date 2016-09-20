﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Draw2D.Spatial.UnitTests
{
    [TestFixture]
    public class Line2Tests
    {
        [TestCase]
        public void Construtor_Point_Sets_All_Fields()
        {
            var target = new Line2(20, 30, 40, 50);
            Assert.AreEqual(20.0, target.A.X);
            Assert.AreEqual(30.0, target.A.Y);
            Assert.AreEqual(40.0, target.B.X);
            Assert.AreEqual(50.0, target.B.Y);
        }

        [TestCase]
        public void Construtor_Sets_All_Fields()
        {
            var target = new Line2(new Point2(20, 30), new Point2(40, 50));
            Assert.AreEqual(20.0, target.A.X);
            Assert.AreEqual(30.0, target.A.Y);
            Assert.AreEqual(40.0, target.B.X);
            Assert.AreEqual(50.0, target.B.Y);
        }

        [TestCase]
        public void FromPoints_Returns_Valid_Line()
        {
            var target1 = Line2.FromPoints(0, 0, 10, 10);
            Assert.AreEqual(0.0, target1.A.X);
            Assert.AreEqual(0.0, target1.A.Y);
            Assert.AreEqual(10.0, target1.B.X);
            Assert.AreEqual(10.0, target1.B.Y);

            var target2 = Line2.FromPoints(20, 30, 45, 55);
            Assert.AreEqual(20.0, target2.A.X);
            Assert.AreEqual(30.0, target2.A.Y);
            Assert.AreEqual(45.0, target2.B.X);
            Assert.AreEqual(55.0, target2.B.Y);

            var target3 = Line2.FromPoints(20, 20, 5, 5, 3, 2);
            Assert.AreEqual(23.0, target3.A.X);
            Assert.AreEqual(22.0, target3.A.Y);
            Assert.AreEqual(8.0, target3.B.X);
            Assert.AreEqual(7.0, target3.B.Y);
        }

        [TestCase]
        public void FromPoints_Point_Returns_Valid_Line()
        {
            var target1 = Line2.FromPoints(new Point2(0, 0), new Point2(10, 10));
            Assert.AreEqual(0.0, target1.A.X);
            Assert.AreEqual(0.0, target1.A.Y);
            Assert.AreEqual(10.0, target1.B.X);
            Assert.AreEqual(10.0, target1.B.Y);

            var target2 = Line2.FromPoints(new Point2(20, 30), new Point2(45, 55));
            Assert.AreEqual(20.0, target2.A.X);
            Assert.AreEqual(30.0, target2.A.Y);
            Assert.AreEqual(45.0, target2.B.X);
            Assert.AreEqual(55.0, target2.B.Y);

            var target3 = Line2.FromPoints(new Point2(20, 20), new Point2(5, 5), 3, 2);
            Assert.AreEqual(23.0, target3.A.X);
            Assert.AreEqual(22.0, target3.A.Y);
            Assert.AreEqual(8.0, target3.B.X);
            Assert.AreEqual(7.0, target3.B.Y);
        }


        [TestCase]
        public void AngleBetween_Calculates_Angle_In_Degrees()
        {
            var line0 = new Line2(0, 0, 10, 10);
            var line1 = new Line2(5, 0, 5, 10);
            var line2 = new Line2(10, 0, 0, 10);
            var line3 = new Line2(10, 5, 0, 5);
            var line4 = new Line2(10, 10, 0, 0);
            var line5 = new Line2(5, 10, 5, 0);
            var line6 = new Line2(0, 10, 10, 0);
            var line7 = new Line2(0, 5, 10, 5);
            Assert.AreEqual(0.0, Line2.AngleBetween(line0.A, line0.B, line0.A, line0.B));
            Assert.AreEqual(45.0, Line2.AngleBetween(line0.A, line0.B, line1.A, line1.B));
            Assert.AreEqual(90.0, Line2.AngleBetween(line0.A, line0.B, line2.A, line2.B));
            Assert.AreEqual(135.0, Line2.AngleBetween(line0.A, line0.B, line3.A, line3.B));
            Assert.AreEqual(180.0, Line2.AngleBetween(line0.A, line0.B, line4.A, line4.B));
            Assert.AreEqual(225.0, Line2.AngleBetween(line0.A, line0.B, line5.A, line5.B));
            Assert.AreEqual(270.0, Line2.AngleBetween(line0.A, line0.B, line6.A, line6.B));
            Assert.AreEqual(315.0, Line2.AngleBetween(line0.A, line0.B, line7.A, line7.B));
        }

        [TestCase]
        public void Middle_Calculates_Line_Middle_Point_In_Degrees()
        {
            var line0 = new Line2(0, 0, 10, 10);
            var line1 = new Line2(5, 0, 5, 10);
            var line2 = new Line2(10, 0, 0, 10);
            var line3 = new Line2(10, 5, 0, 5);
            var line4 = new Line2(10, 10, 0, 0);
            var line5 = new Line2(5, 10, 5, 0);
            var line6 = new Line2(0, 10, 10, 0);
            var line7 = new Line2(0, 5, 10, 5);
            var expected = new Point2(5, 5);
            Assert.AreEqual(expected, Line2.Middle(line0.A, line0.B));
            Assert.AreEqual(expected, Line2.Middle(line1.A, line1.B));
            Assert.AreEqual(expected, Line2.Middle(line2.A, line2.B));
            Assert.AreEqual(expected, Line2.Middle(line3.A, line3.B));
            Assert.AreEqual(expected, Line2.Middle(line4.A, line4.B));
            Assert.AreEqual(expected, Line2.Middle(line5.A, line5.B));
            Assert.AreEqual(expected, Line2.Middle(line6.A, line6.B));
            Assert.AreEqual(expected, Line2.Middle(line7.A, line7.B));
        }

        [TestCase]
        public void LineIntersectWithLine_Returns_False_If_Lines_Are_Parallel()
        {
            var horizontal = new Line2(0, 5, 10, 5);
            var vertical = new Line2(5, 0, 5, 10);
            var diagonal = new Line2(10, 0, 0, 10);
            Point2 clipH;
            Point2 clipV;
            Point2 clipD;
            Assert.False(Line2.LineIntersectWithLine(horizontal.A, horizontal.B, horizontal.A, horizontal.B, out clipH));
            Assert.AreEqual(default(Point2), clipH);
            Assert.False(Line2.LineIntersectWithLine(vertical.A, vertical.B, vertical.A, vertical.B, out clipV));
            Assert.AreEqual(default(Point2), clipV);
            Assert.False(Line2.LineIntersectWithLine(diagonal.A, diagonal.B, diagonal.A, diagonal.B, out clipD));
            Assert.AreEqual(default(Point2), clipD);
        }

        [TestCase]
        public void LineIntersectWithLine_Returns_True_If_Lines_Intersect()
        {
            var line0 = new Line2(0, 0, 10, 10);
            var line1 = new Line2(5, 0, 5, 10);
            var line2 = new Line2(10, 0, 0, 10);
            var line3 = new Line2(10, 5, 0, 5);
            var line5 = new Line2(5, 10, 5, 0);
            var line6 = new Line2(0, 10, 10, 0);
            var line7 = new Line2(0, 5, 10, 5);
            var expected = new Point2(5, 5);
            Point2 clip1;
            Point2 clip2;
            Point2 clip3;
            Point2 clip5;
            Point2 clip6;
            Point2 clip7;
            Assert.True(Line2.LineIntersectWithLine(line0.A, line0.B, line1.A, line1.B, out clip1));
            Assert.AreEqual(expected, clip1);
            Assert.True(Line2.LineIntersectWithLine(line0.A, line0.B, line2.A, line2.B, out clip2));
            Assert.AreEqual(expected, clip2);
            Assert.True(Line2.LineIntersectWithLine(line0.A, line0.B, line3.A, line3.B, out clip3));
            Assert.AreEqual(expected, clip3);
            Assert.True(Line2.LineIntersectWithLine(line0.A, line0.B, line5.A, line5.B, out clip5));
            Assert.AreEqual(expected, clip5);
            Assert.True(Line2.LineIntersectWithLine(line0.A, line0.B, line6.A, line6.B, out clip6));
            Assert.AreEqual(expected, clip6);
            Assert.True(Line2.LineIntersectWithLine(line0.A, line0.B, line7.A, line7.B, out clip7));
            Assert.AreEqual(expected, clip7);
        }

        [TestCase]
        public void LineIntersectsWithEllipse_Returns_False_If_Does_Not_Intersects_Segment()
        {
            var rects = new[]
            {
                new Point2(-5, 5).ExpandToRect(3),
                new Point2(15, 5).ExpandToRect(3),
                new Point2(5, -5).ExpandToRect(3),
                new Point2(5, 15).ExpandToRect(3)
            };

            var lines = new[]
            {
                new Line2(0, 0, 10, 10),
                new Line2(5, 0, 5, 10),
                new Line2(10, 0, 0, 10),
                new Line2(10, 5, 0, 5),
                new Line2(10, 10, 0, 0),
                new Line2(5, 10, 5, 0),
                new Line2(0, 10, 10, 0),
                new Line2(0, 5, 10, 5)
            };

            foreach (var rect in rects)
            {
                foreach (var line in lines)
                {
                    IList<Point2> points;
                    Assert.False(Line2.LineIntersectsWithEllipse(line.A, line.B, rect, true, out points));
                    Assert.Null(points);
                }
            }
        }

        [TestCase]
        public void LineIntersectsWithEllipse_Returns_False_If_Does_Not_Intersects_Infinite()
        {
            var rects = new[]
            {
                new Point2(-5, 5).ExpandToRect(3),
                new Point2(15, 5).ExpandToRect(3),
                new Point2(5, -5).ExpandToRect(3),
                new Point2(5, 15).ExpandToRect(3)
            };

            var lines = new[]
            {
                new Line2(0, 0, 10, 10),
                new Line2(10, 0, 0, 10),
                new Line2(10, 10, 0, 0),
                new Line2(0, 10, 10, 0)
            };

            foreach (var rect in rects)
            {
                foreach (var line in lines)
                {
                    IList<Point2> points;
                    Assert.False(Line2.LineIntersectsWithEllipse(line.A, line.B, rect, false, out points));
                    Assert.Null(points);
                }
            }
        }

        [TestCase]
        public void LineIntersectsWithEllipse_Returns_True_If_Intersects_Segment()
        {
            // TODO: Test all rectangle combinations.

            var rects = new[]
            {
                new Point2(5, 5).ExpandToRect(3),
                //new Point2(0, 0).ExpandToRect(3),
                //new Point2(10, 0).ExpandToRect(3),
                //new Point2(0, 10).ExpandToRect(3),
                //new Point2(10, 10).ExpandToRect(3)
            };

            var lines = new[]
            {
                new Line2(0, 0, 10, 10),
                new Line2(5, 0, 5, 10),
                new Line2(10, 0, 0, 10),
                new Line2(10, 5, 0, 5),
                new Line2(10, 10, 0, 0),
                new Line2(5, 10, 5, 0),
                new Line2(0, 10, 10, 0),
                new Line2(0, 5, 10, 5)
            };

            foreach (var rect in rects)
            {
                foreach (var line in lines)
                {
                    IList<Point2> points;
                    Assert.True(Line2.LineIntersectsWithEllipse(line.A, line.B, rect, true, out points));
                    Assert.NotNull(points);
                    // TODO: Validate points.
                }
            }
        }

        [TestCase]
        public void LineIntersectsWithEllipse_Returns_True_If_Intersects_Infinite()
        {
            // TODO: Test all rectangle combinations.

            var rects = new[]
            {
                //new Point2(-5, 5).ExpandToRect(3),
                //new Point2(15, 5).ExpandToRect(3),
                //new Point2(5, -5).ExpandToRect(3),
                //new Point2(5, 15).ExpandToRect(3),
                new Point2(5, 5).ExpandToRect(3),
                //new Point2(0, 0).ExpandToRect(3),
                //new Point2(10, 0).ExpandToRect(3),
                //new Point2(0, 10).ExpandToRect(3),
                //new Point2(10, 10).ExpandToRect(3)
            };

            var lines = new[]
            {
                new Line2(0, 0, 10, 10),
                new Line2(5, 0, 5, 10),
                new Line2(10, 0, 0, 10),
                new Line2(10, 5, 0, 5),
                new Line2(10, 10, 0, 0),
                new Line2(5, 10, 5, 0),
                new Line2(0, 10, 10, 0),
                new Line2(0, 5, 10, 5)
            };

            foreach (var rect in rects)
            {
                foreach (var line in lines)
                {
                    IList<Point2> points;
                    Assert.True(Line2.LineIntersectsWithEllipse(line.A, line.B, rect, false, out points));
                    Assert.NotNull(points);
                    // TODO: Validate points.
                }
            }
        }

        [TestCase]
        public void LineIntersectsWithRect_Returns_False_If_Does_Not_Intersect()
        {
            var rects = new[]
            {
                new Point2(-5, 5).ExpandToRect(3),
                new Point2(15, 5).ExpandToRect(3),
                new Point2(5, -5).ExpandToRect(3),
                new Point2(5, 15).ExpandToRect(3)
            };

            var lines = new[]
            {
                new Line2(0, 0, 10, 10),
                new Line2(5, 0, 5, 10),
                new Line2(10, 0, 0, 10),
                new Line2(10, 5, 0, 5),
                new Line2(10, 10, 0, 0),
                new Line2(5, 10, 5, 0),
                new Line2(0, 10, 10, 0),
                new Line2(0, 5, 10, 5)
            };

            foreach (var rect in rects)
            {
                foreach (var line in lines)
                {
                    double x0clip;
                    double y0clip;
                    double x1clip;
                    double y1clip;
                    Assert.False(Line2.LineIntersectsWithRect(line.A, line.B, rect, out x0clip, out y0clip, out x1clip, out y1clip));
                }
            }
        }

        [TestCase]
        public void LineIntersectsWithRect_Returns_True_If_Intersects()
        {
            // TODO: Test all rectangle combinations.

            var rects = new[]
            {
                new Point2(5, 5).ExpandToRect(3),
                //new Point2(0, 0).ExpandToRect(3),
                //new Point2(10, 0).ExpandToRect(3),
                //new Point2(0, 10).ExpandToRect(3),
                //new Point2(10, 10).ExpandToRect(3)
            };

            var lines = new[]
            {
                new Line2(0, 0, 10, 10),
                new Line2(5, 0, 5, 10),
                new Line2(10, 0, 0, 10),
                new Line2(10, 5, 0, 5),
                new Line2(10, 10, 0, 0),
                new Line2(5, 10, 5, 0),
                new Line2(0, 10, 10, 0),
                new Line2(0, 5, 10, 5)
            };

            foreach (var rect in rects)
            {
                foreach (var line in lines)
                {
                    double x0clip;
                    double y0clip;
                    double x1clip;
                    double y1clip;
                    Assert.True(Line2.LineIntersectsWithRect(line.A, line.B, rect, out x0clip, out y0clip, out x1clip, out y1clip));
                    // TODO: Validate clip points.
                }
            }
        }
    }
}
