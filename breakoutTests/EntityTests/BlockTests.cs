using System;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;

namespace BreakoutTests
{

    [TestFixture]
    public class EntityHitTest
    {
        private Block Normalblock;
        private Block indestructibleblock;
        // private Block powerupblock;
        private Block hardenedblock;


        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();

            Normalblock = new NormalBlock(new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)), new Image("Assets/Images/red-block.png")); 
            indestructibleblock = new IndestructibleBlock(new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)), new Image("Assets/Images/red-block.png"));
            hardenedblock = new HardenedBlock(new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)), new Image("Assets/Images/red-block.png"), new Image("Assets/Images/red-block-damaged.png"));
          
        }

        [Test]
        public void TestHit()
        {
            //testing if block is hit
            var prehealth = Normalblock.Health;
            Normalblock.DecreaseHealth();
            Assert.That(Normalblock.Health, Is.EqualTo(prehealth-1));
            
        }

        [Test]
        public void TestDelete()
        {
            //testing if block is deleted
            Normalblock.DecreaseHealth();
            Normalblock.DeleteBlock();
            Assert.That(Normalblock.IsDeleted(), Is.EqualTo(true));
        }

        [Test]
        public void TestIndestructible(){
            //testing if indestructable block is indestructable
            var prehealth = indestructibleblock.Health;
            indestructibleblock.DecreaseHealth();
            Assert.That(indestructibleblock.Health, Is.EqualTo(prehealth));
        }

        
    }
}