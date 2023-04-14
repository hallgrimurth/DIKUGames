using DIKUArcade.Events;

namespace Breakout {
    public static class BreakoutBus {
        private static GameEventBus eventBus;
        public static GameEventBus GetBus() {
            return Breakout.eventBus ?? (Breakout.eventBus = new GameEventBus());
        }
    }
}

