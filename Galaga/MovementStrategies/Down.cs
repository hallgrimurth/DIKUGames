using System;
using DIKUArcade;
using DIKUArcade.Entities;
using Galaga.MovementStrategy;


namespace Galaga.MovementStrategy {

    public class Down : IMovementStrategy {
        public void MoveEnemy (Enemy enemy){
            enemy.Shape.MoveY(enemy.speed);
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies) {
            foreach(Enemy enemy in enemies){
                MoveEnemy(enemy);
            }     
            
        }
    }
}

