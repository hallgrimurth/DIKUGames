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

    
    }
}