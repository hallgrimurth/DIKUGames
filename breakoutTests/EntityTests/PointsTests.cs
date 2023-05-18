using System;
using Breakout;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Math;
using System.Collections.Generic;
using Breakout.BreakoutStates;

namespace BreakoutTests
{

    [TestFixture]
    public class PointTest
    {
        private Points points;
       [SetUp]
        public void Setup()
        {
            points = new Points(new Vec2F(0.69f, -0.3f), new Vec2F(0.4f, 0.4f), 1);
        }


        [Test]
        public void TestPoint1()
        {
            Assert.That(points.PointsValue, Is.EqualTo(1));
        }

        [Test]
        public void TestPoint2()
        {
            points.PointsValue = 2147483647;
            Assert.That(points.PointsValue, Is.GreaterThan(0));
        }

    }

}