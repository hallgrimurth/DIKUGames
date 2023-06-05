using DIKUArcade.Events;
using System.Collections.Generic;

namespace Breakout {
    public static class BreakoutBus {
        private static GameEventBus eventBus;
        public static GameEventBus GetBus() {
            if (eventBus == null) {
                
                eventBus = new GameEventBus();

                eventBus.InitializeEventBus(new List<GameEventType>() {
                    GameEventType.InputEvent,
                    GameEventType.WindowEvent,
                    GameEventType.MovementEvent,
                    GameEventType.GameStateEvent,
                    GameEventType.StatusEvent,
                    GameEventType.ControlEvent,
                    GameEventType.PlayerEvent,
                    GameEventType.GraphicsEvent

                });

            }
            return eventBus;
            // return BreakoutBus.eventBus ?? (BreakoutBus.eventBus = new GameEventBus());
        }

        // public static BreakoutBus() {
        //     GetBus();

        //     GetBus().initializeEventBus(new List<GameEventType>() {
        //         GameEventType.InputEvent,
        //         GameEventType.WindowEvent,
        //         GameEventType.MovementEvent,
        //         GameEventType.GameStateEvent,
        //         GameEventType.StatusEvent,
        //         GameEventType.ControlEvent,
        //         GameEventType.PlayerEvent,
        //         GameEventType.ScoreEvent
        //     });
        // }
    }
}

