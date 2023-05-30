using DIKUArcade.Events;
using System.Collections.Generic;

namespace Breakout {
    public static class BreakoutBus {
        private static GameEventBus eventBus;
        public static GameEventBus GetBus() {
            if (BreakoutBus.eventBus == null) {
                BreakoutBus.eventBus = new GameEventBus();

                BreakoutBus.eventBus.InitializeEventBus(new List<GameEventType>() {
                    GameEventType.InputEvent,
                    GameEventType.WindowEvent,
                    GameEventType.MovementEvent,
                    GameEventType.GameStateEvent,
                    GameEventType.StatusEvent,
                    GameEventType.ControlEvent,
                    GameEventType.PlayerEvent,
                    GameEventType.ScoreEvent
                });

            }
            return BreakoutBus.eventBus;
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

