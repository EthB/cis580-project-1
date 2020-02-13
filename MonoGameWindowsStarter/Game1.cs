using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont gameOver;
        SpriteFont scoreFont;
        Random random = new Random();
        Texture2D background;
        //Texture2D ball;
        //Vector2 ballPosition = Vector2.Zero;
        //Vector2 ballVelocity;

        //Paddle paddle;
        Shield shield;
        Ufo ufo;
        Asteroid[] asteroids;
        bool timeFlag = false;
        bool timeFlagScore = false;
        double time;
        int numberOfAsteroids;
        int score;

        bool lost = false;
        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        //sound
        
        SoundEffect asteroidExplode;
        SoundEffect playerHit;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //paddle = new Paddle(this);
            ufo = new Ufo(this);
            shield = new Shield(this);
            numberOfAsteroids = 3;
            asteroids = new Asteroid[] { new Asteroid(this, random), new Asteroid(this, random), new Asteroid(this, random) };
            score = 0;
            
            
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            //ballVelocity = new Vector2(
            //    (float)random.NextDouble(),
            //    (float)random.NextDouble()
            //);
            //ballVelocity.Normalize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            asteroidExplode = Content.Load<SoundEffect>("AsteroidExplodeSound");
            playerHit = Content.Load<SoundEffect>("PlayerHitSound");
            // TODO: use this.Content to load your game content here
            //ball = Content.Load<Texture2D>("ball");
            //paddle.LoadContent(Content);
            background = Content.Load<Texture2D>("space");
            ufo.LoadContent(Content);
            foreach (Asteroid a in asteroids)
            {
                a.LoadContent(Content);
            }
            //load font
            gameOver = Content.Load<SpriteFont>("font");
            scoreFont = Content.Load<SpriteFont>("score");
            shield.LoadContent(Content, ufo);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (newKeyboardState.IsKeyDown(Keys.Enter))
                Restart();

            if (newKeyboardState.IsKeyDown(Keys.Escape))
                Exit();
            if (lost == false)
            {
                
                //paddle.Update(gameTime);
                ufo.Update(gameTime);
                foreach (Asteroid a in asteroids)
                {
                    a.Update(gameTime);
                }
                shield.Update(gameTime, ufo);

                time = gameTime.TotalGameTime.TotalSeconds;
                if ((int)time != 0 && timeFlag == false)
                {
                    if ((int)time % 5 == 0)
                    {
                        Array.Resize(ref asteroids, asteroids.Length + 1);
                        Asteroid temp = new Asteroid(this, random);
                        temp.LoadContent(Content);
                        asteroids[numberOfAsteroids] = temp;
                        numberOfAsteroids++;
                        timeFlag = true;
                    }

                }
                if ((int)time != 0 && timeFlagScore == false)
                {
                    if ((int)time % 2 == 0)
                    {
                        score += 100;
                        timeFlagScore = true;
                    }

                }
                if ((int)time != 0)
                {
                    if ((int)time % 5 != 0)
                    {

                        timeFlag = false;
                    }
                    if((int)time % 2 != 0)
                    {
                        timeFlagScore = false;
                    }
                }



                //oldKeyboardState = newKeyboardState;
                foreach (Asteroid a in asteroids)
                {
                    if (Collisions.CollidesWith(ufo.bounds, a.bounds))
                    {
                        if (shield.used == true)
                        {
                            if (!a.destroyed)
                            {
                                asteroidExplode.Play();
                            }
                            a.Destroy();
                            
                        }
                        else if (!a.destroyed)
                        {
                            lost = true;
                            playerHit.Play();
                        }
                        else
                        {
                            
                        }
                    }
                }
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //spriteBatch.Draw(ball, 
            //    new Rectangle(
            //        (int)ballPosition.X, 
            //        (int)ballPosition.Y, 
            //        100, 
            //        100), 
            //        Color.White);
            //paddle.Draw(spriteBatch);
            spriteBatch.Draw(background, new Rectangle( 0 , 0 , this.GraphicsDevice.Viewport.Width , this.GraphicsDevice.Viewport.Height), Color.White);
            ufo.Draw(spriteBatch);
            shield.Draw(spriteBatch);
            foreach (Asteroid a in asteroids)
            {
                a.Draw(spriteBatch);
            }
            spriteBatch.DrawString(scoreFont, "Score: " + score.ToString(), new Vector2(10,10), Color.White);
            if (lost == true)
            {
                spriteBatch.DrawString(gameOver, "YOU LOST, press 'enter' to restart", new Vector2(this.GraphicsDevice.Viewport.Width/4, this.GraphicsDevice.Viewport.Height/2), Color.White);
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }

        protected void Restart()
        {
            numberOfAsteroids = 3;
            lost = false;
            ufo = new Ufo(this);
            shield = new Shield(this);
            asteroids = new Asteroid[] { new Asteroid(this, random), new Asteroid(this, random), new Asteroid(this, random) };
            score = 0;
            LoadContent();
        }
    }
}
