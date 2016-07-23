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
    public class HowToPlayScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        public HowToPlayScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.spriteBatch = spriteBatch;

            string title = "How To Play";
            SpriteFont sf1 = game.Content.Load<SpriteFont>("Font/BigFont");
            Vector2 dimension = sf1.MeasureString(title);
            Vector2 pos = new Vector2(Shared.stage.X / 2 - dimension.X / 2 + 165, 
                    20);
            BottomInformationString titleString =
                    new BottomInformationString(game, spriteBatch, sf1, pos, title, Color.Gold);
            this.Components.Add(titleString);

            string playHint = "1, Use key W/S/A/D or arrow keys to move Nemo. " + "\n" +
                    " --Use W or Up arrow key to move up" + "\n" +
                    " --Use S or Down arrow key to move down" + "\n" +
                    " --Use D or Right arrow key to move forwards" + "\n" +
                    " --Use A or Left arrow key to move backwards" + "\n" +
                    "2, Use Space key to pause and continue the game when playing." + "\n" +
                    "3, Use ESC key to return to Main Menu." + "\n" +
                    "4, Use Enter key to enter each specific scene on Main Menu.";
            SpriteFont spriteFont = game.Content.Load<SpriteFont>("Font/NormalFont");
            Vector2 dimension1 = spriteFont.MeasureString(playHint);
            Vector2 pos1 = new Vector2(Shared.stage.X / 2 - dimension1.X / 2 + 180,
                    80);
            BottomInformationString playHintSring =
                    new BottomInformationString(game, spriteBatch, spriteFont, pos1, playHint, Color.Gold);
            this.Components.Add(playHintSring);

            string title2 = "Save and Load";
            Vector2 dimension2 = sf1.MeasureString(title);
            Vector2 pos2 = new Vector2(Shared.stage.X / 2 - dimension2.X / 2 - 25,
                    Shared.stage.Y / 2 - 100);
            BottomInformationString title2String =
                    new BottomInformationString(game, spriteBatch, sf1, pos2, title2, Color.Gold);
            this.Components.Add(title2String);

            string title2Description =
                    "1, If you want to Save your game progress (current level and high score), press ESC key when " + "\n" +
                    "you are playing. You will return back to Main Menu. Then select the Save menu. Then press " + "\n" +
                    "Enter key and your game progress will be saved." + "\n" +
                    "Warning:" + "\n" +
                    " --Every time you save a game progress will replace the last  game progress you saved previously." + "\n" +
                    " You can only have one Saved game progress. " + "\n" +
                    " --You cannot save the progress when Nemo is too close to a shark and then the game is over. You " + "\n" +
                    "could start a new game or load a saved game progress." + "\n" +
                    "2, If you want to Load your last game progress you saved press Enter key when you have" + "\n" +
                    "Load Game Menu selected on Main Menu.";
            Vector2 dimension3 = spriteFont.MeasureString(title2Description);
            Vector2 pos3 = new Vector2(Shared.stage.X / 2 - dimension3.X / 2,
                    pos2.Y + 60);
            BottomInformationString title2DescriptionString =
                    new BottomInformationString(game, spriteBatch, spriteFont, 
                            pos3, title2Description, Color.Gold);
            this.Components.Add(title2DescriptionString);


            string middleString = "Please press ESC key to return to Main Menu.";
            SpriteFont hl = game.Content.Load<SpriteFont>("Font/HighLightFont");
            Vector2 dimension11 = hl.MeasureString(middleString);
            Vector2 posMiddle = new Vector2(Shared.stage.X / 2 - dimension11.X / 2,
                    Shared.stage.Y - 70);
            BlinkingString middleLevelChangeString =
                    new BlinkingString(game, spriteBatch, hl, 
                            posMiddle, middleString, Color.Gold, 100, true);
            this.Components.Add(middleLevelChangeString);

            tex = game.Content.Load<Texture2D>("Image/PngNemo");
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
