using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;

namespace Breakout
{
    /// <summary>
    /// The Points class handles the score display in the Breakout game.
    /// </summary>
    public class Points : IGameEventProcessor
    {
        private Text display;
        private int pointsValue = 0;

        /// <summary>
        /// The current points value.
        /// </summary>
        public int PointsValue
        {
            get { return pointsValue; }
            set { pointsValue = value; }
        }

        /// <summary>
        /// Constructs a Points instance.
        /// </summary>
        /// <param name="position">The position of the score display.</param>
        /// <param name="extent">The extent of the score display.</param>
        public Points(Vec2F position, Vec2F extent)
        {
            display = new Text("Score:" + pointsValue.ToString(), position, extent);
            display.SetColor(new Vec3I(255, 255, 255));

            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
        }

        /// <summary>
        /// Adds points to the score.
        /// </summary>
        /// <param name="point">The points to add.</param>
        private void AddPoints(int point)
        {
            // Value must always be positive
            pointsValue += point;
            if (pointsValue >= 0)
            {
                display.SetText("Score: " + pointsValue.ToString());
            }
            else
            {
                pointsValue = 0;
            }
        }

        /// <summary>
        /// Displays the final points when the game is over.
        /// </summary>
        private void FinalPoints()
        {
            display = new Text("GameOver \n you reached \n   level: " + pointsValue.ToString(),
                new Vec2F(0.25f, 0.25f), new Vec2F(0.5f, 0.5f));
            display.SetColor(new Vec3I(255, 255, 255));
        }

        /// <summary>
        /// Renders the score display.
        /// </summary>
        public void Render()
        {
            display.RenderText();
        }
        public void Update()
        {
            if (pointsValue >= 25)
            {
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent{
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_WON"
                    }
                );
        }
        }

        /// <summary>
        /// Processes game events.
        /// </summary>
        /// <param name="gameEvent">The game event to process.</param>
        public void ProcessEvent(GameEvent gameEvent)
        {
            if (gameEvent.EventType == GameEventType.PlayerEvent)
            {
                switch (gameEvent.Message)
                {
                    case "ADD_POINTS":
                        AddPoints(gameEvent.IntArg1);
                        break;
                    case "GAME_OVER":
                        FinalPoints();
                        break;
                }
            }
        }
    }
}