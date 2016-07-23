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
    public class ScrollingSeaBackground : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        protected Texture2D tex;
        protected Vector2 position;
        private Vector2 speed;
        private Game1 game;

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private Vector2 position1, position2;


        public ScrollingSeaBackground(Game1 game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            Shared.WAVE_HEIGHT = 143;
            this.position2 = new Vector2(position1.X + tex.Width, position1.Y);
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
            if (position1.X > -tex.Width)
            {
                position1.X -= speed.X;
            }
            else
            {
                //CreateNewBubble();
                position1.X = position2.X - 11 + tex.Width;
            }

            if (position2.X > -tex.Width)
            {
                position2.X -= speed.X;
            }
            else
            {
                //CreateNewBubble();
                position2.X = position1.X - 11 + tex.Width;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position1, Color.White);
            spriteBatch.Draw(tex, position2, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //public void CreateNewBubble()
        //{
        //    Texture2D texBubble = game.Content.Load<Texture2D>("Image/b3");
        //    Vector2 positionBuble = new Vector2(Shared.stage.X, 683);
        //    AirBubbleBackground airBubbleBackground = new AirBubbleBackground(game, spriteBatch,
        //            texBubble, positionBuble, new Vector2(3, 0.3f));
        //    game.Components.Add(airBubbleBackground);
        //}
    }
}
