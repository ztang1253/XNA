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
    public class AnimatedShark : Microsoft.Xna.Framework.DrawableGameComponent
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
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = 0;

        private float delay = 2000f;
        private float elapsed;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed;

        public AnimatedShark(Game1 game, SpriteBatch spriteBatch, Vector2 position)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            r = new Random();
            this.tex = game.Content.Load<Texture2D>("Image/shark002");
            dimension = new Vector2(152, 110);
            this.position = position;
            float x = (float)(r.NextDouble() * (X_MAX - X_MIN) + X_MIN);
            float y = r.Next(2) == 0 ? Y_SPEED : -Y_SPEED;
            this.speed = new Vector2(x, y);
            createFrames();
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 4; i++)
            {
                int x = i * (int)dimension.X;
                int y = 0;
                Rectangle r = new Rectangle(x, y,
                        (int)dimension.X, (int)dimension.Y);
                frames.Add(r);
            }
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
                if (frameIndex >= 3)
                {
                    frameIndex = 0;
                }
                else
                {
                    frameIndex++;
                }
                elapsed = 0;
            }

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

            spriteBatch.Draw(tex, position, 
                    frames.ElementAt<Rectangle>(frameIndex), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X,
                    (int)position.Y,
                    tex.Width/4, tex.Height);
        }
    }
}
