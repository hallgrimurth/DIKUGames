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
        private string[] textData;
        private List<string> mapData = new List<string>();
        private List<string> metaData = new List<string>();
        private List<string> legendData = new List<string>();
        private Dictionary<char, string> metaDict;
        private Dictionary<char, string> legendDict;
        
        public EntityContainer<Block> blocks {get;}
        public List<string> MetaData{
            get{ return metaData; }
        }

        public LevelManager(){
            blocks = new EntityContainer<Block>();
        }

        public void LoadMap(string filePath) {
            LoadTextData(filePath);
            LoadMapEntities(mapData);
        }

        //Loading map, meta and legend from file
        public void LoadTextData(string filePath) {
            try {
                // Reads all lines from the file
                textData = File.ReadAllLines(filePath);

                mapData = ParseSegment(textData, "Map");
                metaData = ParseSegment(textData, "Meta");
                legendData = ParseSegment(textData, "Legend");

                metaDict = GetDict(metaData);
                legendDict = GetDict(legendData);

            } catch (FileNotFoundException) {
                // If the file is not found, this catch is needed to avoid crashing
            }
        }
    



        //Load entities into entity container
        public void LoadMapEntities(List<string> mapLines) {
            float length = 12;
            float height = 24;
            // Create the map from the lines
            for (int i = 0; i < height ; i++) {
                for (int j = 0 ; j < length; j++) {
                    // Attempts to add a block to the entity container depending on the char
                    try {
                        if (legendDict.ContainsKey(mapLines[i][j])) {
                            char type = 'n' ;

                            // Check if the char is a value in the meta dictionary if so, set 
                            // type to that key
                            if (metaDict.ContainsValue(mapLines[i][j].ToString())) type = 
                                metaDict.Where(x => x.Value[0] == 
                                    mapLines[i][j]).Select(x => x.Key).FirstOrDefault();
                            // Calls the block-factory to create and add a block entity
                            Image blockImage = new Image(Path.Combine("Assets", "Images", 
                                legendDict[mapLines[i][j]]));
                            blocks.AddEntity(BlockFactory.CreateBlock(i, j, blockImage, type));
                        }

                    // // These catches is needed to avoid crashing if the file is empty or data is missing
                    // // The program will continue without placing a block
                    } catch (ArgumentOutOfRangeException) {

                    } catch (IndexOutOfRangeException) {

                    }
                }
            }
        }

        // Parse a segment of the text file
        private List<string> ParseSegment(string[] lines ,string segment){
            return lines.SkipWhile(line => !line.StartsWith(segment)).Skip(1).TakeWhile(
                line => !line.StartsWith(segment)).ToList();
        }

        // Create a dictionary from a list of strings
        private Dictionary<char, string> GetDict(List<String> list){
            return list.Select(line => line.Split(' ')).ToDictionary(
                line => line[0][0], line => line[1]);
        }
    }
}

