using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    class Ufo
    {
        Game1 game;

        public BoundingRectangle bounds;

        Texture2D texture;

        /// <summary>
        /// Creates a Ufo
        /// </summary>
        /// <param name="game">The game this Ufo belongs to</param>
        public Ufo(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Ufo");
            bounds.Width = 50;
            bounds.Height = 50;
            bounds.X = game.GraphicsDevice.Viewport.Width / 2;
            bounds.Y = game.GraphicsDevice.Viewport.Height / 2 - bounds.Height / 2;
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                // move up
                bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }


            if (keyboardState.IsKeyDown(Keys.Down))
            {
                // move down
                bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                //move right
                bounds.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                //move left
                bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (bounds.Y < 0)
            {
                bounds.Y = 0;
            }
            if (bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }
            if (bounds.X < 0)
            {
                bounds.X = 0;
            }
            if(bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width)
            {
                bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}

