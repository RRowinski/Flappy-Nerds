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


namespace FlappyNerds
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GamePillar : Microsoft.Xna.Framework.DrawableGameComponent
    {

        //the pillar texture
        Texture2D pillar;
        Texture2D pillarDown;

        //The walking position of the first pillar
        Vector2 walkingPos;
        Vector2 walkingPos2;
        Vector2 velocity;

        int thisHeight;
        Random rnd = new Random();
        int gap = 150;
        int topPillarLength;

        public GamePillar(Game game) : base(game)
        {
        }

        //return x coord
        public int getX()
        {
            return (int)walkingPos.X;
        }
        
        //return Y coord
        public int getY()
        {
            return (int)walkingPos.Y;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            velocity = new Vector2(-60, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            thisHeight = rnd.Next(10, GraphicsDevice.Viewport.Height - (gap+10));

            pillar = Game.Content.Load<Texture2D>("TopPillar");
            pillarDown = Game.Content.Load<Texture2D>("BottomPillar");

            //The position of the pillar
            walkingPos = Vector2.Zero;
            walkingPos.X = GraphicsDevice.Viewport.Width;
            walkingPos.Y = GraphicsDevice.Viewport.Height - thisHeight;

            walkingPos2 = Vector2.Zero;
            walkingPos2.X = GraphicsDevice.Viewport.Width;
            walkingPos2.Y = 0;
            topPillarLength = GraphicsDevice.Viewport.Height - thisHeight - gap;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            walkingPos += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            walkingPos2 += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(pillarDown, walkingPos, new Rectangle(5, 0, 35, thisHeight), Color.White); // draw the pillar
                            //texture, position, color tint
            spriteBatch.Draw(pillar, walkingPos2, new Rectangle(5, pillar.Height - 1 - topPillarLength, 35, topPillarLength), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
