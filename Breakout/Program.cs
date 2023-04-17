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
            
            game.Run();

        }
    }
}
