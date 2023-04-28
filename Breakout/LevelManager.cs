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

        //Loads the map and its block entities
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
                // If the file is not found, this exception is needed to avoid crashing
            }
        }
    



        //Load entities into entity container
        public void LoadMapEntities(List<string> mapLines) {
            try {
                //Map dimensions
                float length = mapLines[0].Length;
                float height = mapLines.Count;
                    //Add blocks to entity container depending on the char
                for (int i = 0; i < mapLines.Count ; i++) {
                    for (int j = 0 ; j < length; j++) {

                        if (legendDict.ContainsKey(mapLines[i][j])) {
                            //check if the char is a value in the meta dictionary if so, set type to that key
                            char type = 'n' ;
                            
                            if (metaDict.ContainsValue(mapLines[i][j].ToString())) type = metaDict.Where(x => x.Value[0] == mapLines[i][j]).Select(x => x.Key).FirstOrDefault();
                            // Calls the block-factory to create and add a block entity
                            Image blockImage = new Image(Path.Combine("Assets", "Images", legendDict[mapLines[i][j]]));
                            blocks.AddEntity(BlockFactory.CreateBlock(i, j, blockImage, type));

                        }
                    }
                }
            // }
            } catch (ArgumentOutOfRangeException) {
                // This exception is needed to avoid crashing if the file is empty or data is missing
            }
        }

        //We use this method to only read through the relevant segments of the level data 
        private List<string> ParseSegment(string[] lines ,string segment){
            return lines.SkipWhile(line => !line.StartsWith(segment)).Skip(1).TakeWhile(line => !line.StartsWith(segment)).ToList();
        }

        //Converts string into a dictionary where the first character of the first part is the key and the second part is the value
        private Dictionary<char, string> GetDict(List<String> list){
            return list.Select(line => line.Split(' ')).ToDictionary(line => line[0][0], line => line[1]);
        }
    }
}

