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
        private int levelCounter = 1;
        private String[] levelPaths;
        private Level currentLevel;
        public Level CurrentLevel {
            get { return currentLevel; }
        }
        private bool start = false;
        public bool Start{
            get{ return start; }  
        }

        public LevelManager(){
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
            levelPaths = Directory.GetFiles(Path.Combine(Constants.MAIN_PATH, "Assets/Levels/"));
            LoadLevel(levelPaths[levelCounter]);
        }

        public void LoadLevel(string filePath) {
            start = false;
            currentLevel = new Level(filePath);

        }
        private void NextLvl() {
            levelCounter++;
            if (levelCounter >= levelPaths.Count()) {
                levelCounter = 0;
            }
            currentLevel.ClearLevel();
            LoadLevel(levelPaths[levelCounter]);
        }

        private void PrevLvl() {
            levelCounter--;
            if (levelCounter < 0) {
                levelCounter = levelPaths.Count() - 1;
            }
            currentLevel.ClearLevel();
            LoadLevel(levelPaths[levelCounter]);
        }


        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                Console.WriteLine(gameEvent.Message + " received in level manager " );
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
                        currentLevel.powerups.AddEntity(PowerUpFactory.CreatePowerUp(pos));
                        break;
                    
                }
            }
        }

        public void RenderLevel() {
            currentLevel.blocks.RenderEntities();
            currentLevel.powerups.RenderEntities();
        }
    }
}

