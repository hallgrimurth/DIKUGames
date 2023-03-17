using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga.Squadron {
    public class ZigZag : ISquadron {
        public EntityContainer<Enemy> Enemies {get;}
        public int MaxEnemies {get;}
        public void CreateEnemies (List<Image> enemyStride,List<Image> alternativeEnemyStride){
                // zigzag pattern
            for (int i = 0; i < MaxEnemies; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, (0.9f - ((float)i*0.1f%0.2f))), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, BlueMonster), new ImageStride(80, enemyStridesRed)));
            }

        }
    }
}

        // void ZigZagFormation(){
        //     enemies = new CreateEnemies (List<Image> enemyStride,
        //     List<Image> alternativeEnemyStride);
            //new EntityContainer<Enemy>(numEnemies);
            // zigzag pattern
                // for (int i = 0; i < MaxEnemies; i++) {
                //  enemies.AddEntity(new Enemy(
                        // new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, (0.9f - ((float)i*0.1f%0.2f))), new Vec2F(0.1f, 0.1f)),
                        // new ImageStride(80, BlueMonster), new ImageStride(80, enemyStridesRed)));
                // }
        // }
            
            
        
            // normal enemies in a row
            //for (int i = 0; i < numEnemies; i++) {
            //     enemies.AddEntity(new Enemy(
            //         new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
            //         new ImageStride(80, BlueMonster), new ImageStride(80, enemyStridesRed)));
            // }
        

            // // wave pattern
            // for (int i = 0; i < numEnemies; i++) {
            //     enemies.AddEntity(new Enemy(
            //         new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, (0.9f - (float)i*0.03f)), new Vec2F(0.1f, 0.1f)),
            //         new ImageStride(80, BlueMonster), new ImageStride(80, enemyStridesRed)));
            // }

            // // enemies in a circle
            // for (int i = 0; i < numEnemies; i++) {
            //     enemies.AddEntity(new Enemy(
            //         new DynamicShape(new Vec2F(0.45f + (float)Math.Cos(i*2*Math.PI/numEnemies)*0.2f, 0.6f + (float)Math.Sin(i*2*Math.PI/numEnemies)*0.2f), new Vec2F(0.1f, 0.1f)),
            //         new ImageStride(80, BlueMonster), new ImageStride(80, enemyStridesRed)));
            // }