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
    public class AirBubbleBackground : Microsoft.Xna.Framework.DrawableGameComponent
    {
        const float X_MIN = 2;
        const float X_MAX = 3;
        private Game1 game;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed;
        private float scaleFactor = 0.3f; //��ʼ�Ŵ�ϵ��=ԭʼ��С
        private float scaleChange = 0.0003f; //0.0003f; //�Ŵ�ϵ��
        private const float MAX_SCALE = 3f; //�Ŵ���
        private const float MIN_SCALE = 0.1f; //��С����


        public AirBubbleBackground(Game1 game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            Random r = new Random();
            float x = (float)(r.NextDouble() * (X_MAX - X_MIN) + X_MIN);
            x = r.Next(2) == 0 ? x : -x;
            x += 3;
            if (x > 5)
            {
                x = 4;
            }
            this.speed = new Vector2(x, 0.3f);
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
            scaleFactor += scaleChange;
            position -= speed;
            if (position.Y < Shared.WAVE_HEIGHT)
            {
                this.Visible = false;
                this.Enabled = false;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, null, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, //�����κ�����
                    (int)position.Y, //������������
                    tex.Width, tex.Height);
        }
    }
}
