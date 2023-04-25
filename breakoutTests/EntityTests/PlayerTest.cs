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

        [Test]
        public void TestMovement()
        { 
            var currentPos = player.Shape.Position;
            var move = player.Move;
            var newPos =  player.Shape.Position;
            Assert.AreNotEqual(currentPos, newPos);
        }
    }

}