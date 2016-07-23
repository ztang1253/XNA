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
    public class StartScene : GameScene
    {
        private MenuComponent menu;
        private Game1 game;
        private Texture2D tex;
        private Random r;
        private int delayCount = 0;

        public MenuComponent Menu
        {
            get { return menu; }
            set { menu = value; }
        }
        private SpriteBatch spriteBatch;
        string[] menuItems = {"New Game",
                             "Load Game",
                             "Help",
                             "High Score",
                             "About",
                             "How to play",
                             "Save",
                             "Quit"};


        public StartScene(Game1 game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.game = game;
            menu = new MenuComponent(game, spriteBatch,
                game.Content.Load<SpriteFont>("Font/NormalFont"),
                game.Content.Load<SpriteFont>("Font/HighlightFont"),
                game.Content.Load<SpriteFont>("Font/MenuFont"),
                game.Content.Load<SpriteFont>("Font/MenuSelectedFont"),
                menuItems);
            tex = game.Content.Load<Texture2D>("Image/menu1");
            r = new Random();
            this.Components.Add(menu);
        }

        private void CreateBubble()
        {
            Texture2D texBubble = game.Content.Load<Texture2D>("Image/bubbleSmall1");
            Vector2 positionBuble = new Vector2(r.Next(0, (int)Shared.stage.X), r.Next((int)Shared.stage.Y - 250, (int)Shared.stage.Y));
            Vector2 speedBubble = new Vector2(0, r.Next(1, 2));
            MenuBubble eatableBubble =
                    new MenuBubble(game, spriteBatch, texBubble, positionBuble);
            this.Components.Add(eatableBubble);
        }

        public void showMessage()
        {
            SpriteFont sf1 = game.Content.Load<SpriteFont>("Font/HighLightFont");
            string middleString = "Cannot load game." + "\n" +
                     "No saved game progress.";
            Vector2 dimension11 = sf1.MeasureString(middleString);
            Vector2 posMiddle = new Vector2(Shared.stage.X / 2 - dimension11.X / 2,
                    (Shared.stage.Y / 2 - dimension11.Y / 2) / 2);
            BlinkingString middleLevelChangeString =
                    new BlinkingString(game, spriteBatch, sf1,
                            posMiddle, middleString, Color.SeaShell, 10, false);
            this.Components.Add(middleLevelChangeString);
        }

        public void showSaveMessage()
        {
            SpriteFont sf1 = game.Content.Load<SpriteFont>("Font/HighLightFont");
            string saveMessage = "";
            //if (Shared.isGameOver)
            //{
            //    if (Shared.isNewRecord)
            //    {
            //        saveMessage = "Game is over. " + "\n" + 
            //                "Your score will be saved" + "\n" +
            //                "for you have entered the top five.";
            //    }
            //    else
            //    {
            //        saveMessage = "Game is over. You cannot save.";
            //    }
            //}
            //else
            //{
            //    saveMessage = "Your game progress is saved. " + "\n" +
            //        "Your game progress is saved. " + "\n" +
            //        "Your game progress is saved. ";
            //}
            
            saveMessage = "Your game progress is saved. " + "\n" +
                    "Your game progress is saved. " + "\n" +
                    "Your game progress is saved. ";

            Vector2 dimension11 = sf1.MeasureString(saveMessage);
            Vector2 posMiddle = new Vector2(Shared.stage.X / 2 - dimension11.X / 2,
                    (Shared.stage.Y / 2 - dimension11.Y / 2) / 2);
            BlinkingString saveMessageString =
                    new BlinkingString(game, spriteBatch, sf1,
                            posMiddle, saveMessage, Color.SeaShell, 10, false);
            this.Components.Add(saveMessageString);
        }


        public void showCannotSaveMessage()
        {
            SpriteFont sf1 = game.Content.Load<SpriteFont>("Font/HighLightFont");
            string canNotSaveMessage = "You cannot save. " + "\n" + 
                    "There is no game progress. ";
            Vector2 dimension11 = sf1.MeasureString(canNotSaveMessage);
            Vector2 posMiddle = new Vector2(Shared.stage.X / 2 - dimension11.X / 2,
                    (Shared.stage.Y / 2 - dimension11.Y / 2) / 2);
            BlinkingString canNotSaveMessageString =
                    new BlinkingString(game, spriteBatch, sf1,
                            posMiddle, canNotSaveMessage, Color.SeaShell, 10, false);
            this.Components.Add(canNotSaveMessageString);
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
            delayCount++;
            if (delayCount >= 20)
            {
                CreateBubble();
                delayCount = 0;
            }

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
