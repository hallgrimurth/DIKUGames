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
                case var value when value == "CHECK_COLLISION_EVENT"
                    && CheckGameEvent(gameEvent):
                    TryCollide(gameEvent.StringArg1, (ICollidable) gameEvent.From);
                    break;

                case var value when value == "SUBSCRIBE_COLLISION_EVENT"
                    && CheckGameEvent(gameEvent):
                    Subscribe(gameEvent.StringArg1,(ICollidable) gameEvent.From);
                    break;

                case var value when value == "UNSUBSCRIBE_COLLISION_EVENT"
                    && CheckGameEvent(gameEvent):
                    Console.WriteLine("CollisionManager: Unsubscribing from collision event");
                    UnSubscribe(gameEvent.StringArg1,(ICollidable) gameEvent.From);
                    break;
                case "RESTART_LEVEL":
                    Console.WriteLine("CollisionManager: Restarting level" + collisionEvents.Count);
                    collisionEvents.Clear();
                    break;
            }
        }

        public void Subscribe(string collisionIdentifier, ICollidable subscriber) {
            if (collisionEvents.ContainsKey(collisionIdentifier)) {
                collisionEvents[collisionIdentifier].Add(subscriber);

            } else {
                collisionEvents[collisionIdentifier] = new List<ICollidable> { subscriber };
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
            return !(
                gameEvent.From is null
                || gameEvent.From is not ICollidable
                || gameEvent.StringArg1 is null);
        }

        private void TryCollide(string collisionIdentifier, ICollidable actor) {


            if (!collisionEvents.ContainsKey(collisionIdentifier)) {
                return;
            }
            foreach (ICollidable subscriber in collisionEvents[collisionIdentifier]) {

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






