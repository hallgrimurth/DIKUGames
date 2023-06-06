using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Tests {
    [TestFixture]
    public class BlockTests {
        private Block block;
        private float errorMargin = 0.0001f;

        [SetUp]
        public void Setup() {
            // Create a new Block instance for each test case
            // var image = new Image("block.png");
            block = BlockFactory.CreateBlock(1, 1, "red-block.png", 'N');
        }

        [Test]
        public void TestInitialPosition() {
            // Ensure that the block's initial position is set correctly
            Vec2F expectedPosition = new Vec2F((1 / 12.0f), (1 - (1 / 36.0f)));
            Vec2F actualPosition = block.Shape.Position;

            float errorX = Math.Abs(actualPosition.X - expectedPosition.X);
            float errorY = Math.Abs(actualPosition.Y - expectedPosition.Y);
            
            Assert.That(errorX, Is.LessThan(errorMargin));
            Assert.That(errorY, Is.LessThan(errorMargin));
        }

        [Test]
        public void TestInitialExtent() {
            // Ensure that the block's initial extent is set correctly
            Vec2F expectedExtent = new Vec2F((1 / 12.0f), (1 / 36.0f));
            Vec2F actualExtent = block.Shape.Extent;

            float errorX = Math.Abs(actualExtent.X - expectedExtent.X);
            float errorY = Math.Abs(actualExtent.Y - expectedExtent.Y);

            Assert.That(errorX, Is.LessThan(errorMargin));
            Assert.That(errorY, Is.LessThan(errorMargin));       
    
        }

        [Test]
        public void TestTryDeleteEntity_HealthZero() {
            // Set the block's health to zero and check if the entity is deleted
            // block.health = 0;
            // block.TryDeleteEntity();
            block.DecreaseHealth();
            bool isDeleted = block.IsDeleted();
            Assert.IsTrue(isDeleted);
        }

        [Test]
        public void TestTryDeleteEntity_HealthNonZero() {
            // Set the block's health to a non-zero value and check if the entity is not deleted
            // block.health = 2;
            block.IncreaseHealth();
            block.TryDeleteEntity();
            bool isDeleted = block.IsDeleted();
            Assert.IsFalse(isDeleted);
        }

        [Test]
        public void TestDecreaseHealth() {
            // Decrease the block's health and check if it is updated correctly
            // block.health = 2;
            Console.WriteLine(block.Health);
            block.IncreaseHealth();
            Console.WriteLine(block.Health);
            block.DecreaseHealth();
            Console.WriteLine(block.Health);
            int expectedHealth = 1;
            int actualHealth = block.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestDecreaseHealth_Delete() {
            // Decrease the block's health when it is already one and check if it is deleted
            // block.health = 1;
            block.DecreaseHealth();
            bool isDeleted = block.IsDeleted();
            Assert.IsTrue(isDeleted);
        }
    }

    [TestFixture]
    public class PowerUpBlockTests {
        private Block powerUpBlock;
        private float errorMargin = 0.0001f;

        [SetUp]
        public void Setup() {
            // Create a new PowerUpBlock instance for each test case
            // var image = new Image("powerupblock.png");
            powerUpBlock = BlockFactory.CreateBlock(1, 1, "red-block.png", 'P');
        }

        [Test]
        public void TestInitialPosition() {
            // Ensure that the power-up block's initial position is set correctly
            Vec2F expectedPosition = new Vec2F((1 / 12.0f), (1 - (1 / 36.0f)));
            Vec2F actualPosition = powerUpBlock.Shape.Position;

            float errorX = Math.Abs(actualPosition.X - expectedPosition.X);
            float errorY = Math.Abs(actualPosition.Y - expectedPosition.Y);

            Assert.That(errorX, Is.LessThan(errorMargin));
            Assert.That(errorY, Is.LessThan(errorMargin));
        }

        [Test]
        public void TestInitialExtent() {
            // Ensure that the power-up block's initial extent is set correctly
            Vec2F expectedExtent = new Vec2F((1 / 12.0f), (1 / 36.0f));
            Vec2F actualExtent = powerUpBlock.Shape.Extent;
            
            float errorX = Math.Abs(actualExtent.X - expectedExtent.X);
            float errorY = Math.Abs(actualExtent.Y - expectedExtent.Y);

            Assert.That(errorX, Is.LessThan(errorMargin));
            Assert.That(errorY, Is.LessThan(errorMargin));
        }

        [Test]
        public void TestTryDeleteEntity_HealthZero() {
            // Set the power-up block's health to zero and check if the entity is deleted
            powerUpBlock.DecreaseHealth();
            // powerUpBlock.TryDeleteEntity();
            bool isDeleted = powerUpBlock.IsDeleted();
            Assert.IsTrue(isDeleted);
        }

        [Test]
        public void TestTryDeleteEntity_HealthNonZero() {
            // Set the power-up block's health to a non-zero value and check if the entity is not deleted
            // powerUpBlock.health = 2;
            powerUpBlock.IncreaseHealth();
            powerUpBlock.DecreaseHealth();
            bool isDeleted = powerUpBlock.IsDeleted();
            Assert.IsFalse(isDeleted);
        }

        [Test]
        public void TestDecreaseHealth() {
            // Decrease the power-up block's health and check if it is updated correctly
            powerUpBlock.IncreaseHealth();
            Console.WriteLine(powerUpBlock.Health);
            powerUpBlock.DecreaseHealth();
            int expectedHealth = 1;
            int actualHealth = powerUpBlock.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }
    }
}
