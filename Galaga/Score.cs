using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Score {
    private Text display;
    private int score = 1;

    public Score(Vec2F position, Vec2F extent) {
        // shape = new DynamicShape(new Vec2F(0.0f, 0.9f), new Vec2F(0.3f, 0.1f));
        display = new Text("level:" + score.ToString(), position, extent);
        display.SetColor(new Vec3I(0, 255, 255));
        display.SetFontSize(30);
    }

    public void AddPoint() {
        score++;
        display.SetText("level:" + score.ToString());
    }

    public void Render() {
        display.RenderText();
    }

    public void ResetScore() {
        score = 0;
        display.SetText("level:" + score.ToString());
    }
}
