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

    public Points(Vec2F position, Vec2F extent, int startingvalue) {
        pointsValue = startingvalue;
        display = new Text("Score:" + pointsValue.ToString(), position, extent);
        display.SetColor(new Vec3I(0, 255, 255));
        display.SetFontSize(30);
    }

    private void AddPoints(string blockType) {
        // In the future we should use the Block classes and the BlockFactory class 
        switch (blockType) {
            case "Breakout.NormalBlock":
            pointsValue += 2; 
            break;
            case "Breakout.IndestructibleBlock":
            pointsValue += 100000;
            break;
            case "Breakout.HardenedBlock":
            pointsValue += 5;
            break;
            case "Breakout.PowerUpBlock":
            pointsValue += 10;
            break;
        }
        // Value must always be positive
        if (pointsValue >= 0){
            display.SetText("Score:" + pointsValue.ToString());
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
        if (gameEvent.EventType == GameEventType.ScoreEvent) {
            switch (gameEvent.Message) {
                case "ADD_SCORE":
                    AddPoints(gameEvent.StringArg1.ToString());
                    break;
                case "GAME_OVER":
                    FinalPoints();
                    break;
            }
        }
    }

}
