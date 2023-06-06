using DIKUArcade.State;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using System;
using Breakout;
using Breakout.BreakoutStates;

namespace Breakout.Tests
{
    [TestFixture]
    public class BallTests
    {
        private Ball ball;
        private Block block;
        private Player player;
        private float errorMargin = 0.01f;

        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();

            ball = new Ball(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.03f, 0.03f)));
            block = BlockFactory.CreateBlock(0,0, "red-block.png", 'N');
        }
        [Test]
        public void TestInitialPosition() {
            // Ensure that the ball's initial position is set correctly
            Vec2F expectedPosition = new Vec2F(0.5f, 0.5f);
            Vec2F actualPosition = ball.Shape.Position;
            Assert.That(Math.Abs(actualPosition.X - expectedPosition.X), Is.LessThan(errorMargin));
            Assert.That(Math.Abs(actualPosition.Y - expectedPosition.Y), Is.LessThan(errorMargin));
        }

        [Test]
        public void TestInitialDirection() {
            // Ensure that the ball's initial direction is set correctly
            Vec2F expectedDirection = new Vec2F(0.01f, 0.01f);
            Vec2F actualDirection = ball.Shape.AsDynamicShape().Direction;
            Assert.That(Math.Abs(actualDirection.X - expectedDirection.X), Is.EqualTo(errorMargin));
            Assert.That(Math.Abs(actualDirection.Y - expectedDirection.Y), Is.EqualTo(errorMargin));
        }

        [Test]
        public void TestMove() {
            // Move the ball and check if its position is updated correctly
            ball.Move();
            Vec2F expectedPosition = new Vec2F(0.51f, 0.51f);
            Vec2F actualPosition = ball.Shape.Position;
            Assert.That(Math.Abs(actualPosition.X - expectedPosition.X), Is.LessThan(errorMargin));
            Assert.That(Math.Abs(actualPosition.Y - expectedPosition.Y), Is.LessThan(errorMargin));
        }

        [Test]
        public void TestChangeDirection() {
            // Change the ball's direction and check if it is updated correctly
            Vec2F newDirection = new Vec2F(-0.01f, 0.01f);
            ball.ChangeDirection(newDirection);
            Vec2F actualDirection = ball.Shape.AsDynamicShape().Direction;
            Assert.That(Math.Abs(actualDirection.X - newDirection.X), Is.LessThan(errorMargin));
            Assert.That(Math.Abs(actualDirection.Y - newDirection.Y), Is.LessThan(errorMargin));
        }
    }
}