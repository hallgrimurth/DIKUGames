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
            
            // /string filePath = @"C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/Breakout/Assets/Levels/central-mass.txt";
            string[] lines = File.ReadAllLines(filePath);

            var mapLines = lines.SkipWhile(line => !line.StartsWith("Map")).Skip(1).TakeWhile(line => !line.StartsWith("Map")).ToList();
            var metaLines = lines.SkipWhile(line => !line.StartsWith("Meta")).Skip(1).TakeWhile(line => !line.StartsWith("Meta")).ToList();
            var legendLines = lines.SkipWhile(line => !line.StartsWith("Legend")).Skip(1).TakeWhile(line => !line.StartsWith("Legend")).ToList();

            // Print the lines between the two instances of "Map"
            Console.WriteLine("Map lines:");
            foreach (string line in mapLines)
            {
                //Add blocks to entity container depending on the char
                for (int i = 0; i < mapLines.Count; i++) {
                    for (int j = 0 ; j < mapLines[i].Length; j++) {
                        if (mapLines[i][j] == '#') {
                            blocks.AddEntity(new Block(new DynamicShape(new Vec2F(0.05f*j, 0.083f*i), new Vec2F(0.2f, 0.1f)), new Image(Path.Combine("Assets", "Images", "red-block.png"))));
                        }
                    }
                    // Console.WriteLine(mapLines[i]);
                    // Console.WriteLine("eeeeeeeeeeeeeeeeeeeeeeee");
                    // if (line[i] == '#') {
                    //     blocks.AddEntity(new Block(new DynamicShape(new Vec2F(0.1f*i, 0.1f), new Vec2F(0.2f, 0.1f)), new Image(Path.Combine("Assets", "Images", "red-block.png"))));
                    // }
                }

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

