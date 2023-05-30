using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;

namespace Breakout;
public class Points : IGameEventProcessor {
    private Text display;
    private int pointsValue;
    public int PointsValue{
        get {return pointsValue;}
        set {pointsValue = value;}
    }

    public Points(Vec2F position, Vec2F extent) {
        display = new Text("Score:" + pointsValue.ToString(), position, extent);
        display.SetColor(new Vec3I(0, 255, 255));
        display.SetFontSize(30);
        // BreakoutBus.GetBus().InitializeEventBus
        //     (GameEventType.ScoreEvent);
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
    }

    private void AddPoints(int point) {
        // Value must always be positive

        pointsValue += point;
        if (pointsValue >= 0){
            display.SetText("Score: " + pointsValue.ToString());
        } else {
            pointsValue = 0;
        }
    }

    private void FinalPoints() {
        display = new Text("GameOver \n you reached \n   level: " + pointsValue.ToString(), 
            new Vec2F(0.25f, 0.25f), new Vec2F(0.5f, 0.5f));
        display.SetColor(new Vec3I(0, 255, 255));
    }
    public void Render() {
        display.RenderText();
    }

     public void ProcessEvent(GameEvent gameEvent) {

        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            switch (gameEvent.Message) {
                case "ADD_POINTS":
                    Console.WriteLine("Points: " + gameEvent.IntArg1);
                    AddPoints(gameEvent.IntArg1);
                    break;
                case "GAME_OVER":
                    FinalPoints();
                    break;
            }
        }
    }

}
