using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using System;

namespace Galaga.MovementStrategy {
    public class SideLoop : IMovementStrategy {

        public void MoveEnemy (Enemy enemy){
            enemy.shape.MoveX(-enemy.speed * 2);

            if (enemy.shape.Position.X > 1.0f) {
                enemy.shape.SetPosition(new Vec2F(0.0f - enemy.shape.Extent.Y, enemy.shape.Position.Y));
            }

            enemy.shape.MoveY(enemy.speed / 2);
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies){
            foreach(Enemy enemy in enemies){
                MoveEnemy(enemy);
            }
        }
    }
}