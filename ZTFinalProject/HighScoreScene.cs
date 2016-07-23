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
    public class HighScoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private SpriteFont sf1;
        private Game1 game;
        private BottomInformationString scoreString;

        public HighScoreScene(Game1 game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.game = game;
            sf1 = game.Content.Load<SpriteFont>("Font/HighLightFont");

            UpdateHighScore();
            string middleString = "Please press ESC key to return to Main Menu.";
            Vector2 dimension11 = sf1.MeasureString(middleString);
            Vector2 posMiddle = new Vector2(Shared.stage.X / 2 - dimension11.X / 2,
                    Shared.stage.Y - 100);
            BlinkingString middleLevelChangeString =
                    new BlinkingString(game, spriteBatch, sf1,
                            posMiddle, middleString, Color.Gold, 200, true);
            this.Components.Add(middleLevelChangeString);

            tex = game.Content.Load<Texture2D>("Image/png2");
            sf1 = game.Content.Load<SpriteFont>("Font/HighLightFont");
        }

        public void UpdateHighScore()
        {
            if (scoreString != null)
                this.Components.Remove(scoreString);

            List<int> highScore = game.HighestScore;
            string title = "\n" + "\n" + "High Score Top 5" + "\n" + "\n" +
                "        " + highScore[0] + "   " + "\n" + "\n" +
                    "        " + highScore[1] + "   " + "\n" + "\n" +
                    "        " + highScore[2] + "   " + "\n" + "\n" +
                    "        " + highScore[3] + "   " + "\n" + "\n" +
                    "        " + highScore[4] + "   ";

            Vector2 dimension = sf1.MeasureString(title);
            Vector2 pos = new Vector2(Shared.stage.X / 2 - dimension.X / 2 + 165,
                                      20);
            scoreString =
                new BottomInformationString(game, spriteBatch, sf1, pos, title, Color.Gold);
            this.Components.Add(scoreString);
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





























//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;


//namespace ZTFinalProject
//{
//    /// <summary>
//    /// This is a game component that implements IUpdateable.
//    /// </summary>
//    public class HighScoreScene : GameScene
//    {
//        private SpriteBatch spriteBatch;
//        private Texture2D tex;
//        private List<int> highScore;
//        private BottomInformationString titleString;

//        public HighScoreScene(Game1 game, SpriteBatch spriteBatch)
//            : base(game)
//        {
//            this.spriteBatch = spriteBatch;
//            highScore = game.HigestScore;

//            string title = "\n" + "\n" + "High Score Top 5" + "\n" + "\n" +
//                            "     ---" + highScore[0] + "---" + "\n" + "\n" +
//                            "     ---" + highScore[1] + "---" + "\n" + "\n" +
//                            "     ---" + highScore[2] + "---" + "\n" + "\n" +
//                            "     ---" + highScore[3] + "---" + "\n" + "\n" +
//                            "     ---" + highScore[4] + "---";
//            SpriteFont sf1 = game.Content.Load<SpriteFont>("Font/HighLightFont");
//            Vector2 dimension = sf1.MeasureString(title);
//            Vector2 pos = new Vector2(Shared.stage.X / 2 - dimension.X / 2 + 165,
//                    20);
//            titleString =
//                    new BottomInformationString(game, spriteBatch, sf1, pos, title, Color.Gold);
//            this.Components.Add(titleString);

//            string middleString = "Please press ESC key to return to Main Menu.";
//            Vector2 dimension11 = sf1.MeasureString(middleString);
//            Vector2 posMiddle = new Vector2(Shared.stage.X / 2 - dimension11.X / 2,
//                    Shared.stage.Y - 100);
//            BlinkingString middleLevelChangeString =
//                    new BlinkingString(game, spriteBatch, sf1,
//                            posMiddle, middleString, Color.Gold, 200, true);
//            this.Components.Add(middleLevelChangeString);

//            tex = game.Content.Load<Texture2D>("Image/PngNemo");
//        }

//        /// <summary>
//        /// Allows the game component to perform any initialization it needs to before starting
//        /// to run.  This is where it can query for any required services and load content.
//        /// </summary>
//        public override void Initialize()
//        {
//            // TODO: Add your initialization code here

//            base.Initialize();
//        }

//        /// <summary>
//        /// Allows the game component to update itself.
//        /// </summary>
//        /// <param name="gameTime">Provides a snapshot of timing values.</param>
//        public override void Update(GameTime gameTime)
//        {
//            // TODO: Add your update code here
//            titleString.Message = "\n" + "\n" + "High Score Top 5" + "\n" + "\n" +
//                            "     ---" + highScore[0] + "---" + "\n" + "\n" +
//                            "     ---" + highScore[1] + "---" + "\n" + "\n" +
//                            "     ---" + highScore[2] + "---" + "\n" + "\n" +
//                            "     ---" + highScore[3] + "---" + "\n" + "\n" +
//                            "     ---" + highScore[4] + "---";

//            base.Update(gameTime);
//        }

//        public override void Draw(GameTime gameTime)
//        {
//            spriteBatch.Begin();
//            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
//            spriteBatch.End();
//            base.Draw(gameTime);
//        }
//    }
//}
