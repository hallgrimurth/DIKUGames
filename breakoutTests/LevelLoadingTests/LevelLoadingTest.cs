// using System;
// using System.IO;
// using Breakout;

// namespace BreakoutTests
// {

//     [TestFixture]
//     public class LevelLoadingTests
//     {
//         // private string fileName;
//         private string path;
//         private LevelManager level;
//         [SetUp]
//         public void Setup()
//         {
//             //loading levels  Environment.CurrentDirectory,       
//             path = "C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/Levels";//"Assets/Levels/";//Path.Combine("Assets/Levels/");
//             level = new LevelManager();
//         }

//         [TestCase("central-mass.txt")]
//         [TestCase("columns.txt")]
//         [TestCase("level1.txt")]
//         [TestCase("level2.txt")]
//         [TestCase("level3.txt")]
//         public void TestMapChange(string fileName)
//         {
//             //Testing if txt level file alters when loading new level
//             string fileTextPre = File.ReadAllText(path+fileName);
//             level.LoadTextData(path+fileName); 
//             string fileTextPost = File.ReadAllText(path+fileName);

//             Assert.AreEqual(fileTextPre, fileTextPost);

//         }

//         [Test]
//         public void TestMetaData()
//         {
//             //Testing if meta data can be different for each level
//             level.LoadTextData(path+"central-mass.txt");
//             var metaPre = level.MetaLines;
//             level.LoadTextData(path+"columns.txt");
//             var metaPost = level.MetaLines;

//             Assert.AreNotEqual(metaPre, metaPost);
//         }

//         //Testing empty file
//         [Test]
//         public void TestEmptyFile()
//         {
//             Assert.Throws<FileNotFoundException>(() => level.LoadTextData(path+"empty.txt"));
//         }
        
//     }
// }