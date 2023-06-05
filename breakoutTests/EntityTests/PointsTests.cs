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
       [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestPointValue()
        {
            var points = new Points(new Vec2F(0.69f, -0.3f), new Vec2F(0.4f, 0.4f));
            Assert.That(points.PointsValue, Is.EqualTo(0));
        }


    }

}