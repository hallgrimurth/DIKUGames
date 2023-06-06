using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Tests {
    [TestFixture]
    public class BlockTests {
        private Block block;

        [SetUp]
        public void Setup() {
            // Create a new Block instance for each test case
            // var image = new Image("block.png");
            block = BlockFactory.CreateBlock(1, 1, "red-block.png", 'N');
        }

        [Test]
        public void TestInitialPosition() {
            // Ensure that the block's initial position is set correctly
            Vec2F expectedPosition = new Vec2F(0.5f, 0.5f);
            Vec2F actualPosition = block.Shape.Position;
            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [Test]
        public void TestInitialExtent() {
            // Ensure that the block's initial extent is set correctly
            Vec2F expectedExtent = new Vec2F(0.1f, 0.1f);
            Vec2F actualExtent = block.Shape.Extent;
            Assert.AreEqual(expectedExtent, actualExtent);
        }

        [Test]
        public void TestTryDeleteEntity_HealthZero() {
            // Set the block's health to zero and check if the entity is deleted
            block.health = 0;
            block.TryDeleteEntity();
            bool isDeleted = block.IsDeleted();
            Assert.IsTrue(isDeleted);
        }

        [Test]
        public void TestTryDeleteEntity_HealthNonZero() {
            // Set the block's health to a non-zero value and check if the entity is not deleted
            block.health = 2;
            block.TryDeleteEntity();
            bool isDeleted = block.IsDeleted();
            Assert.IsFalse(isDeleted);
        }

        [Test]
        public void TestDecreaseHealth() {
            // Decrease the block's health and check if it is updated correctly
            block.health = 2;
        block.DecreaseHealth();
            int expectedHealth = 1;
            int actualHealth = block.health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }
    }

    [TestFixture]
    public class PowerUpBlockTests {
        private Block powerUpBlock;

        [SetUp]
        public void Setup() {
            // Create a new PowerUpBlock instance for each test case
            // var image = new Image("powerupblock.png");
            powerUpBlock = BlockFactory.CreateBlock(1, 1, "red-block.png", 'P');
        }

        [Test]
        public void TestInitialPosition() {
            // Ensure that the power-up block's initial position is set correctly
            Vec2F expectedPosition = new Vec2F(0.5f, 0.5f);
            Vec2F actualPosition = powerUpBlock.Shape.Position;
            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [Test]
        public void TestInitialExtent() {
            // Ensure that the power-up block's initial extent is set correctly
            Vec2F expectedExtent = new Vec2F(0.1f, 0.1f);
            Vec2F actualExtent = powerUpBlock.Shape.Extent;
            Assert.AreEqual(expectedExtent, actualExtent);
        }

        [Test]
        public void TestTryDeleteEntity_HealthZero() {
            // Set the power-up block's health to zero and check if the entity is deleted
            powerUpBlock.health = 0;
            powerUpBlock.TryDeleteEntity();
            bool isDeleted = powerUpBlock.IsDeleted();
            Assert.IsTrue(isDeleted);
        }

        [Test]
        public void TestTryDeleteEntity_HealthNonZero() {
            // Set the power-up block's health to a non-zero value and check if the entity is not deleted
            powerUpBlock.health = 2;
            powerUpBlock.TryDeleteEntity();
            bool isDeleted = powerUpBlock.IsDeleted();
            Assert.IsFalse(isDeleted);
        }

        [Test]
        public void TestDecreaseHealth() {
            // Decrease the power-up block's health and check if it is updated correctly
            powerUpBlock.health = 2;
            Console.WriteLine(powerUpBlock.health);
            powerUpBlock.DecreaseHealth();
            int expectedHealth = 1;
            int actualHealth = powerUpBlock.health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }
    }
}
