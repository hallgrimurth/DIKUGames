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



namespace Breakout{
    class Game : DIKUGame {
        private GameEventBus eventBus;


        public Game(WindowArgs windowArgs) : base(windowArgs) {
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            // window.SetKeyEventHandler(KeyHandler);
            // eventBus.Subscribe(GameEventType.InputEvent, this); 
        
        }

        public override void Render() { 
                
        }

        public override void Update() {
            throw new Exception("");
        }
    }
}