using System;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
//using DIKUArcade.Graphics;

namespace BreakoutTests
{

    [TestFixture]
    public class EntityHitTest
    {
        private EntityContainer<Block> blocks;
        [SetUp]
        public void Setup()
        {
            //var game = new Game();
            //Window.CreateOpenGLContext();
            var blocks = new EntityContainer<Block>(); 
            //blocks.AddEntity(BlockFactory.CreateBlock(1, 1, new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "block.png")), 'N'));
            //block = new BlockFactory(); //(new DynamicShape(new Vec2F(0.5f, 0.95f - 0.5f), new Vec2F(0.5f, 0.5f)), new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "block.png"))); 
        }

        // [Test]
        // public void TestHit()
        // {
        //     //Testing if block is hit
        //     blocks.DecreaseHealth();
        //     Assert.AreEqual(block.Health, 2);
        // }
    }
}