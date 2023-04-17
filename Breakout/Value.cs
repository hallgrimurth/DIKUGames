using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class Value {
    private Text display;
    public int value;

    public Value(Vec2F position, Vec2F extent, int startingvalue) {
        display = new Text("level:" + value.ToString(), position, extent);
        display.SetColor(new Vec3I(0, 255, 255));
        display.SetFontSize(30);
        value = startingvalue;
    }

    public void AddValue() {
        value++;
        display.SetText("level:" + value.ToString());
    }

    public void FinalValue() {
        display = new Text("GameOver \n you reached \n   level: " + value.ToString(), 
            new Vec2F(0.25f, 0.25f), new Vec2F(0.5f, 0.5f));
        display.SetColor(new Vec3I(0, 255, 255));
    }
    public void Render() {
        display.RenderText();
    }

}
