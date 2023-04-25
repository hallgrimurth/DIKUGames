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
        private List<string> mapLines = new List<string>();
        private List<string> metaLines = new List<string>();
        private List<string> legendLines = new List<string>();
        private List<string> availableLevels = new List<string>();
        private Dictionary<char, string> legend;
        private Dictionary<char, string> meta;
        
        public EntityContainer<Block> blocks {get;}
        public List<string> MetaLines{
            get{ return metaLines; }
        }

        public LevelManager(){
            blocks = new EntityContainer<Block>();
            // LoadMap(filePath);

        }

        //Loading map, meta and legend from file
        public void LoadTextData(string filePath) {
            //Fill list with levels
            // var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Breakout/Assets/Levels/"));
            // foreach (string file in files) {
            //     availableLevels.Add(Path.GetFileName(file));
            // }

            try {
                // Reads all lines from the file
                lines = File.ReadAllLines(filePath);

                // Creates the map, meta and legend data from the file
                // mapLines = lines.SkipWhile(line => !line.StartsWith("Map")).Skip(1).TakeWhile(line => !line.StartsWith("Map")).ToList();
                // metaLines = lines.SkipWhile(line => !line.StartsWith("Meta")).Skip(1).TakeWhile(line => !line.StartsWith("Meta")).ToList();
                // legendLines = lines.SkipWhile(line => !line.StartsWith("Legend")).Skip(1).TakeWhile(line => !line.StartsWith("Legend")).ToList();

                mapLines = ParseSegment(lines, "Map");
                metaLines = ParseSegment(lines, "Meta");
                legendLines = ParseSegment(lines, "Legend");

                meta = GetDict(metaLines);
                legend = GetDict(legendLines);

                // Changes legend and meta lines to dictionary with the char as key and the image as value
                // legend = new Dictionary<char, string>();
                // meta = new Dictionary<string, string>();

                // legend = legendLines.Select(line => line.Split(' ')).ToDictionary(line => line[0][0], line => line[1]);
                // meta = metaLines.Select(line => line.Split(' ')).ToDictionary(line => line[0][0], line => line[1]);

                // foreach (string line in legendLines) {
                //     string[] legendLine = line.Split(' ');
                //     legend.Add(legendLine[0][0], legendLine[1]);
                // }

                // foreach (string line in metaLines) {
                //     string[] metaLine = line.Split(' ');
                //     meta.Add(metaLine[0].Remove(metaLine[0].Length-1,1), metaLine[1]);
                // }

            } catch (FileNotFoundException) {
                // If file is not found, give options of levels
                Console.WriteLine("The level could not be loaded. Please choose a level from the following list:");
                foreach (string level in availableLevels) {
                    Console.WriteLine(level);
                }
                Console.WriteLine();
            }
        }

        //Load entities into entity container
        public void LoadMapEntities() {
            try {
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
                                GetBlock(x, y, i, j);
                            }
                        }
                    }
            }
            } catch (ArgumentOutOfRangeException) {
                // If the file is empty, this exception is nedded to avoid crashing
            }
        }

        public List<string> ParseSegment(string[] lines ,string segment){
            return lines.SkipWhile(line => !line.StartsWith(segment)).Skip(1).TakeWhile(line => !line.StartsWith(segment)).ToList();
        }

        public Dictionary<char, string> GetDict(List<String> list){
            return list.Select(line => line.Split(' ')).ToDictionary(line => line[0][0], line => line[1]);
        }

        // method for instantiating new block entities and add them to the proper positions when invoked elsewhere 
        public void GetBlock(float x, float y, int i, int j) {
            Vec2F blockPos = new Vec2F(x*j, 1.0f - y*i - y);
            Vec2F blockExtent = new Vec2F(x, y);
            Image blockImage = new Image(Path.Combine("Assets", "Images", legend[mapLines[i][j]]));
            DynamicShape blockShape = new DynamicShape(blockPos, blockExtent);
            Block block = new Block(blockShape, blockImage);
            blocks.AddEntity(block);
        }
    }
}

