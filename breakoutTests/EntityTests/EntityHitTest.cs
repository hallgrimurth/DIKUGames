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
<<<<<<< HEAD
          
            
            //Loading block
            LevelManager level = new LevelManager();
            level.LoadMap("C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/BreakoutTests/Assets/Levels/central-mass.txt");
            //write blocks in level
            level.blocks.AddEntity(BlockFactory.CreateBlock(1, 1, new Image(Path.Combine("Assets", "Images", "IndestructibleBlock.png")), 'I'));
            
            
=======
            //var game = new Game();
            //Window.CreateOpenGLContext();
            var blocks = new EntityContainer<Block>(); 
            //blocks.AddEntity(BlockFactory.CreateBlock(1, 1, new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "block.png")), 'N'));
            //block = new BlockFactory(); //(new DynamicShape(new Vec2F(0.5f, 0.95f - 0.5f), new Vec2F(0.5f, 0.5f)), new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "block.png"))); 
>>>>>>> 2bed3188cfc16d0c173682e2e789637f79856d1f
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