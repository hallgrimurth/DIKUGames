using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using DIKUArcade.Entities;

namespace Breakout{ 
    class LevelManager{
        private string[] map;
        public LevelManager(string filePath){
            LoadMap(filePath);
        } 

        public void LoadMap(string filePath) {
            if (!File.Exists(filePath)) {
                throw new FileNotFoundException("The specified file does not exist.");
            }
            
            // /string filePath = @"C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/Breakout/Assets/Levels/central-mass.txt";
            string[] lines = File.ReadAllLines(filePath);

            var mapLines = lines.SkipWhile(line => !line.StartsWith("Map")).Skip(1).TakeWhile(line => !line.StartsWith("Map")).ToList();
            var metaLines = lines.SkipWhile(line => !line.StartsWith("Meta")).Skip(1).TakeWhile(line => !line.StartsWith("Meta")).ToList();
            var legendLines = lines.SkipWhile(line => !line.StartsWith("Legend")).Skip(1).TakeWhile(line => !line.StartsWith("Legend")).ToList();

            // Print the lines between the two instances of "Map"
            Console.WriteLine("Map lines:");
            foreach (string line in mapLines)
            {
                Console.WriteLine(line);
            }

            // Print the lines between the two instances of "Meta"
            Console.WriteLine("\nMeta lines:");
            foreach (string line in metaLines)
            {
                Console.WriteLine(line);
            }
            
            // Print the lines between the two instances of "Legend"
            Console.WriteLine("\nLegend lines:");
            foreach (string line in legendLines)
            {
                Console.WriteLine(line);
            }
        }
    }
}

