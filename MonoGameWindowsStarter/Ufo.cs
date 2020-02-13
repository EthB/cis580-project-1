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

    enum State
    {
        Idle = 0,
        East = 1,
        West = 2,
        North = 0,
        South = 0,
    }
    class Ufo
    {
        Game1 game;

        public BoundingRectangle bounds;

        Texture2D texture;

        const int FRAME_WIDTH = 1000;
        const int FRAME_HEIGHT = 771;
        const int ANIMATION_FRAME_RATE = 124;

        TimeSpan timer;
        State state;
        int frame;

        /// <summary>
        /// Creates a Ufo
        /// </summary>
        /// <param name="game">The game this Ufo belongs to</param>
        public Ufo(Game1 game)
        {
            this.game = game;
            timer = new TimeSpan(0);
            state = State.Idle;
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
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            bool keyflag = false;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                // move up
                bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                state = State.North;
                keyflag = true;
            }


            if (keyboardState.IsKeyDown(Keys.Down))
            {
                // move down
                bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                state = State.South;
                keyflag = true;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                //move right
                bounds.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                state = State.East;
                keyflag = true;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                //move left
                bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                state = State.West;
                keyflag = true;
            }

            if (keyflag == false)
            {
                state = State.Idle;
            }

            if (state != State.Idle) timer += gameTime.ElapsedGameTime;

            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            // Keep the frame within bounds (there are four frames)
            frame %= 4;

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
            var source = new Rectangle(
                (int)state % 4 * FRAME_WIDTH,
                0,
                FRAME_WIDTH,
                FRAME_HEIGHT
                );
            spriteBatch.Draw(texture, bounds, source, Color.White);
        }
    }
}

