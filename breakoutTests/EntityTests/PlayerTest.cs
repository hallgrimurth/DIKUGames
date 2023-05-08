using System;
using Breakout;
using DIKUArcade.Graphics;

using DIKUArcade.GUI;
using DIKUArcade.Events;
using System.Collections.Generic;

namespace BreakoutTests
{

    [TestFixture]
    public class PlayerTest
    {
        private Player player;

       [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();
            //adding enitites
            player = new Player();      
            
        }

        // [Test]
        // public void TestMovement()
        // { 
        //     var currentPos = player.Shape.Position;
        //     var move = player.Move;
        //     var newPos =  player.Shape.Position;
        //     Assert.AreNotEqual(currentPos, newPos);
        // }

        [Test]
        public void TestPlayerMovement()
        {
            player.Move();
            Assert.That(player.Shape.Position.Y, Is.EqualTo(0.05f));
        }

        [Test]
        public void TestPlayerMovesRight() {
            player.SetMoveRight(true);
            player.Move();
            Assert.That(player.Shape.Position.X, Is.EqualTo((0.41f)));
        }


        [Test]
        public void TestPlayerMovesLeft() {
            player.SetMoveLeft(true);
            player.Move();
            Assert.That(player.Shape.Position.Y, Is.EqualTo((0.05f)));
        }
    }

}