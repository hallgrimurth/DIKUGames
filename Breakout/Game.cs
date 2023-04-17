using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using Breakout.BreakoutStates;

namespace Breakout{
    class Game : DIKUGame, IGameEventProcessor {
        //state machine
        // private StateMachine stateMachine ;
        //Entities
        // private GameEventBus eventBus;
        private string filePath;
        private LevelManager level;
        private List<GameEventType> eventQueue;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            filePath = "C:/Users/Hallgrimur/Desktop/KU/SoftwareDev/Assignment_4/DIKUGames/Breakout/Assets/Levels/central-mass.txt";
            level = new LevelManager(filePath);
        } 

        public void ProcessEvent(GameEvent gameEvent) {  
            //  switch (gameEvent.EventType) {
                // case GameEventType.WindowEvent:
                // //send message to state machine
                //     window.CloseWindow();
                //     break;

                // case GameEventType.GameStateEvent:
                //     stateMachine.ProcessEvent(gameEvent);
                //     window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
                //     break;
            // }    
        }

        public override void Render() {
            window.Clear();
            // stateMachine.ActiveState.RenderState();
        }
        
        public override void Update() {
            //make new window and display game over text
            // stateMachine.ActiveState.UpdateState();
            window.PollEvents();
            // BreakoutBus.GetBus().ProcessEventsSequentially();
        }
    }
}