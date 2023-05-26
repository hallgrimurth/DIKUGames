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
        private Block? Normalblock;
        private Block? indestructibleblock;

        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestHit()
        {
            //testing if block is hit
            Normalblock = new NormalBlock(new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)), new Image("Assets/Images/red-block.png")); 
            var prehealth = Normalblock.Health;
            Normalblock.DecreaseHealth();
            Assert.That(Normalblock.Health, Is.EqualTo(prehealth-1));
            
        }


        [Test]
        public void TestIndestructible(){
            //testing if indestructable block is indestructable
            indestructibleblock = new IndestructibleBlock(new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)), new Image("Assets/Images/red-block.png"));
            var prehealth = indestructibleblock.Health;
            indestructibleblock.DecreaseHealth();
            Assert.That(indestructibleblock.Health, Is.EqualTo(prehealth));
        } 
    }
}