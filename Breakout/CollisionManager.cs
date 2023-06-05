using System.Collections.Generic;

using DIKUArcade.Physics;
using DIKUArcade.Events;
using System;

namespace Breakout {

    public class CollisionManager : IGameEventProcessor {


        private Dictionary<string, List<ICollidable>> collisionEvents;


        public CollisionManager() {
            collisionEvents = new Dictionary<string, List<ICollidable>>();
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        }

        public void ProcessEvent(GameEvent gameEvent) {

            switch (gameEvent.Message) {
                case var value when value == "TRY_COLLIDE"
                    && CheckGameEvent(gameEvent):
                    TryCollide(gameEvent.StringArg1, (ICollidable) gameEvent.From);
                    break;

                case var value when value == "SUBSCRIBE_COLLISION_EVENT"
                    && CheckGameEvent(gameEvent):
                    // Console.WriteLine("SUBSCRIBING TO");
                    Subscribe(gameEvent.StringArg1,(ICollidable) gameEvent.From);
                    break;

                case var value when value == "UNSUBSCRIBE_COLLISION_EVENT"
                    && CheckGameEvent(gameEvent):
                    UnSubscribe(gameEvent.StringArg1,(ICollidable) gameEvent.From);
                    break;
            }
        }

        public void Subscribe(string collisionIdentifier, ICollidable subscriber) {
            if (collisionEvents.ContainsKey(collisionIdentifier)) {
                collisionEvents[collisionIdentifier].Add(subscriber);

            } else {
                collisionEvents[collisionIdentifier] = new List<ICollidable> { subscriber };
            }
            foreach (string key in collisionEvents.Keys) {
                Console.WriteLine(key);
            }

        }

        public void UnSubscribe(string collisionIdentifier, ICollidable subscriber) {
            if (!collisionEvents.ContainsKey(collisionIdentifier)) {
                return;
            }
            // remove subscriber from list
            collisionEvents[collisionIdentifier].RemoveAll(item => ReferenceEquals(item, subscriber));
        }

        private bool CheckGameEvent(GameEvent gameEvent) {
            // Console.WriteLine("CHECKING GAME EVENT");
            return !(
                gameEvent.From is null
                || gameEvent.From is not ICollidable
                || gameEvent.StringArg1 is null);
        }

        private void TryCollide(string collisionIdentifier, ICollidable actor) {
            // Console.WriteLine("TRYING TO COLLIDE with " + collisionIdentifier + );
            // Console.WriteLine(collisionIdentifier);
            // foreach (ICollidable key in collisionEvents[collisionIdentifier]) {
            //     Console.WriteLine(key);
            // }

            if (!collisionEvents.ContainsKey(collisionIdentifier)) {
                // Console.WriteLine("NO COLLISION EVENT");
                return;
            }
            foreach (ICollidable subscriber in collisionEvents[collisionIdentifier]) {
                if (collisionIdentifier == "POWERUP") Console.WriteLine("POWERUP");

                    // Console.WriteLine("COLLIDING with " + actor + " " + subscriber);
                // Console.WriteLine("COLLIDING with " + collisionIdentifier + " " + subscriber);
                // }
                ComputeCollision(actor, subscriber);
            }
        }

        private void ComputeCollision(ICollidable actor, ICollidable other) {
            CollisionData data = CollisionDetection.Aabb(actor.Shape.AsDynamicShape(), other.Shape);
            if (data.Collision) {

                other.Collision(data, actor);
                actor.Collision(data, other);
            }
        }
    }
}






