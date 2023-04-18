using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using DIKUArcade.Entities;
using System.Linq;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using System.Collections.Generic;
using DIKUArcade.Graphics;


namespace Breakout{ 
    class LevelManager{
        private string[] map;
        public EntityContainer<Block> blocks {get;}
        

        public LevelManager(string filePath){
            blocks = new EntityContainer<Block>();
            LoadMap(filePath);

        }

        public void LoadMap(string filePath) {
            if (!File.Exists(filePath)) {
                throw new FileNotFoundException("The specified file does not exist.");
            }



            
            // // /string filePath = @"C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/Breakout/Assets/Levels/central-mass.txt";
            string[] lines = File.ReadAllLines(filePath);

            var mapLines = lines.SkipWhile(line => !line.StartsWith("Map")).Skip(1).TakeWhile(line => !line.StartsWith("Map")).ToList();
            var metaLines = lines.SkipWhile(line => !line.StartsWith("Meta")).Skip(1).TakeWhile(line => !line.StartsWith("Meta")).ToList();
            var legendLines = lines.SkipWhile(line => !line.StartsWith("Legend")).Skip(1).TakeWhile(line => !line.StartsWith("Legend")).ToList();

            //Change legend lines to dictionary with the char as key and the image as value
            Dictionary<char, string> legend = new Dictionary<char, string>();
            Dictionary<string, string> meta = new Dictionary<string, string>();

            foreach (string line in legendLines) {
                string[] legendLine = line.Split(' ');
                legend.Add(legendLine[0][0], legendLine[1]);
            }

            foreach (string line in metaLines) {
                string[] metaLine = line.Split(' ');
                meta.Add(metaLine[0].Remove(metaLine[0].Length-1,1), metaLine[1]);
            }

            //print legend
            foreach (KeyValuePair<char, string> entry in legend) {
                Console.WriteLine(entry.Key + " " + entry.Value);
            }

            // Print the lines between the two instances of "Map"
            foreach (string line in mapLines)
            {
                //Add blocks to entity container depending on the char
                for (int i = 0; i < mapLines.Count; i++) {
                    for (int j = 0 ; j < mapLines[i].Length; j++) {
                        if (legend.ContainsKey(mapLines[i][j])) {
                            blocks.AddEntity(new Block(new DynamicShape(new Vec2F(0.1f*j - 0.1f, 0.03f*i + 0.3f), new Vec2F(0.1f, 0.03f)), new Image(Path.Combine("Assets", "Images", legend[mapLines[i][j]]))));
                        }
                    }

                }

            }

            // // Print the lines between the two instances of "Meta"
            // Console.WriteLine("\nMeta lines:");
            // foreach (string line in metaLines)
            // {
            //     Console.WriteLine(line);
            // }
            
            // // Print the lines between the two instances of "Legend"
            // Console.WriteLine("\nLegend lines:");
            // foreach (string line in legendLines)
            // {
            //     Console.WriteLine(line);
            // }






            // string[] lines = File.ReadAllLines(filePath);

            // int rows = lines.Length;
            // int cols = lines[0].Length;

            // map = new char[rows, cols];

            // for (int row = 0; row < rows; row++) {
            //     for (int col = 0; col < cols; col++) {
            //         map[row, col] = lines[row][col];
            //     }
            // }
            // foreach(string line in lines) {
            //     Console.WriteLine(line);
            // }
        }
    }
}

