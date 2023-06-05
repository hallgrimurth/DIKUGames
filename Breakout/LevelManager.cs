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
        private int levelCounter = 0;
        private String[] levelPaths;
        private Level currentLevel;
        public Level CurrentLevel {
            get { return currentLevel; }
        }
        private bool start = false;
        public bool Start{
            get{ return start; }  
            set{ start = value; } 
        }

        public LevelManager(){
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
            levelPaths = Directory.GetFiles(Path.Combine(Constants.MAIN_PATH, "Assets/Levels/"));
            LoadLevel(levelPaths[levelCounter]);
        }

        public void LoadLevel(string filePath) {
            currentLevel = new Level(filePath);

        }
        private void NextLvl() {
            levelCounter++;
            if (levelCounter >= levelPaths.Count()) {
                levelCounter = 0;
            }
            LoadLevel(levelPaths[levelCounter]);

        }

        private void PrevLvl() {
            levelCounter--;
            if (levelCounter < 0) {
                levelCounter = levelPaths.Count() - 1;
            }
            LoadLevel(levelPaths[levelCounter]);
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
                        currentLevel.StartGame();
                        break;
                    case "SPAWN_POWERUP":
                        Vec2F pos = new Vec2F(
                            float.Parse(gameEvent.StringArg1), 
                            float.Parse(gameEvent.StringArg2));
                        PowerUp newPowerUp = PowerUpFactory.CreatePowerUp(pos);
                        newPowerUp.ChangeDirection(new Vec2F(0.0f, -0.005f));
                        currentLevel.powerups.AddEntity(newPowerUp);
                        break;
                    
                }
            }
        }

        public void RenderLevel() {
            currentLevel.Render();
        }

        public void UpdateLevel() {
            currentLevel.Update();
            }
}   
}

