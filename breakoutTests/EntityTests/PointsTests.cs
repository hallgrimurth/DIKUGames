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
        private Value value;
       [SetUp]
        public void Setup()
        {
            value = new Value(new Vec2F(0.69f, -0.3f), new Vec2F(0.4f, 0.4f), 1);
        }


        [Test]
        public void TestPoint1()
        {
            Assert.That(value.Score, Is.EqualTo(1));
        }

        [Test]
        public void TestPoint2()
        {
            value.Score = 2147483647;
            Assert.That(value.Score, Is.GreaterThan(0));
        }

    }

}