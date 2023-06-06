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
    /// The LevelManager class handles loading and managing levels in the Breakout game.
    /// </summary>
    public class LevelManager : IGameEventProcessor
    {
        private int levelCounter = 0;
        private String[] levelPaths;
        private Level currentLevel;
        
        /// <summary>
        /// The current level being played.
        /// </summary>
        public Level CurrentLevel
        {
            get { return currentLevel; }
        }
        
        private bool start = false;

        /// <summary>
        /// Indicates whether the game should start.
        /// </summary>
        public bool Start
        {
            get { return start; }
            set { start = value; }
        }

        /// <summary>
        /// Constructs a LevelManager instance.
        /// </summary>
        public LevelManager()
        {
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
            levelPaths = Directory.GetFiles(Path.Combine(Constants.MAIN_PATH, "Assets/Levels/"));
            LoadLevel(levelPaths[levelCounter]);
        }

        /// <summary>
        /// Loads a level from the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the level file to load.</param>
        public void LoadLevel(string filePath)
        {
            currentLevel = new Level(filePath);
        }

        /// <summary>
        /// Loads the next level.
        /// </summary>
        private void NextLvl()
        {
            levelCounter++;
            if (levelCounter >= levelPaths.Count())
            {
                levelCounter = 0;
            }
            LoadLevel(levelPaths[levelCounter]);
        }

        /// <summary>
        /// Loads the previous level.
        /// </summary>
        private void PrevLvl()
        {
            levelCounter--;
            if (levelCounter < 0)
            {
                levelCounter = levelPaths.Count() - 1;
            }
            LoadLevel(levelPaths[levelCounter]);
        }

        /// <summary>
        /// Processes game events.
        /// </summary>
        /// <param name="gameEvent">The game event to process.</param>
        public void ProcessEvent(GameEvent gameEvent)
        {
            switch (gameEvent.Message)
            {
                case "PREV_LEVEL":
                    PrevLvl();
                    break;
                case "NEXT_LEVEL":
                    NextLvl();
                    break;
                case "START_GAME":
                    currentLevel.StartGame();
                    break;
                case "SPAWN_POWERUP":
                    Vec2F pos = new Vec2F(
                        float.Parse(gameEvent.StringArg1),
                        float.Parse(gameEvent.StringArg2));
                    PowerUp newPowerUp = PowerUpFactory.CreatePowerUp(pos);
                    newPowerUp.ChangeDirection(new Vec2F(0.0f, -0.0035f));
                    currentLevel.powerups.AddEntity(newPowerUp);
                    break;
                case "SPAWN_HAZARD":
                    Vec2F pos2 = new Vec2F(
                        float.Parse(gameEvent.StringArg1), 
                        float.Parse(gameEvent.StringArg2));
                    Hazard newHazard = HazardFactory.CreateHazard(pos2);
                    newHazard.ChangeDirection(new Vec2F(0.0f, -0.0035f));
                    currentLevel.hazards.AddEntity(newHazard);
                    break;
                case "RESET_BALL":
                    Console.WriteLine("RESET_BALL");
                    this.start = false;
                    currentLevel.Start = false;
                    currentLevel.SetBall();
                    break;
            }
        }

        /// <summary>
        /// Renders the current level.
        /// </summary>
        public void RenderLevel()
        {
            currentLevel.Render();
        }

        /// <summary>
        /// Updates the current level.
        /// </summary>
        public void UpdateLevel()
        {
            currentLevel.Update();
        }
    }
}
