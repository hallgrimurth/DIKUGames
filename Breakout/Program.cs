using System;
using DIKUArcade.GUI;

namespace Breakout
{
    class Program
    {
        static void Main(string[] args)
        {
            // Related to the MapMananger class
<<<<<<< HEAD
            string filePath = "C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/Breakout/Assets/Levels/central-mass.txt";
=======
            string filePath = @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Breakout\Assets\Levels\central-mass.txt";
>>>>>>> d151f3871565bd275abafd06cbaae18db5cd3ec8
            LevelManager level = new LevelManager(filePath);
            // level.LoadMap(filePath);
            
            // Related to the Game class
            // var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
            // var game = new Game(windowArgs);
            // game.Run();
        }
    }
}
