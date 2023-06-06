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

namespace Breakout
{
    /// <summary>
    /// Represents a level in the Breakout game.
    /// </summary>
    public class Level
    {
        private string[] textData;
        private List<string> mapData = new List<string>();
        private List<string> metaData = new List<string>();
        private List<string> legendData = new List<string>();
        private Dictionary<char, string> metaDict;
        private Dictionary<char, string> legendDict;
        private char type;
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
        private Player player;
        private bool start;
        public bool Start {
            get{ return start; }
            set{ start = value; }
        }
        private TimeManager timeManager;

        /// <summary>
        /// Gets the metadata associated with the level.
        /// </summary>
        public List<string> MetaData
        {
            get { return metaData; }
        }

        /// <summary>
        /// Creates a new level instance with the specified file path.
        /// </summary>
        /// <param name="filePath">The path to the level file.</param>
        public Level(string filePath)
        {
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
        }

        /// <summary>
        /// Sets the player and ball actors in the level.
        /// </summary>
        public void SetActors()
        {
            player = new Player();
            SetBall();
        }

        /// <summary>
        /// Sends a timer event based on the level metadata.
        /// </summary>
        public void SendTimer()
        {
            if (metaDict.ContainsKey('T'))
            {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.GraphicsEvent,
                    Message = "DISPLAY_TIME",
                    StringArg1 = metaDict['T']
                });
            }
            else if (metaDict.ContainsKey('T'))
            {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.GraphicsEvent,
                    Message = "UPDATE_TIME"
                });
            }
            else
            {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.GraphicsEvent,
                    Message = "NO_TIME"
                });
            }
        }

        /// <summary>
        /// Sets the initial position and direction of the ball.
        /// </summary>
        public void SetBall()
        {
            Vec2F extent = new Vec2F(0.03f, 0.03f);
            Vec2F dir = new Vec2F(0.1f * 10e-6f, 0.01f);
            Vec2F pos = new Vec2F(
                (player.Shape.Position.X + (player.Shape.Extent.X / 2) - (extent.X / 2)),
                (player.Shape.Position.Y + (3 * extent.Y)));
            DynamicShape ballShape = new DynamicShape(pos, extent);
            ball = new Ball(ballShape);
            ball.ChangeDirection(dir);
        }

        /// <summary>
        /// Allows the player to aim the ball before the game starts.
        /// </summary>
        /// <param name="ball">The ball entity.</param>
        private void AimBall(Ball ball)
        {
            if (start == false)
            {
                ball.Shape.Position.X = player.Shape.Position.X + (player.Shape.Extent.X / 2) - (ball.Shape.Extent.X / 2);
                ball.Shape.Position.Y = player.Shape.Position.Y + (3 * ball.Shape.Extent.Y);
            }
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            start = true;
        }

        /// <summary>
        /// Loads the level data from the specified file path.
        /// </summary>
        /// <param name="filePath">The path to the level file.</param>
        public void LoadData(String filePath)
        {
            LoadTextData(filePath);
            LoadMapEntities(mapData);
        }

        /// <summary>
        /// Loads the text data from the level file.
        /// </summary>
        /// <param name="filePath">The path to the level file.</param>
        public void LoadTextData(string filePath)
        {
            try
            {
                textData = File.ReadAllLines(filePath);

                mapData = ParseSegment(textData, "Map");
                metaData = ParseSegment(textData, "Meta");
                legendData = ParseSegment(textData, "Legend");

                metaDict = GetDict(metaData);
                legendDict = GetDict(legendData);
            }
            catch (FileNotFoundException)
            {
                // If the file is not found, this catch is needed to avoid crashing
            }
        }

        /// <summary>
        /// Loads the map entities into the entity container.
        /// </summary>
        /// <param name="mapLines">The lines representing the map.</param>
        public void LoadMapEntities(List<string> mapLines) {
            float length = 12;
            float height = 24;

            for (int i = 0; i < height; i++) {
                for (int j = 0; j < length; j++) {
                    try {
                        if (legendDict.ContainsKey(mapLines[i][j])) {
                            type = 'N';

                            if (metaDict.ContainsValue(mapLines[i][j].ToString())) {
                                type = metaDict.Where(
                                    x => x.Value[0] == mapLines[i][j]).Select(
                                        x => x.Key).FirstOrDefault();
                            }

                            string blockImage = legendDict[mapLines[i][j]];
                            Block block = BlockFactory.CreateBlock(i, j, blockImage, type);
                            blocks.AddEntity(block);
                            // if (type == 'P'){
                            //     powerups.AddEntity(PowerUpFactory.CreatePowerUp(block.Shape.Position));
                            // } else if (type == 'D'){
                            //     hazards.AddEntity(HazardFactory.CreateHazard(block.Shape.Position));
                            // }
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // Ignore and continue if the file is empty or data is missing
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // Ignore and continue if the file is empty or data is missing
                    }
                }
            }
        }

        /// <summary>
        /// Parses a segment of the text file.
        /// </summary>
        /// <param name="lines">The lines of the text file.</param>
        /// <param name="segment">The segment to parse.</param>
        /// <returns>The parsed segment as a list of strings.</returns>
        private List<string> ParseSegment(string[] lines, string segment) {
            return lines.SkipWhile(
                line => !line.StartsWith(segment)).Skip(1).TakeWhile(
                line => !line.StartsWith(segment)).ToList();
        }

        /// <summary>
        /// Creates a dictionary from a list of strings.
        /// </summary>
        /// <param name="list">The list of strings.</param>
        /// <returns>The created dictionary.</returns>
        private Dictionary<char, string> GetDict(List<String> list) {
            return list.Select(
                line => line.Split(' ')).ToDictionary(
                line => line[0][0], line => line[1]);
        }

        /// <summary>
        /// Clears the level by removing all blocks and power-ups.
        /// </summary>
        public void ClearLevel() {
            blocks.ClearContainer();
            powerups.ClearContainer();

            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "RESTART_LEVEL",
                From = this
            });
        }

        /// <summary>
        /// Updates the level by updating all entities and aiming the ball.
        /// </summary>
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

        /// <summary>
        /// Renders the level by rendering all entities.
        /// </summary>
        public void Render() {
            blocks.RenderEntities();
            powerups.RenderEntities();
            hazards.RenderEntities();
            ball.Render();
            player.Render();
        }
    }
}