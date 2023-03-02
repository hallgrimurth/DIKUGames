using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;

// create a score object that displays the number of points the player has based on the number of enemies killed
namespace Galaga;
public class Score {
    private Entity entity;
    private DynamicShape shape;
    private Text scoreText;
    private int score;

    public Score() {
        shape = new DynamicShape(new Vec2F(0.0f, 0.9f), new Vec2F(0.3f, 0.1f));
        entity = new Entity(shape, new Image(Path.Combine("Assets", "Images", "Score.png")));
        scoreText = new Text("0", new Vec2F(0.0f, 0.6f), new Vec2F(0.65f, 0.4f));
        scoreText.SetColor(new Vec3I(255, 255, 255));
        scoreText.SetFontSize(30);
    }

    public void AddPoint() {
        score++;
        scoreText.SetText(score.ToString());
    }

    public void RenderScore() {
        entity.RenderEntity();
        scoreText.RenderText();
    }

    public void ResetScore() {
        score = 0;
        scoreText.SetText(score.ToString());
    }
}
