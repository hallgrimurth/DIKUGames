using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;

namespace BreakoutTests
{
    [TestFixture]
    public class LevelTests
    {
        private Level level;
        private string? path;
        private string fileName;

        [SetUp]
        public void Setup()
        {
            level = new Level("path");
            path = Path.Combine(Constants.MAIN_PATH, "Assets", "Levels/");
        }

        [TestCase("central-mass.txt")]
        [TestCase("columns.txt")]
        [TestCase("level1.txt")]
        [TestCase("level2.txt")]
        [TestCase("level3.txt")]
        public void TestMapChange(string fileName)
        {

            // Act
            level.LoadData(path + fileName);

            // Assert

            string fileTextPre = File.ReadAllText(path + fileName);
            string fileTextPost = File.ReadAllText(path + fileName);

            Assert.That(fileTextPre, Is.EqualTo(fileTextPost));
        }

        //Testing empty file
        [Test]
        public void TestEmptyFile()
        {
            Assert.DoesNotThrow(() => level.LoadTextData(path+"empty.txt"));
        }


        [Test]
        public void LoadTextData_ValidFilePath_LoadsTextData()
        {
            // Arrange
            string filePath = Path.Combine(Constants.MAIN_PATH, "Assets", "Levels/", "level1.txt");

            // Act
            level.LoadTextData(filePath);

            // Assert
            Assert.IsNotNull(level.MetaDict);
             //Assert.IsNotNull(level.legendDict);
            Assert.IsNotNull(level.MapData);
            Assert.IsNotNull(level.MetaData);
        }

 

        [Test]
        public void LoadDataValidFilePathTest()
        {
            // Arrange
            string filePath = Path.Combine(Constants.MAIN_PATH, "Assets", "Levels/", "testlevel.txt");

            // Act
            level.LoadData(filePath);
            
            // Assert
            CollectionAssert.AreEqual(new List<string> { "-000----000-", "-000-%%-000-", "-000-11-000-"}, level.MapData);
            CollectionAssert.AreEqual(new List<string> { "Name: LEVEL 1", "Hardened: %" }, level.MetaData);
        }

        [Test]
        public void LoadDataFileNotFoundTest()
        {
            // Arrange
            string nonExistentFilePath = "nonexistent.txt";

            // Act & Assert
            Assert.DoesNotThrow(() => level.LoadData(nonExistentFilePath));
        }


        [Test]
        public void LoadMapEntitiesTest() {

            // Act
            level.LoadMapEntities(level.MapData);

            // Assert
            // Assert.AreEqual();
        }

        [Test]
        public void ClearLevelPowerUpTest()
        {
            // Arrange
            // Should maybe be approached differently
            var powerups = new BigPowerUp(new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)), new Image("Assets/Images/red-block.png"));

            // Act
            level.ClearLevel();

            // Assert
            Assert.IsEmpty(level.powerups);
        }

        [Test]
        public void ClearLevelBlocksTest()
        {
            // Arrange
            // Should maybe be approached differently
            var blocks = new NormalBlock(new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)), new Image("Assets/Images/red-block.png"));

            // Act
            level.ClearLevel();

            // Assert
            Assert.IsEmpty(level.blocks);
        }
    }
}