using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace MonoGameWindowsStarter
{
    class Shield
    {
        Game1 game;

        public BoundingRectangle bounds;

        Texture2D texture;

        const int FRAME_WIDTH = 32;
        const int FRAME_HEIGHT = 32;
        const int ANIMATION_FRAME_RATE = 124;

        TimeSpan timer;
        TimeSpan timer2;
        State state;
        int frame;
        public bool used;

        SoundEffect shieldSound;

        /// <summary>
        /// Creates a Ufo
        /// </summary>
        /// <param name="game">The game this Ufo belongs to</param>
        public Shield(Game1 game)
        {
            this.game = game;
            timer = new TimeSpan(0);
            state = State.Idle;
            used = false;

        }

        public void LoadContent(ContentManager content, Ufo ufo)
        {
            texture = content.Load<Texture2D>("Shield");
            shieldSound = content.Load<SoundEffect>("shieldsound");
            bounds.Width = 65;
            bounds.Height = 70;
            bounds.X = ufo.bounds.X - 7;
            bounds.Y = ufo.bounds.Y - 10;
        }

        public void Update(GameTime gameTime, Ufo ufo)
        {
            var keyboardState = Keyboard.GetState();
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            bool keyflag = false;

            bounds.X = ufo.bounds.X- 7;
            bounds.Y = ufo.bounds.Y - 10;

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                // use shield
                if(!used)
                shieldSound.Play();
                used = true;
                timer2 = new TimeSpan(0);
                
            }

            if(used == true)
            {
                timer2 += gameTime.ElapsedGameTime;
            }


            timer += gameTime.ElapsedGameTime;
            if (timer2.TotalMilliseconds > ANIMATION_FRAME_RATE * 10)
            {
                used = false;
                
            }

            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            // Keep the frame within bounds (there are four frames)
            frame %= 4;

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (used == true) { 
            var source = new Rectangle(
                frame * FRAME_WIDTH,
                0,
                FRAME_WIDTH,
                FRAME_HEIGHT
                );
            spriteBatch.Draw(texture, bounds, source, Color.White);
            }
        }
    }
}

