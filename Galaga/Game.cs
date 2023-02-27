using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;

namespace Galaga
{
    public class Game : DIKUGame
    {
        private Player player;
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
        }
            // TODO: Set key event handler (inherited window field of DIKUGame class)
        

        //private void KeyHandler(KeyboardAction action, KeyboardKey key) {} // TODO: Outcomment

        public override void Render()
        {
            //TODO: Render Game Entities
            window.Clear();
            player.Render();


            // throw new System.NotImplementedException("Galaga game has nothing to render yet.");
        }

        public override void Update()
        {
            window.PollEvents();
            // throw new System.NotImplementedException("Galaga game has no entities to update yet.");
        }
    }
}
