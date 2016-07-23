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
    public class MenuBubble : Microsoft.Xna.Framework.DrawableGameComponent
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
        private float scaleChange = 0.0005f; //�Ŵ�ϵ��
        private const float MAX_SCALE = 3f; //�Ŵ���
        private const float MIN_SCALE = 0.1f; //��С����


        public MenuBubble(Game1 game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            Random r = new Random();
            int x = r.Next(0, 3);
            if (x == 0)
            {
                x = -x;
            }
            else if (x == 1)
            {
                x = 0;
            }
            else if (x == 2)
            {
                
            }

            this.speed = new Vector2((float)(-x*0.1), 0.3f);
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
            //if (position.Y < Shared.WAVE_HEIGHT)
            //{
            //    this.Visible = false;
            //    this.Enabled = false;
            //}
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
