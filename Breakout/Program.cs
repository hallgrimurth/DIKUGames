using System;
using DIKUArcade.GUI;

namespace Breakout
{
    class Program
    {
        static void Main(string[] args)
        {

            // level.LoadMap(filePath);
            
            // Related to the Game class
            var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
            var game = new Game(windowArgs);
                        // Related to the MapMananger class
            string filePath = "C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/Breakout/Assets/Levels/central-mass.txt";
            LevelManager level = new LevelManager(filePath);
            game.Run();

        }
    }
}
