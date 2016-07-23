using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace ZTFinalProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const string DATA_FILE = "NemoSavedGame";
        const int MAX_RECORD = 5; // max 5 highest score records 

        //declare all scenes here.............
        private StartScene startScene;
        private HelpScene helpScene;
        private ActionScene actionScene;
        private HighScoreScene highScoreScene;
        private AboutScene aboutScene;
        private HowToPlayScene howToPlayScene;
        private bool escFromNewGame = true;

        // socre and level
        private int currentLevel;
        private int score;
        private List<int> highestScore;
        private bool canSave = false;

        private Song backgroundMusic;

        public List<int> HighestScore
        {
            get { return highestScore; }
            set { highestScore = value; }
        }
        private int savedLevel;
        private int savedScore;

        public int Level
        {
            get
            {
                return currentLevel;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                int level = 1 + score / 20;
                if (level > currentLevel)
                {
                    actionScene.ShowPassLevel(level);
                    currentLevel = level;
                }
            }
        }

        //public void UpdateHighScore()
        //{
        //    if (score <= highestScore[MAX_RECORD - 1])
        //        return;


        //    for (int num = 0; num < MAX_RECORD; num++)
        //    {
        //        if (score <= highestScore[num])
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            highestScore.Insert(num, score);
        //            Shared.isNewRecord = true;
        //            break;
        //        }
        //    }

        //    if (highestScore.Count > MAX_RECORD)
        //    {
        //        highestScore.RemoveRange(MAX_RECORD, highestScore.Count - MAX_RECORD);
        //    }

        //    highScoreScene.UpdateHighScore();
        //}

        public void UpdateHighScore()
        {
            if (score <= this.HighestScore[MAX_RECORD - 1])
                return;


            for (int num = 0; num < MAX_RECORD; num++)
            {
                if (score <= this.HighestScore[num])
                {
                    continue;
                }
                else
                {
                    this.HighestScore.Insert(num, score);
                    Shared.isNewRecord = true;
                    break;
                }
            }

            highestScore.RemoveAt(MAX_RECORD);
            highScoreScene.UpdateHighScore();
        }

        //----------------Scene declaration ends---------------------

        private void hideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                }
            }
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1100;
            graphics.PreferredBackBufferHeight = 798;

            currentLevel = 1;
            score = 0;
            savedScore = 0;
            savedLevel = 1;
            highestScore = new List<int>();
            InitializeFile();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            backgroundMusic = Content.Load<Song>("Sound/01-super-mario-bros");
            MediaPlayer.IsRepeating = true;

            LoadFromFile();

            //create all scenes and add to the Components list
            startScene = new StartScene(this, spriteBatch);
            Components.Add(startScene);

            helpScene = new HelpScene(this, spriteBatch);
            Components.Add(helpScene);

            actionScene = new ActionScene(this, spriteBatch);
            Components.Add(actionScene);

            highScoreScene = new HighScoreScene(this, spriteBatch);
            Components.Add(highScoreScene);

            aboutScene = new AboutScene(this, spriteBatch);
            Components.Add(aboutScene);

            howToPlayScene = new HowToPlayScene(this, spriteBatch);
            Components.Add(howToPlayScene);

            startScene.show();
            //this.Window.AllowUserResizing = true;
            //this.IsMouseVisible = true;
            //----------------creating scenes ends------------------------------
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter)) // new game
                {
                    escFromNewGame = true;
                    canSave = true;
                    Shared.isNewRecord = false;
                    hideAllScenes();
                    score = 0;
                    currentLevel = 1;
                    Components.Remove(actionScene);
                    actionScene = new ActionScene(this, spriteBatch);
                    Components.Add(actionScene);
                    actionScene.show();
                    MediaPlayer.Play(backgroundMusic);
                    MediaPlayer.IsRepeating = true;
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter)) // load old game
                {
                    if (!File.Exists(DATA_FILE))
                    {
                        startScene.showMessage();
                        return;
                    }
                    escFromNewGame = false;
                    canSave = true;
                    Shared.isNewRecord = false;
                    hideAllScenes();
                    LoadFromFile();
                    currentLevel = savedLevel;
                    score = savedScore;
                    Components.Remove(actionScene);
                    actionScene = new ActionScene(this, spriteBatch);
                    Components.Add(actionScene);
                    actionScene.show();
                    MediaPlayer.Play(backgroundMusic);
                    MediaPlayer.IsRepeating = true;
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    highScoreScene.show();
                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScene.show();
                }
                else if (selectedIndex == 5 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    howToPlayScene.show();
                }
                if (selectedIndex == 6 && ks.IsKeyDown(Keys.Enter))
                {
                    if (!canSave)
                    {
                        startScene.showCannotSaveMessage();
                        return;
                    }
                    SaveToFile();
                    startScene.showSaveMessage();
                }
                if (selectedIndex == 7 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (helpScene.Enabled || actionScene.Enabled || highScoreScene.Enabled ||
                    aboutScene.Enabled || howToPlayScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    if (actionScene.Enabled)
                    {

                        if (escFromNewGame)
                        {
                            // save score to file's high score only if score is higher than the lowest high score
                            // must not save level and current score to file
                            FromNewSaveToFile();
                        }
                        else
                        {
                            UpdateHighScore();
                            SaveToFile();
                        }
                    }
                    hideAllScenes();
                    MediaPlayer.Stop();
                    startScene.show();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        // currentLevel:current:score:1stScore,2ndScore.....
        private void SaveToFile()
        {
            FileStream fs = new FileStream(DATA_FILE, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            //int level2Save = Shared.isGameOver ? savedLevel : currentLevel;
            string content = currentLevel + ":" + score + ":";
            for (int num = 0; num < MAX_RECORD; num++)
            {
                //content += higestScore[num] + ",";
                content += this.HighestScore[num] + ",";
            }
            content = content.Substring(0, content.Length - 1); // remove ',' in last

            sw.Write(content);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        private void InitializeFile()
        {
            if (!File.Exists(DATA_FILE))
            {
                FileStream fs = new FileStream(DATA_FILE, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                string content = 1 + ":" + 0 + ":";
                List<int> temp = new List<int>();
                temp.Add(0);
                temp.Add(0);
                temp.Add(0);
                temp.Add(0);
                temp.Add(0);
                for (int num = 0; num < 5; num++)
                {
                    content += temp[num] + ",";
                }
                content = content.Substring(0, content.Length - 1); // remove ',' in last

                sw.Write(content);
                sw.Flush();
                sw.Close();
                fs.Close();
            }            
        }

        private void FromNewSaveToFile()
        {
            LoadFromFile();
            UpdateHighScore();
            FileStream fs = new FileStream(DATA_FILE, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            string content = savedLevel + ":" + savedScore + ":";
            for (int num = 0; num < MAX_RECORD; num++)
            {
                content += this.HighestScore[num] + ",";
            }
            content = content.Substring(0, content.Length - 1); // remove ',' in last

            sw.Write(content);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        private void LoadFromFile()
        {
            savedLevel = 1;
            if (!File.Exists(DATA_FILE))
            {
                highestScore.Clear();
                highestScore.Add(0);
                highestScore.Add(0);
                highestScore.Add(0);
                highestScore.Add(0);
                highestScore.Add(0);
                return;
            }

            StreamReader sr = new StreamReader(DATA_FILE, Encoding.Default);
            string line;
            string total = "";
            while ((line = sr.ReadLine()) != null)
            {
                total += line;
            }

            savedLevel = int.Parse(total.Split(':')[0]);
            savedScore = int.Parse(total.Split(':')[1]);
            total = total.Split(':')[2];
            string[] scores = total.Split(',');
            highestScore.Clear();
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i].Trim() != "")
                {
                    int scoreInFile = int.Parse(scores[i]);
                    highestScore.Add(scoreInFile);
                }
            }
            sr.Close();
        }
    }
}
