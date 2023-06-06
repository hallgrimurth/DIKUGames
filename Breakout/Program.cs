using System;
using DIKUArcade.GUI;

namespace Breakout
{
    /// <summary>
    /// The main entry point for the Breakout game.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Related to the Game class
            var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
            var game = new Game(windowArgs);

            game.Run();
        }
    }
}