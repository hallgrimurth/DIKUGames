using System;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.GUI;
using System.Collections.Generic;

namespace BreakoutTests
{

    [TestFixture]
    public class EntityHitTest
    {
        private Block block;
        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();
            //adding enitites
            block = new NormalBlock(new DynamicShape(new Vec2F(0.5f, 0.95f - 0.5f), new Vec2F(0.5f, 0.5f)), new Image(Path.Combine("Assets", "Images", "blue-block.png")));       
            
        }

        [Test]
        public void TestHit()
        {
            //Testing if block is hit
            block.DecreaseHealth();
            Assert.That(block.Health, Is.EqualTo(2));
        }
    }
}