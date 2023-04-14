using System;
using DIKUArcade.GUI;

namespace Breakout
{
    class Program
    {
        static void Main(string[] args)
        {
            // Related to the MapMananger class
            string filePath = "DIKUGames/Breakout/Assets/Levels/central-mass.txt";
            MapManager level = new MapManager(filePath);
            level.PrintMap();
            
            // Related to the Game class
            var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
            var game = new Game(windowArgs);
            game.Run();
        }
    }
}
