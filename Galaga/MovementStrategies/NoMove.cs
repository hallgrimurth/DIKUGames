using System;
using DIKUArcade.Entities;
using Galaga.MovementStrategy;

namespace Galaga.MovementStrategy {
    public class NoMove : IMovementStrategy {
        public void MoveEnemy (Enemy enemy){
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies){
            foreach(Enemy enemy in EntityContainer<Enemy> enemies){
                MoveEnemy(enemy);
            }
        }
    }
}
