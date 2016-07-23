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
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        public HelpScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.spriteBatch = spriteBatch;

            string title = "Introduction";
            SpriteFont sf1 = game.Content.Load<SpriteFont>("Font/BigFont");
            Vector2 dimension = sf1.MeasureString(title);
            Vector2 pos = new Vector2(Shared.stage.X / 2 - dimension.X / 2 + 165,
                    20);
            BottomInformationString titleString =
                    new BottomInformationString(game, spriteBatch, sf1, pos, title, Color.Gold);
            this.Components.Add(titleString);

            string playHint = "The game purpose is to get high score by moving" + "\n" +
                              "the red fish Nemo touching the bubbles in the" + "\n" +
                              "water. The more bubbles Nemo touches, the higher" + "\n" +
                              "score you will get at last. However, you must move" + "\n" +
                              "Nemo far awary from the sharks. Once Nemo is too" + "\n" +
                              "close to any shark, you lose Nemo and the game is" + "\n" +
                              "over. You will enter to next level everytime Nemo" + "\n" +
                              "touches over 20 bubbles. Meanwhile, the number of" + "\n" +
                              "the sharks will increase according to the level. " + "\n" +
                              "There is no limit in level. You can get to 1000 level" + "\n" +
                              "as long as you keep Nemo far away from those dangerous" + "\n" +
                              "sharks." + "\n" +
                              "\n" +
                              "Now have fun with your Nemo !";
                                
            SpriteFont spriteFont = game.Content.Load<SpriteFont>("Font/NormalFont");
            Vector2 dimension1 = spriteFont.MeasureString(playHint);
            Vector2 pos1 = new Vector2(Shared.stage.X / 2 - dimension1.X / 2 + 180,
                    80);
            BottomInformationString playHintSring =
                    new BottomInformationString(game, spriteBatch, spriteFont, pos1, playHint, Color.Gold);
            this.Components.Add(playHintSring);

            string middleString = "Please press ESC key to return to Main Menu.";
            SpriteFont hl = game.Content.Load<SpriteFont>("Font/HighLightFont");
            Vector2 dimension11 = hl.MeasureString(middleString);
            Vector2 posMiddle = new Vector2(Shared.stage.X / 2 - dimension11.X / 2,
                    Shared.stage.Y - 100);
            BlinkingString middleLevelChangeString =
                    new BlinkingString(game, spriteBatch, hl, 
                            posMiddle, middleString, Color.Gold, 100, true);
            this.Components.Add(middleLevelChangeString);

            tex = game.Content.Load<Texture2D>("Image/png000");
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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
