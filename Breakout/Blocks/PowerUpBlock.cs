using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class PowerUpBlock : Block {
    private int value;
    private PowerUp powerUp;
    
    public int Value {
        get { return value; }
    }
    //constructor for block
    public PowerUpBlock(DynamicShape Shape, IBaseImage image)
        : base(Shape, image) {
        value = 10;
        // SpawnPowerUp();
    }
    public override void DecreaseHealth() {
        this.Health--; 
        if (Health == 0) {
            DeleteEntity();
            // send event to make powerup move down
        }
    
    }

    // private void SpawnPowerUp() {
    //     Random rand = new Random();

    //     Vec2F position = Shape.Position;
    //     Vec2F extent = new Vec2F(0.0f, -0.01f);
    //     DynamicShape PowerUpShape = new DynamicShape(position, extent);
    //     int random = rand.Next(0, 3);
    //     switch(random){
    //         case 0:
    //             powerUp = new BigPowerUp(PowerUpShape, new Image(Path.Combine("Assets", "Images", "BigPowerUp.png")));
    //             Console.WriteLine(powerUp);
    //             break;
    //         case 1:
    //             powerUp = new WidePowerUp(PowerUpShape, new Image(Path.Combine("Assets", "Images", "WidePowerUp.png")));
    //             break;
    //         case 2:
    //             powerUp = new SplitPowerUp(PowerUpShape, new Image(Path.Combine("Assets", "Images", "SplitPowerUp.png")));
    //             break;
    //     }


        // if (random == 0) {
        //     powerUp = new BigPowerUp(Shape.Position, new Vec2F(0.0f, -0.01f), new Image(Path.Combine("Assets", "Images", "BigPowerUp.png")));
        // } else if (random == 1) {
        //     powerUp = new WidePowerUp(Shape.Position, new Vec2F(0.0f, -0.01f), new Image(Path.Combine("Assets", "Images", "WidePowerUp.png")));
        // } else if (random == 2) {
        //     powerUp = new SplitPowerUp(Shape.Position, new Vec2F(0.0f, -0.01f), new Image(Path.Combine("Assets", "Images", "SplitPowerUp.png")));
        // }
        
        
    // }

    //render powerup
    public void Render(){
        powerUp.RenderEntity();
    }
     
    
}