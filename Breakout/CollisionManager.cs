using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;

namespace Breakout {
    /// <summary>
    /// The CollisionManager class handles collision events and manages collision subscriptions.
    /// </summary>
    public class CollisionManager : IGameEventProcessor {
        private Dictionary<string, List<ICollidable>> collisionEvents;

        /// <summary>
        /// Initializes a new instance of the CollisionManager class.
        /// </summary>
        public CollisionManager() {
            collisionEvents = new Dictionary<string, List<ICollidable>>();
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        }

        /// <summary>
        /// Processes the game event.
        /// </summary>
        /// <param name="gameEvent">The game event to process.</param>
        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.Message) {
                case var value when value == "CHECK_COLLISION_EVENT"
                    && CheckGameEvent(gameEvent):
                    TryCollide(gameEvent.StringArg1, (ICollidable)gameEvent.From);
                    break;

                case var value when value == "SUBSCRIBE_COLLISION_EVENT"
                    && CheckGameEvent(gameEvent):
                    Subscribe(gameEvent.StringArg1, (ICollidable)gameEvent.From);
                    break;

                case var value when value == "UNSUBSCRIBE_COLLISION_EVENT"
                    && CheckGameEvent(gameEvent):
                    UnSubscribe(gameEvent.StringArg1, (ICollidable)gameEvent.From);
                    break;

                case "RESTART_LEVEL":
                    collisionEvents.Clear();
                    break;
            }
        }

        /// <summary>
        /// Subscribes an ICollidable object to a collision event.
        /// </summary>
        /// <param name="collisionIdentifier">The collision identifier.</param>
        /// <param name="subscriber">The ICollidable object to subscribe.</param>
        public void Subscribe(string collisionIdentifier, ICollidable subscriber) {
            if (collisionEvents.ContainsKey(collisionIdentifier)) {
                collisionEvents[collisionIdentifier].Add(subscriber);
            }
            else {
                collisionEvents[collisionIdentifier] = new List<ICollidable> { subscriber };
            }
        }

        /// <summary>
        /// Unsubscribes an ICollidable object from a collision event.
        /// </summary>
        /// <param name="collisionIdentifier">The collision identifier.</param>
        /// <param name="subscriber">The ICollidable object to unsubscribe.</param>
        public void UnSubscribe(string collisionIdentifier, ICollidable subscriber) {
            if (!collisionEvents.ContainsKey(collisionIdentifier)) {
                return;
            }

            // Remove subscriber from the list
            collisionEvents[collisionIdentifier].RemoveAll(item => ReferenceEquals(item, subscriber));
        }

        /// <summary>
        /// Checks if the game event is valid for collision processing.
        /// </summary>
        /// <param name="gameEvent">The game event to check.</param>
        /// <returns>True if the game event is valid; otherwise, false.</returns>
        private bool CheckGameEvent(GameEvent gameEvent) {
            return !(gameEvent.From is null
                || gameEvent.From is not ICollidable
                || gameEvent.StringArg1 is null);
        }

        /// <summary>
        /// Attempts to collide an ICollidable object with all subscribers of a collision event.
        /// </summary>
        /// <param name="collisionIdentifier">The collision identifier.</param>
        /// <param name="actor">The ICollidable object to collide.</param>
        private void TryCollide(string collisionIdentifier, ICollidable actor) {

            if (!collisionEvents.ContainsKey(collisionIdentifier)) {
                return;
            }

            foreach (ICollidable subscriber in collisionEvents[collisionIdentifier]) {
                ComputeCollision(actor, subscriber);
            }
        }

        /// <summary>
        /// Computes the collision between two ICollidable objects.
        /// </summary>
        /// <param name="actor">The first ICollidable object.</param>
        /// <param name="other">The second ICollidable object.</param>
        private void ComputeCollision(ICollidable actor, ICollidable other) {
            if (actor is PowerUp && other is Player) {
                CollisionData data = CollisionDetection.Aabb(actor.Shape.AsDynamicShape(), other.Shape);
                if (data.Collision && !((PowerUp)actor).Activated && data.CollisionDir == CollisionDirection.CollisionDirDown) {
                    other.Collision(data, actor);
                    actor.Collision(data, other);
                }
            } else if (actor is Hazard && other is Player) {
                CollisionData data = CollisionDetection.Aabb(actor.Shape.AsDynamicShape(), other.Shape);
                if (data.Collision && !((Hazard)actor).Activated && data.CollisionDir == CollisionDirection.CollisionDirDown) {
                    other.Collision(data, actor);
                    actor.Collision(data, other);
                }
            } else {
                CollisionData data = CollisionDetection.Aabb(actor.Shape.AsDynamicShape(), other.Shape);
                if (data.Collision) {
                    other.Collision(data, actor);
                    actor.Collision(data, other);
                }
            }
        }
    }
}
