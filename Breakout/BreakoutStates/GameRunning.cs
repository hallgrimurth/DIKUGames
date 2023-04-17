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


        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        
        public void InitializeGameState(){
            player = SetPlayer();
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


        public Player SetPlayer(){
            playerStrides = ImageStride.CreateStrides
                (4, Path.Combine("Assets", "Images", "playerStride.png"));
            player = new Player(
                new DynamicShape(new Vec2F(0.4f, 0.2f), new Vec2F(0.2f, 0.05f)),
                new ImageStride(160, playerStrides));
            return player;

        public void RenderState() {
        }

        public void ResetState(){ 
            InitializeGameState();
        }

        public void UpdateState(){
        }
    }
}