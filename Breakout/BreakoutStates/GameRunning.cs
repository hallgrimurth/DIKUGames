using DIKUArcade.State;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using System;


namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        //Entities
        private Player player;


        public int NumEnemies {
            get { return numEnemies; }
            set { numEnemies = value; }
        }


        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        
        public void InitializeGameState(){
        }


        public void KeyPress(KeyboardKey key){
            switch(key) {               
                    }
        }


        public void KeyRelease(KeyboardKey key){
            switch(key){
            }
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
        
            switch(action){
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;

                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;        
            }
        }


        public void RenderState() {

            health.RenderHealth();
            player.Render();
            playerShots.RenderEntities();
            squad.Enemies.RenderEntities();
            enemyExplosions.RenderAnimations();
            level.Render();

        }

        public void ResetState(){ 
            InitializeGameState();
        }

        public void UpdateState(){
            IterateHealth();
            IterateEnemies();
            IterateShots();
            player.Move();
        }
    }
}