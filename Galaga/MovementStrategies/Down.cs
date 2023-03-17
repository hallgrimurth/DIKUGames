using System;
using DIKUArcade;
using DIKUArcade.Entities;
using Galaga.MovementStrategy;


namespace Galaga.MovementStrategy {

    public class Down : IMovementStrategy {

        // // Moving enemies down at random speeds and deleting them if they are out of bounds
        // // Also resetting score and enemy speed if enemy is out of bounds
        public void MoveEnemy (Enemy enemy){
            float speed = enemySpeed + rand.Next(1, 100) / 25000.0f;
            enemy.Shape.MoveY(-speed);
            if (enemy.Shape.Position.Y < -0.1f) {
                    enemy.DeleteEntity();
                    //score.ResetScore();
                    enemySpeed = 0.0f;
                }

        }
        public void MoveEnemies (EntityContainer<Enemy> enemies) {
            foreach(Enemy enemy in EntityContainer<Enemy> enemies){
                MoveEnemy(enemy);
            }
        }

    }
}
