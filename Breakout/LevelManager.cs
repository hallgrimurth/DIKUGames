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
        // private List<string> availableLevels = new List<string>();
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

        public void LoadMap(string filePath) {
            LoadTextData(filePath);
            LoadMapMetrics(mapLines);
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
                // Create the map from the lines
                foreach (string line in mapLines) {
                    //Add blocks to entity container depending on the char
                    for (int i = 0; i < height ; i++) {
                        for (int j = 0 ; j < length; j++) {

                            if (legendDict.ContainsKey(mapLines[i][j])) {
                                // Calls the block-factory to create and add a block entity
                                Image blockImage = new Image(Path.Combine("Assets", "Images", legendDict[mapLines[i][j]]));
                                blocks.AddEntity(BlockFactory.CreateBlock(i, j, blockImage));

                            }
                        }
                    }
            }
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

