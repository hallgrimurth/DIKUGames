using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
public class HardenedBlock : Block {
    private int value;
    private IBaseImage damageImage;
    //get for damage image
    public IBaseImage DamageImage {
        get { return damageImage; }
    }
    public int Value {
        get { return value; }
    }
    
    //constructor for block
    public HardenedBlock(DynamicShape Shape, IBaseImage image, IBaseImage DamageImage)
        : base(Shape, image) {
        value = 2;
        Health = 2;
        damageImage = DamageImage;
        

    }

    public override void DecreaseHealth() {
        
        this.Health--;

        if (this.Health == 1) {
            this.Image = damageImage;
        }
        
        
        
    }
        
}