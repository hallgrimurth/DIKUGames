using DIKUArcade.Events;
using System.Collections.Generic;

namespace Breakout {
    public static class BreakoutBus {
        private static GameEventBus eventBus;

        /// <summary>
        /// Returns the singleton instance of the game event bus.
        /// </summary>
        /// <returns>The game event bus instance.</returns>
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
        }
    }
}
