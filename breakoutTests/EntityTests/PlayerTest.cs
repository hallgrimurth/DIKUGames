using System;
using Breakout;
using DIKUArcade.Graphics;

using DIKUArcade.GUI;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.Collections.Generic;

namespace BreakoutTests
{

    [TestFixture]
    public class PlayerTest
    {

       [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();          
        }

        [Test]
        public void TestMovement()
        { 
            // Arrange
            var player = new Player("Assets/Images/player.png"); 
            var initPos = player.Shape.Position;     
            
            // Act
            player.Move();

            // Assert
            var newPos =  player.Shape.Position;
            Assert.That(initPos, Is.Not.EqualTo(newPos));
        }

        [Test]
        public void TestPosition()
        { 
            // Arrange
            var player = new Player("Assets/Images/player.png"); 
            player.Shape.Position.X = 2.0f;     
            
            // Act
            player.Move();

            // Assert
            var newPos =  player.Shape.Position.X;
            Assert.That(newPos, Is.EqualTo(0.8f));
        }

        [Test]
        public void TestPosition2()
        { 
            // Arrange
            var player = new Player("Assets/Images/player.png"); 
            player.Shape.Position.X = -2.0f;     
            
            // Act
            player.Move();

            // Assert
            var newPos =  player.Shape.Position.X;
            Assert.That(newPos, Is.EqualTo(0.0f));
        }

        [Test]
        public void TestRightMovement()
        { 
            // Arrange
            var player = new Player("Assets/Images/player.png"); 
            
            // Act
            player.SetMoveRight(true);

            // Assert
            Assert.That(player.Shape.Direction.X, Is.EqualTo(0.01f));
        }

        [Test]
        public void TestRightMovement2()
        { 
            // Arrange
            var player = new Player("Assets/Images/player.png"); 
            
            // Act
            player.SetMoveRight(false);

            // Assert
            Assert.That(player.Shape.Direction.X, Is.EqualTo(0.0f));
        }

        [Test]
        public void TestLeftMovement()
        { 
            // Arrange
            var player = new Player("Assets/Images/player.png"); 
            
            // Act
            player.SetMoveLeft(true);

            // Assert
            Assert.That(player.Shape.Direction.X, Is.EqualTo(-0.01f));
        }

        [Test]
        public void TestLeftMovement2()
        { 
            // Arrange
            var player = new Player("Assets/Images/player.png"); 
            
            // Act
            player.SetMoveLeft(false);

            // Assert
            Assert.That(player.Shape.Direction.X, Is.EqualTo(0.0f));
        }
    }

}