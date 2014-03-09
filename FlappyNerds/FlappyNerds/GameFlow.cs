using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;

namespace FlappyNerds
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameFlow : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D bird;
        KinectData player;
        bool running = true;
        bool canFlap = false;
        float birdY;
        float birdYVol;
        const float birdX = 30;
        const float flapVol = -5;
        const float gravity = 0.2F;

        //the background texture
        Texture2D background;

        SpriteFont font;

        //the pillar X
        double[] pillarX = new double[3];
        
        //stuff
        int gap = 150;
        int score;
        int velocity = -60;

        //the pillar Y
        double[] pillarY = new double[3];

        bool released;

        public GameFlow()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new KinectData();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            birdY = GraphicsDevice.Viewport.Height / 2;

            player.StartKinect();
            // TODO: Add your initialization logic here
            GamePillar newGP = new GamePillar(this);
            birdYVol = 0;
            
            pillarX[0] = GraphicsDevice.Viewport.Width;
            pillarX[1] = GraphicsDevice.Viewport.Width;
            pillarX[2] = GraphicsDevice.Viewport.Width;
            
            pillarY[0] = GraphicsDevice.Viewport.Height;
            pillarY[1] = GraphicsDevice.Viewport.Height;
            pillarY[2] = GraphicsDevice.Viewport.Height;

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

            bird = Content.Load<Texture2D>("cuteBird");
            background = Content.Load<Texture2D>("Background");
            released = false;

            Services.AddService(typeof(SpriteBatch), spriteBatch);
            font =  Content.Load<SpriteFont>("GameFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            if (player.IsPlayerFound() && running)
            {
            pillarX[0] += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            pillarX[1] += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            pillarX[2] += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (!canFlap && player.GetLeftHandY() > player.GetHeadY() && player.GetRightHandY() > player.GetHeadY())
                canFlap = true;

            if (canFlap && player.GetLeftHandY() < player.GetHipY() && player.GetRightHandY() < player.GetHipY())
            {
                birdYVol = flapVol;
                canFlap = false;
            }
            
            birdY += birdYVol;
            birdYVol += gravity;

            if (birdY < 0)
            {
                birdY = 0;
                birdYVol = 0;
            }
            else if (birdY > GraphicsDevice.Viewport.Height)
            {
                birdY = GraphicsDevice.Viewport.Height;
                birdYVol = 0;
            }
            
            if ((birdX > pillarX[0]))
                score++;

            if ((birdX > pillarX[0]) && (birdX < pillarX[0] + 35) && ((birdY > pillarY[0]) || (birdY < pillarY[0] - gap)))
            {
                //collision
                running = false;
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }

            
                if (((int)gameTime.TotalGameTime.TotalSeconds % 5 == 4) && (released == false))
                {
                    GamePillar newGP = new GamePillar(this);
                    Components.Add(newGP);

                    pillarX[0] = pillarX[1]; pillarX[1] = pillarX[2]; pillarX[2] = newGP.getX();
                    pillarY[0] = pillarY[1]; pillarY[1] = pillarY[2]; pillarY[2] = newGP.getY();


                    released = true;
                }

                if ((int)gameTime.TotalGameTime.TotalSeconds % 5 == 0)
                    released = false;
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

            spriteBatch.Begin();
            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.Draw(bird, new Vector2(birdX, birdY),
                            null, Color.White, 0.3f,
                            new Vector2(bird.Width / 2, bird.Height / 2),
                            0.5f, SpriteEffects.None, 1.0f);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score: " + score, Vector2.Zero, Color.White);
            spriteBatch.End();

            if (!running)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Game over!", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
