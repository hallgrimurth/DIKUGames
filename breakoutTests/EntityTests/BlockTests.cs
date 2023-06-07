using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade;
using System.Collections.Generic;
using System;

namespace Breakout.Tests {
    [TestFixture]
    public class BlockTests {
        private Block block;
        // private List<Block> blocks;
        private float errorMargin = 0.0001f;

        [SetUp]
        public void Setup() {
            // Create a new Block instance for each test case
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
        public void TestHealth() {
            // Ensure that the block's health is set correctly
            int expectedHealth = 1;
            int actualHealth = block.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestValue() {
            // Ensure that the block's value is set correctly
            int expectedValue = 10;
            int actualValue = block.Value;
            Assert.AreEqual(expectedValue, actualValue);
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
            // The block's health is a non-zero value. check if the entity is not deleted
            block.TryDeleteEntity();
            bool isDeleted = block.IsDeleted();
            Assert.IsFalse(isDeleted);
        }

        [Test]
        public void TestIncreaseHealth() {
            // Increase the block's health and check if it is updated correctly
            block.IncreaseHealth();
            int expectedHealth = 2;
            int actualHealth = block.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestDecreaseHealth() {
            // Increase and then decrease the block's health and check if it is updated correctly
            block.IncreaseHealth();
            block.DecreaseHealth();
            int expectedHealth = 1;
            int actualHealth = block.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestDecreaseHealth_Delete() {
            // Decrease the block's health when it is already one and check if it is deleted
            block.DecreaseHealth();
            bool isDeleted = block.IsDeleted();
            Assert.IsTrue(isDeleted);
        }
    }

    [TestFixture]
    public class PowerUpBlockTests {
        private Block powerUpBlock;
        // private List<Block> blocks;
        private float errorMargin = 0.0001f;

        [SetUp]
        public void Setup() {
            // Create a new PowerUpBlock instance for each test case
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
        public void TestHealth() {
            // Ensure that the power-up block's health is set correctly
            int expectedHealth = 1;
            int actualHealth = powerUpBlock.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestValue() {
            // Ensure that the power-up block's value is set correctly
            int expectedValue = 20;
            int actualValue = powerUpBlock.Value;
            Assert.AreEqual(expectedValue, actualValue);
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
            powerUpBlock.TryDeleteEntity();
            bool isDeleted = powerUpBlock.IsDeleted();
            Assert.IsFalse(isDeleted);
        }

        [Test]
        public void TestIncreaseHealth() {
            // Increase the power-up block's health and check if it is updated correctly
            powerUpBlock.IncreaseHealth();
            int expectedHealth = 2;
            int actualHealth = powerUpBlock.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestDecreaseHealth() {
            // Incrase and then decrease the power-up block's health
            // check if it is updated correctly
            powerUpBlock.IncreaseHealth();
            powerUpBlock.DecreaseHealth();
            int expectedHealth = 1;
            int actualHealth = powerUpBlock.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestDecreaseHealth_Delete() {
            // Decrease the power-up block's health when it is already one and check if it is deleted
            powerUpBlock.DecreaseHealth();
            bool isDeleted = powerUpBlock.IsDeleted();
            Assert.IsTrue(isDeleted);
        }
    }

    [TestFixture]
    public class HardenedBlockTests {
        private Block hardenedBlock;
        private float errorMargin = 0.0001f;

        [SetUp]
        public void Setup() {
            // Create a new HardenedBlock instance for each test case
            // var image = new Image("hardenedblock.png");
            hardenedBlock = BlockFactory.CreateBlock(1, 1, "red-block.png", 'H');
        }

        [Test]
        public void TestInitialPosition() {
            // Ensure that the hardened block's initial position is set correctly
            Vec2F expectedPosition = new Vec2F((1 / 12.0f), (1 - (1 / 36.0f)));
            Vec2F actualPosition = hardenedBlock.Shape.Position;

            float errorX = Math.Abs(actualPosition.X - expectedPosition.X);
            float errorY = Math.Abs(actualPosition.Y - expectedPosition.Y);

            Assert.That(errorX, Is.LessThan(errorMargin));
            Assert.That(errorY, Is.LessThan(errorMargin));
        }

        [Test]
        public void TestInitialExtent() {
            // Ensure that the hardened block's initial extent is set correctly
            Vec2F expectedExtent = new Vec2F((1 / 12.0f), (1 / 36.0f));
            Vec2F actualExtent = hardenedBlock.Shape.Extent;
            
            float errorX = Math.Abs(actualExtent.X - expectedExtent.X);
            float errorY = Math.Abs(actualExtent.Y - expectedExtent.Y);

            Assert.That(errorX, Is.LessThan(errorMargin));
            Assert.That(errorY, Is.LessThan(errorMargin));
        }

        [Test]
        public void TestHealth() {
            // Ensure that the hardened block's health is set correctly
            int expectedHealth = 2;
            int actualHealth = hardenedBlock.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestValue() {
            // Ensure that the hardened block's value is set correctly
            int expectedValue = 20;
            int actualValue = hardenedBlock.Value;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestDecreaseHealth() {
            // Decrease the hardened block's health and check if it is updated correctly
            hardenedBlock.DecreaseHealth();
            int expectedHealth = 1;
            int actualHealth = hardenedBlock.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestTryDeleteEntity_HealthZero() {
            // The hardened block has two health points
            // Decrease the hardened block's health to zero and check if the entity is deleted
            hardenedBlock.DecreaseHealth();
            hardenedBlock.DecreaseHealth();
            bool isDeleted = hardenedBlock.IsDeleted();
            Assert.IsTrue(isDeleted);
        }

        [Test]
        public void TestTryDeleteEntity_HealthNonZero() {
            // The hardened block has two health points
            // Decrease health to one and check if the entity is not deleted
            hardenedBlock.DecreaseHealth();
            bool isDeleted = hardenedBlock.IsDeleted();
            Assert.IsFalse(isDeleted);
        }
    }

    [TestFixture]
    public class HazardBlockTests {
        private Block hazardBlock;
        private float errorMargin = 0.0001f;

        [SetUp]
        public void Setup() {
            // Create a new HazardBlock instance for each test case
            hazardBlock = BlockFactory.CreateBlock(1, 1, "red-block.png", 'D');
        }

        [Test]
        public void TestInitialPosition() {
            // Ensure that the hazard block's initial position is set correctly
            Vec2F expectedPosition = new Vec2F((1 / 12.0f), (1 - (1 / 36.0f)));
            Vec2F actualPosition = hazardBlock.Shape.Position;

            float errorX = Math.Abs(actualPosition.X - expectedPosition.X);
            float errorY = Math.Abs(actualPosition.Y - expectedPosition.Y);

            Assert.That(errorX, Is.LessThan(errorMargin));
            Assert.That(errorY, Is.LessThan(errorMargin));
        }

        [Test]
        public void TestInitialExtent() {
            // Ensure that the hazard block's initial extent is set correctly
            Vec2F expectedExtent = new Vec2F((1 / 12.0f), (1 / 36.0f));
            Vec2F actualExtent = hazardBlock.Shape.Extent;
            
            float errorX = Math.Abs(actualExtent.X - expectedExtent.X);
            float errorY = Math.Abs(actualExtent.Y - expectedExtent.Y);

            Assert.That(errorX, Is.LessThan(errorMargin));
            Assert.That(errorY, Is.LessThan(errorMargin));
        }

        [Test]
        public void TestHealth() {
            // Ensure that the hazard block's health is set correctly
            int expectedHealth = 1;
            int actualHealth = hazardBlock.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestValue() {
            // Ensure that the hazard block's value is set correctly
            int expectedValue = 20;
            int actualValue = hazardBlock.Value;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestIncreaseHealth() {
            // Increase the hazard block's health and check if it is updated correctly
            hazardBlock.IncreaseHealth();
            int expectedHealth = 2;
            int actualHealth = hazardBlock.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestDecreaseHealth() {
            // Decrease the hazard block's health and check if it is updated correctly
            hazardBlock.IncreaseHealth();
            hazardBlock.DecreaseHealth();
            int expectedHealth = 1;
            int actualHealth = hazardBlock.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [Test]
        public void TestTryDeleteEntity_HealthZero() {
            // Decrease the hazard block's health to zero and check if the entity is deleted
            hazardBlock.DecreaseHealth();
            bool isDeleted = hazardBlock.IsDeleted();
            Assert.IsTrue(isDeleted);
        }

        [Test]
        public void TestTryDeleteEntity_HealthNonZero() {
            // The hazard block's health is a non-zero value
            // Check if the entity is not deleted
            hazardBlock.TryDeleteEntity();
            bool isDeleted = hazardBlock.IsDeleted();
            Assert.IsFalse(isDeleted);
        }
    }

    [TestFixture]
    public class IndestructibleBlockTests {
        private Block indestructibleBlock;
        private float errorMargin = 0.0001f;

        [SetUp]
        public void Setup() {
            // Create a new IndestructibleBlock instance for each test case
            indestructibleBlock = BlockFactory.CreateBlock(1, 1, "red-block.png", 'I');
        }

        [Test]
        public void TestInitialPosition() {
            // Ensure that the indestructible block's initial position is set correctly
            Vec2F expectedPosition = new Vec2F((1 / 12.0f), (1 - (1 / 36.0f)));
            Vec2F actualPosition = indestructibleBlock.Shape.Position;

            float errorX = Math.Abs(actualPosition.X - expectedPosition.X);
            float errorY = Math.Abs(actualPosition.Y - expectedPosition.Y);

            Assert.That(errorX, Is.LessThan(errorMargin));
            Assert.That(errorY, Is.LessThan(errorMargin));
        }

        [Test]
        public void TestInitialExtent() {
            // Ensure that the indestructible block's initial extent is set correctly
            Vec2F expectedExtent = new Vec2F((1 / 12.0f), (1 / 36.0f));
            Vec2F actualExtent = indestructibleBlock.Shape.Extent;
            
            float errorX = Math.Abs(actualExtent.X - expectedExtent.X);
            float errorY = Math.Abs(actualExtent.Y - expectedExtent.Y);

            Assert.That(errorX, Is.LessThan(errorMargin));
            Assert.That(errorY, Is.LessThan(errorMargin));
        }

        [Test]
        public void TestTryDeleteEntity() {
            // Try to delete the indestructible block and check if the entity is not deleted
            indestructibleBlock.TryDeleteEntity();
            bool isDeleted = indestructibleBlock.IsDeleted();
            Assert.IsFalse(isDeleted);
        }

        [Test]
        public void TestDecreaseHealth() {
            // Decrease the indestructible block's health and check if it is updated correctly
            // This block should not be able to decrease its health
            indestructibleBlock.DecreaseHealth();
            int expectedHealth = 1;
            int actualHealth = indestructibleBlock.Health;
            Assert.AreEqual(expectedHealth, actualHealth);
        }
    }
}
