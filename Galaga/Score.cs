using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Score {
    private Text display;
    public int score;

    public Score(Vec2F position, Vec2F extent, int startingscore) {
        // shape = new DynamicShape(new Vec2F(0.0f, 0.9f), new Vec2F(0.3f, 0.1f));
        display = new Text("level:" + score.ToString(), position, extent);
        display.SetColor(new Vec3I(0, 255, 255));
        display.SetFontSize(30);
        score = startingscore;
    }

    public void AddPoint() {
        score++;
        display.SetText("level:" + score.ToString());
    }

    public void FinalScore() {
        display = new Text("GameOver \n you reached \n   level: " + score.ToString(), 
            new Vec2F(0.25f, 0.25f), new Vec2F(0.5f, 0.5f));
        display.SetColor(new Vec3I(0, 255, 255));
    }
    public void Render() {
        display.RenderText();
    }

}
