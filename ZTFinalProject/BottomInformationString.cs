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
    public class BottomInformationString : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //Initialization and declaration
        protected SpriteBatch spriteBatch;
        protected SpriteFont spriteFont;
        protected Vector2 position;
        protected Color color;
        protected string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// constructor for intialization
        /// </summary>
        /// <param name="game">game</param>
        /// <param name="spriteBatch">spriteBatch</param>
        /// <param name="spriteFont">spriteFont</param>
        /// <param name="position">position</param>
        /// <param name="message">message</param>
        /// <param name="color">color</param>
        public BottomInformationString(Game game, SpriteBatch spriteBatch,
                SpriteFont spriteFont,
                Vector2 position,
                string message,
                Color color)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.position = position;
            this.message = message;
            this.color = color;
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
            base.Update(gameTime);
        }

        /// <summary>
        /// to draw bottom information string
        /// </summary>
        /// <param name="gameTime">gameTime</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, message, position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
