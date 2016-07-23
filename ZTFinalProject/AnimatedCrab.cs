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


namespace ZTFinalProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class AnimatedCrab : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game1 game;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;
        private int frameIndex = 0;

        private float delay = 100f;
        private float elapsed;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed;
        private List<Texture2D> longShark;

        private string[] sharksTexture = { "Image/oie_transparent", 
                                     "Image/oie_transparent (1)", 
                                     "Image/oie_transparent (2)", 
                                     "Image/oie_transparent (3)",
                                     "Image/oie_transparent (4)",
                                     "Image/oie_transparent (5)"};

        public AnimatedCrab(Game1 game, SpriteBatch spriteBatch, Vector2 position, Vector2 speed)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            longShark = new List<Texture2D>();
            for (int i = 0; i < sharksTexture.Length; i++)
            {
                longShark.Add(game.Content.Load<Texture2D>(sharksTexture[i]));
            }
            this.tex = game.Content.Load<Texture2D>(sharksTexture[0]);
            this.position = position;
            this.speed = speed;
        }
        
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frameIndex > 4)
                {
                    frameIndex = 0;
                }
                else
                {
                    frameIndex++;
                }
                elapsed = 0;
            }
            position -= speed;

            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(longShark[frameIndex], position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
