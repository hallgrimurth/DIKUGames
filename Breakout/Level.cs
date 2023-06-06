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
    public class Level {
        private string[] textData;
        private List<string> mapData = new List<string>();
        private List<string> metaData = new List<string>();
        private List<string> legendData = new List<string>();
        private Dictionary<char, string> metaDict;
        public Dictionary<char, string> MetaDict{
            get{ return metaDict; }
        }
        private Dictionary<char, string> legendDict;
        private char type;
        public char Type {
            get{ return type; }   
        }
        
        private LevelManager levelManager;
        public LevelManager LevelManager {
            get{ return levelManager; }
            set{ levelManager = value; }
        }
        public EntityContainer<Block> blocks {get;}
        public EntityContainer<PowerUp> powerups {get;}
        public EntityContainer<Hazard> hazards {get;}
        private Ball ball;
        private CollisionManager collisionManager;        
        public List<string> MetaData{
            get{ return metaData; }
        }
        private Player player {get; set;}
        private bool start;
        private TimeManager timeManager;

        public Level(string filePath){
            blocks = new EntityContainer<Block>();
            powerups = new EntityContainer<PowerUp>();
            hazards = new EntityContainer<Hazard>();
            collisionManager = new CollisionManager();
            timeManager = new TimeManager();
            start = false;

            ClearLevel();
            LoadData(filePath);
            SendTimer();
            SetActors();
            SetBall();
            
        }

        public void SetActors(){
            player = new Player();
            SetBall();
        }

        public void SendTimer(){
            if (MetaDict.ContainsKey('T') ) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.GraphicsEvent,
                    Message = "DISPLAY_TIME",
                    StringArg1 = MetaDict['T']
                });
            } else if (MetaDict.ContainsKey('T') ) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.GraphicsEvent,
                    Message = "UPDATE_TIME" 
                });
            } else {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.GraphicsEvent,
                    Message = "NO_TIME" 
                });
            }
                    
        }

        
        private void SetBall() {
            Vec2F extent = new Vec2F(0.03f, 0.03f);
            Vec2F dir = new Vec2F(0.1f * 10e-6f, 0.01f);
            Vec2F pos = new Vec2F(
                (player.Shape.Position.X + (player.Shape.Extent.X / 2) - (extent.X / 2)), 
                (player.Shape.Position.Y + (3 * extent.Y)));
            DynamicShape ballShape = new DynamicShape(pos, extent);
            ball = new Ball(ballShape);
            ball.ChangeDirection(dir);
        }

        // Allows the player to aim the ball before the game starts
        private void AimBall(Ball ball) {
            if (start == false) {
                ball.Shape.Position.X = player.Shape.Position.X + (player.Shape.Extent.X / 2) - (ball.Shape.Extent.X / 2);
                ball.Shape.Position.Y = player.Shape.Position.Y + (3 * ball.Shape.Extent.Y);
            }
        }

        public void StartGame() {
            start = true;
        }

        public void LoadData(String filePath) {
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
                            //send event to collision manager
                            blocks.AddEntity(block);
                            // if (type == 'P'){
                            //     powerups.AddEntity(PowerUpFactory.CreatePowerUp(block.Shape.Position));
                            // } else if (type == 'D'){
                            //     hazards.AddEntity(HazardFactory.CreateHazard(block.Shape.Position));
                            // }
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

        public void ClearLevel() {
            blocks.ClearContainer();
            powerups.ClearContainer();
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.StatusEvent, 
                Message = "RESTART_LEVEL",
                From = this
                });
        }

        public void Update() {
            blocks.Iterate(block => 
                block.Update()
            );
            powerups.Iterate(powerup =>
                powerup.Update()
            );
            hazards.Iterate(hazard =>
                hazard.Update()
            );

            ball.Update();
            player.Update();
            AimBall(ball);
        }

        public void Render() {
            blocks.RenderEntities();
            powerups.RenderEntities();
            hazards.RenderEntities();
            ball.Render();
            player.Render();
        }
    }
}

