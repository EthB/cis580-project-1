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
    class Asteroid
    {

        Game1 game;

        public BoundingRectangle bounds;

        Texture2D texture;

        Vector2 direction;
        //Vector2 position;
        double speed;
        double time;
        bool timeFlag;
        Random random;
        int spawn;

        TimeSpan timer;
        TimeSpan timer2;
        int frame;

        public bool destroyed;

        /// <summary>
        /// How quickly the animation should advance frames (1/8 second as milliseconds)
        /// </summary>
        const int ANIMATION_FRAME_RATE = 250;

        /// <summary>
        /// The width of the animation frames
        /// </summary>
        const int FRAME_WIDTH = 256;

        /// <summary>
        /// The hieght of the animation frames
        /// </summary>
        const int FRAME_HEIGHT = 256;


        /// <summary>
        /// Creates an Asteroid
        /// </summary>
        /// <param name="game">The game this Asteroid belongs to</param>
        public Asteroid(Game1 game,Random random)
        {
            this.game = game;
            this.random = random;
            timer = new TimeSpan(0);
            destroyed = false;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Asteroid");
            bounds.Width=bounds.Height = random.Next(50, 100);
            spawn = random.Next(0, 3);
            if (spawn == 0)
            {
                bounds.X = random.Next(0, game.GraphicsDevice.Viewport.Width);
                bounds.Y = 0;
            }else if(spawn == 1)
            {
                bounds.X = random.Next(0, game.GraphicsDevice.Viewport.Width);
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }
            else if (spawn == 2)
            {
                bounds.X = 0;
                bounds.Y = random.Next(0, game.GraphicsDevice.Viewport.Height) - bounds.Height;
            }
            else if (spawn == 3)
            {
                bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
                bounds.Y = random.Next(0, game.GraphicsDevice.Viewport.Height) - bounds.Height;
            }
            direction = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2) - new Vector2(bounds.X, bounds.Y)  ;
            direction.Normalize();
            //position = new Vector2(bounds.X, bounds.Y);
            speed = random.NextDouble() * (.3 - .2) + .2;
            timeFlag = false;



            


        }

        public void Destroy()
        {
            if (destroyed == false)
            {
                destroyed = true;
                timer2 = new TimeSpan(0);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (destroyed == false)
            {
                time = gameTime.TotalGameTime.TotalSeconds;
                if ((int)time != 0 && timeFlag == false)
                {
                    if ((int)time % 5 == 0)
                    {
                        speed += .05;
                        timeFlag = true;
                    }

                }
                if ((int)time != 0)
                {
                    if ((int)time % 5 != 0)
                    {

                        timeFlag = false;
                    }
                }
                //position =(-1) * direction* (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                bounds.X += (float)speed * (direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds).X;
                bounds.Y += (float)speed * (direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds).Y;
                //bounds.X += direction.X * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                //bounds.Y += direction.Y * (float)gameTime.ElapsedGameTime.TotalMilliseconds;


                if (bounds.Y < 0)
                {
                    Reset();
                }
                if (bounds.Y > game.GraphicsDevice.Viewport.Height)
                {
                    Reset();
                }
                if (bounds.X < 0)
                {
                    Reset();
                }
                if (bounds.X > game.GraphicsDevice.Viewport.Width)
                {
                    Reset();
                }
            }

            timer += gameTime.ElapsedGameTime;
            timer2 += gameTime.ElapsedGameTime;

            if (timer2.TotalMilliseconds > ANIMATION_FRAME_RATE * 2 && destroyed == true)
            {
                destroyed = false;
                timer = new TimeSpan(0);
                Reset();
            }
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE && destroyed == true)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            // Keep the frame within bounds (there are four frames)
            frame %= 4;
        }

        public void Reset()
        {
            spawn = random.Next(0, 3);
            if (spawn == 0)
            {
                bounds.X = random.Next(0, game.GraphicsDevice.Viewport.Width);
                bounds.Y = 0;
            }
            else if (spawn == 1)
            {
                bounds.X = random.Next(0, game.GraphicsDevice.Viewport.Width);
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }
            else if (spawn == 2)
            {
                bounds.X = 0;
                bounds.Y = random.Next(0, game.GraphicsDevice.Viewport.Height) - bounds.Height;
            }
            else if (spawn == 3)
            {
                bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
                bounds.Y = random.Next(0, game.GraphicsDevice.Viewport.Height) - bounds.Height;
            }
            direction = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2) - new Vector2(bounds.X, bounds.Y);
            direction.Normalize();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (destroyed == false)
            {
                var source = new Rectangle(
                0, // X value 
                0, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );
                spriteBatch.Draw(texture, bounds, source
                        , Color.White);
            }
            else
            {
                var source = new Rectangle(
                frame * FRAME_WIDTH, // X value 
                0, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );
                spriteBatch.Draw(texture, bounds, source, Color.White);
            }
        }
    }
}
