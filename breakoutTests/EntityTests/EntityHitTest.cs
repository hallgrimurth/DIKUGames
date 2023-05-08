using System;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace BreakoutTests
{

    [TestFixture]
    public class EntityHitTest
    {
        private Block block;
        [SetUp]
        public void Setup()
        {
            //Window.CreateOpenGLContext();
            var game = game;
            block = new NormalBlock(new DynamicShape(new Vec2F(0.5f, 0.95f - 0.5f), new Vec2F(0.5f, 0.5f)), new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "block.png"))); 
        }

        [Test]
        public void TestHit()
        {
            //Testing if block is hit
            block.DecreaseHealth();
            Assert.AreEqual(block.Health, 2);
        }
    }
}