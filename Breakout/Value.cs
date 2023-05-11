using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;

namespace Breakout;
public class Value : IGameEventProcessor {
    private Text display;
    private int value;

    public Value(Vec2F position, Vec2F extent, int startingvalue) {
        value = startingvalue;
        display = new Text("Score:" + value.ToString(), position, extent);
        display.SetColor(new Vec3I(0, 255, 255));
        display.SetFontSize(30);
    }

    private void AddValue(string blockType) {
        // var block = new BlockFactory();

        switch (blockType) {
            case "Breakout.NormalBlock":
            value += 2; 
            break;
            case "Breakout.IndestructibleBlock":
            value += 0;
            break;
            case "Breakout.HardenedBlock":
            value += 5;
            break;
            case "Breakout.PowerUpBlock":
            value += 10;
            break;
        }
        display.SetText("Score:" + value.ToString());
    }

    private void FinalValue() {
        display = new Text("GameOver \n you reached \n   level: " + value.ToString(), 
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
                    Console.WriteLine(gameEvent.StringArg1);
                    AddValue(gameEvent.StringArg1.ToString());
                    break;
            }
        }
    }

}
