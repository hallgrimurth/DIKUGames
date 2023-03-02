using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Score {
    private DynamicShape shape;
    private Text scoreText;
    private int score;

    public Score() {
        shape = new DynamicShape(new Vec2F(0.0f, 0.9f), new Vec2F(0.3f, 0.1f));
        scoreText = new Text("0", new Vec2F(0.0f, -0.55f), new Vec2F(0.65f, 0.65f));
        scoreText.SetColor(new Vec3I(255, 255, 255));
        scoreText.SetFontSize(30);
    }

    public void AddPoint() {
        score++;
        scoreText.SetText(score.ToString());
    }

    public void Render() {
        scoreText.RenderText();
    }

    public void ResetScore() {
        score = 0;
        scoreText.SetText(score.ToString());
    }
}
