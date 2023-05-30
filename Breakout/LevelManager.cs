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
using Breakout.BreakoutStates;


namespace Breakout{ 
    public class LevelManager : IGameEventProcessor {
        private string[] textData;
        private List<string> mapData = new List<string>();
        private List<string> metaData = new List<string>();
        private List<string> legendData = new List<string>();
        private Dictionary<char, string> metaDict;
        public Dictionary<char, string> MetaDict{
            get{ return metaDict; }
        }
        private Dictionary<char, string> legendDict;
        private int levelCounter = 1;
        private String[] levelPaths;
        private bool start = false;
        private char type;
        public bool Start{
            get{ return start; }  
        }
        public char Type {
            get{ return type; }   
        }
        
        public EntityContainer<Block> blocks {get;}
        public EntityContainer<PowerUp> powerups {get;}
        public List<string> MetaData{
            get{ return metaData; }
        }

        public LevelManager(){
            blocks = new EntityContainer<Block>();
            powerups = new EntityContainer<PowerUp>();
            levelPaths = Directory.GetFiles(Path.Combine(Constants.MAIN_PATH, "Assets/Levels/"));
            LoadMap(levelPaths[levelCounter]);
        }

        public void LoadMap(string filePath) {
            start = false;
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
                            type = 'N' ;

                            // Check if the char is a value in the meta dictionary if so, set 
                            // type to that key
                            if (metaDict.ContainsValue(mapLines[i][j].ToString())) 
                                {
                                    type = metaDict.Where(
                                        x => x.Value[0] == mapLines[i][j]).Select(
                                            x => x.Key).FirstOrDefault();
                                }
                            // Calls the block-factory to create and add a block entity
                            string blockImage = legendDict[mapLines[i][j]];
                            Block block = BlockFactory.CreateBlock(i, j, blockImage, type);
                            blocks.AddEntity(block);
                            if (type == 'P'){
                                // powerups.AddEntity(PowerUpFactory.CreatePowerUp(block.Shape.Position));
                            }
                        }
                    // // These catches are needed to avoid crashing if the file is empty or data is missing
                    // // The program will continue without placing a block
                    } catch (ArgumentOutOfRangeException) {

                    } catch (IndexOutOfRangeException) {

                    }
                }
            }
        }

        

        // Parse a segment of the text file
        private List<string> ParseSegment(string[] lines ,string segment){
            return lines.SkipWhile(
                line => !line.StartsWith(segment)).Skip(1).TakeWhile(
                line => !line.StartsWith(segment)).ToList();
        }

        // Create a dictionary from a list of strings
        private Dictionary<char, string> GetDict(List<String> list){
            return list.Select(
                line => line.Split(' ')).ToDictionary(
                line => line[0][0], line => line[1]);
        }

        private void NextLvl() {
            levelCounter++;
            if (levelCounter >= levelPaths.Count()) {
                levelCounter = 0;
            }
            blocks.ClearContainer();
            powerups.ClearContainer();
            LoadMap(levelPaths[levelCounter]);
        }

        private void PrevLvl() {
            levelCounter--;
            if (levelCounter < 0) {
                levelCounter = levelPaths.Count() - 1;
            }
            blocks.ClearContainer();
            powerups.ClearContainer();
            LoadMap(levelPaths[levelCounter]);
        }


        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch (gameEvent.Message) {
                    case "PREV_LEVEL":
                        PrevLvl();
                        break;
                    case "NEXT_LEVEL":
                        NextLvl();
                        break;
                    case "START_GAME":
                        start = true;
                        break;
                    case "SPAWN_POWERUP":
                        Vec2F pos = new Vec2F(
                            float.Parse(gameEvent.StringArg1), 
                            float.Parse(gameEvent.StringArg2));
                        PowerUp newPowerUp = PowerUpFactory.CreatePowerUp(pos);
                        newPowerUp.ChangeDirection(new Vec2F(0.0f, -0.005f));
                        powerups.AddEntity(newPowerUp);
                        break;
                    
                }
            }
        }
    }
}

