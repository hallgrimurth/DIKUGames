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
        
        // private BlockFactory blockFactory;
        
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
                // If the file is not found, this exception is needed to avoid crashing
            }
        }
    



        //Load entities into entity container
        public void LoadMapEntities(List<string> mapLines) {
            try {
                float length = mapLines[0].Length;
                float height = mapLines.Count;
                //Add blocks to entity container depending on the char
                for (int i = 0; i < mapLines.Count ; i++) {
                    for (int j = 0 ; j < length; j++) {

                        if (legendDict.ContainsKey(mapLines[i][j])) {
                            //check if the char is a value in the meta dictionary if so, set type to that key
                            char type = 'n' ;
                            //print the key in metadata where the value is the char

                            
                            if (metaDict.ContainsValue(mapLines[i][j].ToString())) {
                                type = metaDict.Where(x => x.Value[0] == mapLines[i][j]).Select(x => x.Key).FirstOrDefault();
                            }
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

        private List<string> ParseSegment(string[] lines ,string segment){
            return lines.SkipWhile(line => !line.StartsWith(segment)).Skip(1).TakeWhile(line => !line.StartsWith(segment)).ToList();
        }

        private Dictionary<char, string> GetDict(List<String> list){
            return list.Select(line => line.Split(' ')).ToDictionary(line => line[0][0], line => line[1]);
        }
    }
}

