using NUnit.Framework;
using System.IO;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;

namespace Breakout.Tests {
    [TestFixture]
    public class PlayerTests {
        
        private Player player;
        
        [SetUp]
        public void Setup() {
            player = new Player();
        }
        
        [Test]
        public void SetMoveLeft() {
            player.SetMoveLeft(true);
            
            Assert.That(player.Shape.AsDynamicShape().Direction.X, Is.EqualTo(-0.02f));
            
            player.SetMoveLeft(false);
            
            Assert.That(player.Shape.AsDynamicShape().Direction.X, Is.EqualTo(0.0f));
        }
        
        [Test]
        public void SetMoveRight() {
            player.SetMoveRight(true);
            
            Assert.That(player.Shape.AsDynamicShape().Direction.X, Is.EqualTo(0.02f));
            
            player.SetMoveRight(false);
            
            Assert.That(player.Shape.AsDynamicShape().Direction.X, Is.EqualTo(0.0f));
        }
      
        [Test]
        public void UpdateDirectionLeftRight() {
            player.SetMoveLeft(true);
            player.SetMoveRight(true);
            player.UpdateDirection();
            
            Assert.That(player.Shape.AsDynamicShape().Direction.X, Is.EqualTo(0.0f));
        }

        [Test]
        public void UpdateDirectionLeftNotRight() {
            player.SetMoveLeft(true);
            player.SetMoveRight(false);
            player.UpdateDirection();
            
            Assert.That(player.Shape.AsDynamicShape().Direction.X, Is.EqualTo(-0.02f));
        }
        [Test]
        public void UpdateDirectionNotLeftRight() {
            player.SetMoveLeft(false);
            player.SetMoveRight(true);
            player.UpdateDirection();
            
            Assert.That(player.Shape.AsDynamicShape().Direction.X, Is.EqualTo(0.02f));
        }
    }
}