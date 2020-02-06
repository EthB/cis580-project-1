﻿using System;
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
        

        /// <summary>
        /// Creates an Asteroid
        /// </summary>
        /// <param name="game">The game this Asteroid belongs to</param>
        public Asteroid(Game1 game,Random random)
        {
            this.game = game;
            this.random = random;
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
            speed = random.NextDouble() * (.4 - .2) + .2;
            timeFlag = false;

            
        }

        public void Update(GameTime gameTime)
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
            spriteBatch.Draw(texture, bounds
                    , Color.White);
        }
    }
}
