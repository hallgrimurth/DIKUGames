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
          
            
            //Loading block
            LevelManager level = new LevelManager();
            level.LoadMap("C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/BreakoutTests/Assets/Levels/central-mass.txt");
            //write blocks in level
            level.blocks.AddEntity(BlockFactory.CreateBlock(1, 1, new Image(Path.Combine("Assets", "Images", "IndestructibleBlock.png")), 'I'));
            
            
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