using System;
using System.IO;
using Breakout;

namespace BreakoutTests
{

    [TestFixture]
    public class LevelLoadingTests
    {
        // private string fileName;
        private string? path;
        private LevelManager? level;
        [SetUp]
        public void Setup()
        {  
            path = Path.Combine(Constants.MAIN_PATH, "Assets", "Levels/"); 
        }

        [TestCase("central-mass.txt")]
        [TestCase("columns.txt")]
        [TestCase("level1.txt")]
        [TestCase("level2.txt")]
        [TestCase("level3.txt")]
        public void TestMapChange(string fileName)
        {
            //Testing if txt level file alters when loading new level
            level = new LevelManager();
            string fileTextPre = File.ReadAllText(path+fileName);
            level.LoadLevel(path+fileName); 
            string fileTextPost = File.ReadAllText(path+fileName);

            Assert.That(fileTextPre, Is.EqualTo(fileTextPost));

        }

        //Testing empty file
        [Test]
        public void TestEmptyFile()
        {
            level = new LevelManager();
            Assert.DoesNotThrow(() => level.LoadLevel(path+"empty.txt"));
        }
    
        
    }
}