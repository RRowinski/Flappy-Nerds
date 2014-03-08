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
        const float birdX = 30;
        float birdY;

<<<<<<< HEAD:FlappyNerds/FlappyNerds/GameFlow.cs
=======

        //the background texture
        Texture2D background;

        //the pillar X
        int[] pillar1 = new int[3];

        bool released;

>>>>>>> Sarah's Work:FlappyNerds/FlappyNerds/GameFlow.cs
        public GameFlow()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new KinectData();
            int birdY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;

            player.StartKinect();
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
            Components.Add(new GamePillar(this));

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

<<<<<<< HEAD:FlappyNerds/FlappyNerds/GameFlow.cs
            bird = Content.Load<Texture2D>("initial-sprite");
=======
            //load in the background image
            background = Content.Load<Texture2D>("image");
            released = false;

            Services.AddService(typeof(SpriteBatch), spriteBatch);

>>>>>>> Sarah's Work:FlappyNerds/FlappyNerds/GameFlow.cs
            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            
            // TODO: Add your update logic here
<<<<<<< HEAD:FlappyNerds/FlappyNerds/GameFlow.cs
            birdY += player.GetLeftHandY() * 10;
            Console.WriteLine("Game flow: " + birdY);
=======
            if (((int)gameTime.TotalGameTime.TotalSeconds % 10 == 8) && (released == false))
            {
                GamePillar newGP = new GamePillar(this);
                Components.Add(newGP);

                released = true;
            }
            if ((int)gameTime.TotalGameTime.TotalSeconds % 10 == 0)
            {
                //Components.Add(new GamePillar(this));
                released = false;
            }
>>>>>>> Sarah's Work:FlappyNerds/FlappyNerds/GameFlow.cs

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
<<<<<<< HEAD:FlappyNerds/FlappyNerds/GameFlow.cs
            spriteBatch.Begin();
            spriteBatch.Draw(bird, new Vector2(birdX,birdY),
                            null, Color.White,0.3f,
                            new Vector2(bird.Width/2,bird.Height/2), 
                            0.1f,SpriteEffects.None,1.0f);
            spriteBatch.End();
=======

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.End();

>>>>>>> Sarah's Work:FlappyNerds/FlappyNerds/GameFlow.cs
            base.Draw(gameTime);
        }
        /*
        protected void Exit()
        {
            player.StopKinect();
        }*/
    }
}
