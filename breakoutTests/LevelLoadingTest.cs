namespace BreakoutTests;
using System;
using Breakout;

public class Tests
{
    // private string fileName;
    private string path;
    private LevelManager level;
    [SetUp]
    public void Setup()
    {
        //loading levels        
        path = Path.Combine(Environment.CurrentDirectory, "Breakout", "Assets", "Levels"); // "C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/Breakout/Assets/Levels/";
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
        level.LoadMap(path+fileName); 
        string fileTextPost = File.ReadAllText(path+fileName);

        Assert.AreEqual(fileTextPre, fileTextPost);

    }

    //Testing empty file
    [Test]
    public void TestEmptyFile()
    {
        Assert.Throws<FileNotFoundException>(() => level.LoadMap(path+"empty.txt"));
    }
    
}