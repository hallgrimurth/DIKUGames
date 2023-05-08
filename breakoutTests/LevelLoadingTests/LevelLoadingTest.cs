using System;
using System.IO;
using Breakout;

namespace BreakoutTests
{

    [TestFixture]
    public class LevelLoadingTests
    {
        // private string fileName;
        private string path;
        private LevelManager level;
        [SetUp]
        public void Setup()
        {
            //loading levels   
            
            path = Path.Combine(Constants.MAIN_PATH, "Assets", "Levels/"); 

            level = new LevelManager();
        }

        [TestCase("central-mass.txt")]
        [TestCase("columns.txt")]
        [TestCase("level1.txt")]
        [TestCase("level2.txt")]
        [TestCase("level3.txt")]
        public void TestMapChange(string fileName)
        {
            //Testing if txt level file alters when loading new level
            string fileTextPre = File.ReadAllText(path+fileName);
            level.LoadTextData(path+fileName); 
            string fileTextPost = File.ReadAllText(path+fileName);

            Assert.AreEqual(fileTextPre, fileTextPost);

        }

        //Testing empty file
        [Test]
        public void TestEmptyFile()
        {
            Assert.DoesNotThrow(() => level.LoadTextData(path+"empty.txt"));
        }
        
    }
}