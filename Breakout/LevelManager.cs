using System;
using System.IO;
using System.Linq;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using System.Collections.Generic;
using DIKUArcade.Graphics;


namespace Breakout{ 
    public class LevelManager{
        private string[] lines;
        private List<string> mapLines;
        private List<string> metaLines;
        private List<string> legendLines;
        // private List<string> levels;
        private Dictionary<char, string> legend;
        private Dictionary<string, string> meta;
        
        public EntityContainer<Block> blocks {get;}
        

        public LevelManager(){
            blocks = new EntityContainer<Block>();
            // LoadMap(filePath);

        }

        //Loading map, meta and legend from file
        public void LoadMap(string filePath) {
            //Fill list with levels
            // levels = new List<string>();
            // var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Breakout/Assets/Levels/"));
            // foreach (string file in files) {
            //     levels.Add(Path.GetFileName(file));
            // }
            // Check if file exists if not give options of levels
            if (!File.Exists(filePath)) {
                // Console.WriteLine("The specified file does not exist.");
                // Console.WriteLine("Please choose a level from the following list:");
                // for (int i = 0; i < levels.Count; i++) {
                //     Console.WriteLine(levels[i]);
                // }

                // return ;
                throw new FileNotFoundException("The specified file does not exist.");
            } 

            // // /string filePath = @"C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/Breakout/Assets/Levels/central-mass.txt";
            lines = File.ReadAllLines(filePath);

            // Console.WriteLine(meta["Name"]);

            mapLines = lines.SkipWhile(line => !line.StartsWith("Map")).Skip(1).TakeWhile(line => !line.StartsWith("Map")).ToList();
            metaLines = lines.SkipWhile(line => !line.StartsWith("Meta")).Skip(1).TakeWhile(line => !line.StartsWith("Meta")).ToList();
            legendLines = lines.SkipWhile(line => !line.StartsWith("Legend")).Skip(1).TakeWhile(line => !line.StartsWith("Legend")).ToList();

            // Change legend lines to dictionary with the char as key and the image as value
            legend = new Dictionary<char, string>();
            meta = new Dictionary<string, string>();

            foreach (string line in legendLines) {
                string[] legendLine = line.Split(' ');
                legend.Add(legendLine[0][0], legendLine[1]);
            }

            foreach (string line in metaLines) {
                string[] metaLine = line.Split(' ');
                meta.Add(metaLine[0].Remove(metaLine[0].Length-1,1), metaLine[1]);
            }

            
        }

            //Load entities into entity container
            public void LoadMapEntities() {
            float length = mapLines[0].Length;
            float height = mapLines.Count;

            float x = (1 / length);
            float y = (1 / height);

            // Print the lines between the two instances of "Map"
            foreach (string line in mapLines) {
                //Add blocks to entity container depending on the char
                for (int i = 0; i < mapLines.Count ; i++) {
                    for (int j = 0 ; j < mapLines[i].Length; j++) {
                        if (legend.ContainsKey(mapLines[i][j])) {
                            blocks.AddEntity(new Block(new DynamicShape(new Vec2F(x*j, 0.95f - y*i), new Vec2F(x, y)), new Image(Path.Combine("Assets", "Images", legend[mapLines[i][j]]))));
                        }
                    }

                }
            }
            }

        
    }
}

