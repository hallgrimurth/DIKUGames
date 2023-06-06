using System;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Entities;
using DIKUArcade.Events;

namespace Breakout {
    public class TimeManager: IGameEventProcessor  {
        private Text display;
        private int givenTime;
        private int elapsedTime;
        private bool hasTime;
        public TimeManager() {
            StaticTimer.RestartTimer();
            display = new Text("Time: ", new Vec2F(0.33f, -0.3f), new Vec2F(0.4f, 0.4f));
            display.SetColor(new Vec3I(255, 255, 255));
            display.SetFontSize(30);
            BreakoutBus.GetBus().Subscribe(GameEventType.GraphicsEvent, this);
        }
        public void ProcessEvent(GameEvent gameEvent) {

            switch (gameEvent.Message) {
                case "DISPLAY_TIME":
                    hasTime = true;
                    givenTime = Int32.Parse(gameEvent.StringArg1);
                    display.SetText("Time:" + (givenTime));
                    break;
                case "NO_TIME":
                    hasTime = false;
                    display.SetText("Time: ");
                    break;
                
                
            }
        }

        public void Update(){
            if (hasTime){
                elapsedTime = (int)(StaticTimer.GetElapsedSeconds());
                display.SetText("Time:" + (givenTime - elapsedTime).ToString());
                if (givenTime - elapsedTime <= 0){
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent { 
                            EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_OVER", 
                            ObjectArg1 = this 
                        }   
                    );
                        
                }
            }
        }

        public void Render(){
            display.RenderText();   
        }
    }
}


