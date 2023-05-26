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

        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();

            var shape = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.030f, 0.030f), new Vec2F(0.01f, 0.005f));
            var image = new Image("Assets/Images/ball.png");
            ball = new Ball(shape, image);
        }


        [Test]
        public void BallOutsideBoundsY()
        {
            // Arrange
            ball.Shape.Position.Y = 0.99f;
            var expectedPosition = 0.99f;

            // Act
            ball.Move();

            // Assert
            Assert.That(ball.Shape.Position.Y, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void BallOutsideBoundsX()
        {
            // Arrange
            ball.Shape.Position.X = 0.99f;
            var expectedPosition = 0.99f;

            // Act
            ball.Move();

            // Assert
            Assert.That(ball.Shape.Position.X, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void GetDirectionX()
        {
            // Arrange
            var expectedDirection = 0.01f;
            // Act
            var direction = ball.GetDirection();

            // Assert
            Assert.That(direction.X, Is.EqualTo(expectedDirection));
        }


        [Test]
        public void GetDirectionY()
        {
            // Arrange
            var expectedDirection = 0.005f;
            // Act
            var direction = ball.GetDirection();

            // Assert
            Assert.That(direction.Y, Is.EqualTo(expectedDirection));
        }

        [Test]
        public void ChangeDirection()
        {
            // Arrange
            var newDirection = new Vec2F(-0.01f, -0.005f);

            // Act
            ball.ChangeDirection(newDirection);
            var direction = ball.GetDirection();

            // Assert
            Assert.That(direction, Is.EqualTo(newDirection));
        }

        // [Test]
        // public void ProcessEvent_MovementEventWithDoubleSize_ShouldUpdateExtent()
        // {
        //     // Arrange
        //     var movementEvent = new GameEvent {
        //         EventType = GameEventType.MovementEvent,
        //         Message = "DOUBLE_SIZE"
        //     };

        //     // Act
        //     ball.ProcessEvent(movementEvent);

        //     // Assert
        //     Assert.AreEqual(new Vec2F(0.1f, 0.1f), ball.Shape.Extent);
        // }

        // [Test]
        // public void ProcessEvent_MovementEventWithNormalSize_ShouldUpdateExtent()
        // {
        //     // Arrange
        //     var movementEvent = new GameEvent {
        //         EventType = GameEventType.MovementEvent,
        //         Message = "NORMAL_SIZE"
        //     };

        //     // Act
        //     ball.ProcessEvent(movementEvent);

        //     // Assert
        //     Assert.AreEqual(new Vec2F(0.03f, 0.03f), ball.Shape.Extent);
        // }
    }
}