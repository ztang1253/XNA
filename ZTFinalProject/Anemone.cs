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
    public class Anemone : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game1 game;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Random r;

        string[] anemoneTexture = { "Image/b1", "Image/b2", "Image/b3", "Image/b4", "Image/b5", "Image/b6",
                                     "Image/b7", "Image/b8", "Image/b9", "Image/b10", "Image/b11", "Image/b12",
                                  "Image/pngg1", "Image/pngg3","Image/pngg4","Image/pngg5",
                                  "Image/pngg6", "Image/pngg7","Image/pngg8","Image/pngg9","Image/pngg10",
                                  "Image/pngg11", "Image/pngg12","Image/pngg13","Image/b1", "Image/b2", "Image/b3", "Image/b4", "Image/b5", "Image/b6",
                                     "Image/b7", "Image/b8", "Image/b9", "Image/b10", "Image/b11", "Image/b12",
                                  "Image/pngg14","Image/pngg15",
                                  "Image/pngg16", "Image/pngg17","Image/pngg18","Image/pngg19","Image/pngg20",
                                  "Image/pngg21", "Image/pngg24","Image/pngg25","Image/b12", "Image/b9", "Image/b2", "Image/b3",
                                   "Image/pngg27","Image/pngg28","Image/pngg29", "Image/b8", "Image/b9",                                  
                                  "Image/b1", "Image/b2", "Image/b3", "Image/b4", "Image/b5", "Image/b6",
                                     "Image/b7", "Image/b8", "Image/b9", "Image/b10", "Image/b11", "Image/b12"
                                  };

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed;

        public Anemone(Game1 game, SpriteBatch spriteBatch, Vector2 position, Vector2 speed)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            r = new Random();
            this.tex = game.Content.Load<Texture2D>(anemoneTexture[r.Next(anemoneTexture.Count())]);
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
            position -= speed;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
