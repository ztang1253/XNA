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
    public class BlinkingString : BottomInformationString
    {
        //Initialization and declaration
        protected int delay;
        protected bool flag;
        protected int delayCounter;
        private int timeCounter;
        private bool isLongBlingking;

        public BlinkingString(Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            Vector2 position,
            string message,
            Color color,
            int delay,
            bool isLongBlingking)
            : base(game, spriteBatch, spriteFont, position, message, color)
        {
            // TODO: Construct any child components here
            this.delay = delay;
            this.isLongBlingking = isLongBlingking;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                flag = !flag;
                delayCounter = 0;
            }

            if (!isLongBlingking)
            {
                timeCounter++;
                if (timeCounter > 100)
                {
                    this.Visible = false;
                    this.Enabled = false;
                }
            }            
        }

        /// <summary>
        /// to draw bottom middle blinking string
        /// </summary>
        /// <param name="gameTime">gameTime</param>
        public override void Draw(GameTime gameTime)
        {
            if (flag)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(spriteFont, message, position, color);
                spriteBatch.End();
            }
        }
    }
}
