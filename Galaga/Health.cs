
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
public class Health {
    private int health;
    public int HealthPoints {
        get { return health; }
    }
    private Text display;
    public Health (Vec2F position, Vec2F extent) {
        health = 3;
        display = new Text ("HP:" + health.ToString(), position, extent);
        display.SetColor(new Vec3I(0, 255, 255));
    }
// Remember to explaination your choice as to what happens
    //when losing health.
    public void LoseHealth () {
        health--;
        display.SetText("HP:" + health.ToString());

    }

    public void GameOver(){
        throw new NotImplementedException();
    }
    
    public void RenderHealth () {
        display.RenderText();
    }
}