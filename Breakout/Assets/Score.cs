using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class Score {
    private Text display;
    public int score;

    public Score(Vec2F position, Vec2F extent, int startingscore) {
        display = new Text("Score:" + score.ToString(), position, extent);
        display.SetColor(new Vec3I(211, 211, 211));
        display.SetFontSize(30);
        score = startingscore;
    }

    public void AddPoint(int points) {
        score+=points;
        display.SetText("Score:" + score.ToString());
    }

 
    public void Render() {
        display.RenderText();
    }

}
