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
    public class Shark : Microsoft.Xna.Framework.DrawableGameComponent
    {
        const float LAST_TIME = 2.17f;
        const float X_MIN = 3.5f;
        const float X_MAX = 4.5f;
        const float Y_SPEED = 1;

        private Game1 game;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Random r;
        private float lastTime = 0;

        string[] sharksTexture = { "Image/shark1", 
                                     "Image/shark2", 
                                     "Image/shark3", 
                                     "Image/shark4",
                                     "Image/shark5",
                                     "Image/shark6",
                                     "Image/shark7"};

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed;

        public Shark(Game1 game, SpriteBatch spriteBatch, Vector2 position)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            r = new Random();
            int sharkIndex = game.Level - 1;
            if (sharkIndex < 0)
            {
                sharkIndex = 0;
            }
            else if (sharkIndex >= sharksTexture.Length)
            {
                sharkIndex = sharksTexture.Length - 1;
            }
            this.tex = game.Content.Load<Texture2D>(sharksTexture[r.Next(sharkIndex + 1)]);

            this.position = position;
            float x = (float)(r.NextDouble() * (X_MAX - X_MIN) + X_MIN);
            float y = r.Next(2) == 0 ? Y_SPEED : -Y_SPEED;
            this.speed = new Vector2(x, y);
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
            lastTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lastTime >= LAST_TIME)
            {
                lastTime = 0;
                speed = new Vector2(speed.X, -speed.Y);
            }

            if (position.Y <= Shared.WAVE_HEIGHT)
            {
                lastTime = 0;
                speed = new Vector2(speed.X, -Y_SPEED);
            }
            else if (position.Y >= Shared.stage.Y - 50)
            {
                lastTime = 0;
                speed = new Vector2(speed.X, Y_SPEED);
            }

            position.X -= speed.X;
            position.Y -= speed.Y;

            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X + 10,
                    (int)position.Y + 20,
                    tex.Width - 30, tex.Height - 30);
        }
    }
}
