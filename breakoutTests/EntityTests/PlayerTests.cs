using System;
using Breakout;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using System.Collections.Generic;

namespace BreakoutTests
{

    [TestFixture]
    public class PlayerTest
    {
        private Player? player;

       [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestPlayerMovement()
        {
            player = new Player();
            player.Move();
            Assert.That(player.Shape.Position.Y, Is.EqualTo(0.2f));
        }

        [Test]
        public void TestPlayerMovesRight() {
            player = new Player();
            player.SetMoveRight(true);
            player.Move();
            Assert.That(player.Shape.Position.X, Is.EqualTo((0.41f)));
        }


        [Test]
        public void TestPlayerMovesLeft() {
            player = new Player();
            player.SetMoveLeft(true);
            player.Move();
            Assert.That(player.Shape.Position.Y, Is.EqualTo((0.2f)));
        }
    }

}