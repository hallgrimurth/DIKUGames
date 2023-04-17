using System;
using DIKUArcade.GUI;

namespace Breakout
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD

            // level.LoadMap(filePath);
=======
            // Related to the MapMananger class
            string filePath = @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Breakout\Assets\Levels\central-mass.txt";
            LevelManager level = new LevelManager(filePath);
>>>>>>> 8d092adf5190627c758296c67161ff59e126d6e9
            
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
