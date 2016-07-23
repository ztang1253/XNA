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
    public class ActionScene : GameScene
    {
        private Game1 game;
        private CollisionManager cm;
        private float bubbleWaitTime;
        private float sharkWaitTime;
        private SpriteBatch spriteBatch;
        private Random r;
        private float sharkInterval;
        private BottomInformationString levelString;
        private BlinkingString middleLevelChangeString;
        private int delayCount = 0;
        private int delayCount2 = 0;

        public ActionScene(Game1 game, SpriteBatch spriteBatch)
            : base(game)
        {
            Shared.isGameOver = false;
            this.game = game;
            this.spriteBatch = spriteBatch;
            r = new Random();

            Texture2D texSea = game.Content.Load<Texture2D>("Image/SeaBackground");
            Vector2 pos1 = new Vector2(0, Shared.stage.Y);
            ScrollingSeaBackground scrollingSeaBackground = new ScrollingSeaBackground(
                    game, spriteBatch, texSea, pos1, new Vector2(3, 0));
            this.Components.Add(scrollingSeaBackground);
           

            Texture2D tex = game.Content.Load<Texture2D>("Image/80Move");
            Nemo nemo = new Nemo(game, spriteBatch, scrollingSeaBackground, tex);
            this.Components.Add(nemo);

            bubbleWaitTime = 0;
            sharkWaitTime = 0;
            cm = new CollisionManager(game, nemo, spriteBatch, this);
            this.Components.Add(cm);

            spriteFont = game.Content.Load<SpriteFont>("Font/HighLightFont");
            strLevel = "Level 1 --- Score 0";
            Vector2 dimension = spriteFont.MeasureString(strLevel);
            Vector2 pos = new Vector2(Shared.stage.X / 2 - dimension.X / 2, Shared.WAVE_HEIGHT / 2);
            levelString = new BottomInformationString(game, spriteBatch, spriteFont, pos, strLevel, Color.LightPink);
            this.Components.Add(levelString);            
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

        public void showGameOverMessage()
        {
            string gameOverString = "Game Over";            
            SpriteFont sf1 = game.Content.Load<SpriteFont>("Font/BigFont");
            Vector2 dimension1 = sf1.MeasureString(gameOverString);
            Vector2 pos1 = new Vector2(Shared.stage.X / 2 - dimension1.X / 2, Shared.stage.Y / 2 - dimension1.Y / 2);
            BottomInformationString gameOver =
                    new BottomInformationString(game, spriteBatch, sf1, pos1, gameOverString, Color.Gold);
            this.Components.Add(gameOver);

            string MessageString = "Nemo is too close to a dangerous shark ...";
            Vector2 dimension2 = spriteFont.MeasureString(MessageString);
            Vector2 pos2 = new Vector2(Shared.stage.X / 2 - dimension2.X / 2 + 5, pos1.Y + 47);
            //Vector2 pos2 = new Vector2(Shared.stage.X / 2 - dimension2.X / 2 + 5, dimension2.Y);
            BottomInformationString nimoMessage =
                    new BottomInformationString(game, spriteBatch, spriteFont, pos2, MessageString, Color.LightYellow);
            this.Components.Add(nimoMessage);

            string helpString = "Press ESC to return to Main Menu.";
            Vector2 dimension3 = spriteFont.MeasureString(helpString);
            Vector2 pos3 = new Vector2(Shared.stage.X / 2 - dimension3.X / 2 + 5, Shared.stage.Y / 2 - dimension3.Y / 2 + dimension1.Y + 20);
            BottomInformationString helpMessage =
                    new BottomInformationString(game, spriteBatch, spriteFont, pos3, helpString, Color.Gold);
            this.Components.Add(helpMessage);
        }

        private void BeginPause(bool UserInitiated)
        {
            paused = true;
            pausedForGuide = !UserInitiated;
            //TODO: Pause audio playback
            //TODO: Pause controller vibration
        }

        private void EndPause()
        {
            //TODO: Resume audio
            //TODO: Resume controller vibration
            pausedForGuide = false;
            paused = false;
        }

        private bool paused = false;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;

        public void checkPauseKey(KeyboardState keyboardState)
        {
            bool pauseKeyDownThisFrame = keyboardState.IsKeyDown(Keys.Space);
            // If key was not down before, but is down now, we toggle the
            // pause setting
            if (!pauseKeyDown && pauseKeyDownThisFrame)
            {
                if (!paused)
                {
                    MediaPlayer.Pause();
                    BeginPause(true);
                }                    
                else
                {
                    EndPause();
                    MediaPlayer.Resume();
                }                    
            }
            pauseKeyDown = pauseKeyDownThisFrame;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!Shared.isGameOver)
            {
                KeyboardState ks = Keyboard.GetState();

                // Check to see if the user has paused or unpaused
                checkPauseKey(ks);

                if (!paused)
                {
                    levelString.Message = "Level " + game.Level + " --- Score " + game.Score;

                    // TODO: Add your update code here
                    bubbleWaitTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    sharkWaitTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    sharkInterval = (float)10 / game.Level;
                    if (bubbleWaitTime >= 1) // every 1s
                    {
                        CreateBubble();
                        CreateBubble();
                        CreateBubble();
                        CreateBubble();
                        CreateBubble();
                        bubbleWaitTime = 0;
                    }

                    if (sharkInterval >= 9)
                    {
                        if (sharkWaitTime >= 3)
                        {
                            int temp = r.Next(0, 4);
                            if (temp == 1)
                            {
                                CreateAnimatedShark();
                            }
                            else if(temp == 2)
                            {
                                CreateShark();
                            }
                            else if(temp == 3)
                            {
                                CreateRealShark();
                            }
                            else
                            {
                                CreateLongAnimatedShark();
                            }
                            sharkWaitTime = 0;
                        }
                    }

                    if (sharkWaitTime >= sharkInterval)
                    {
                        int temp = r.Next(0, 4);
                        if (temp == 1)
                        {
                            CreateAnimatedShark();
                        }
                        else if (temp == 2)
                        {
                            CreateShark();
                        }
                        else if (temp == 3)
                        {
                            CreateRealShark();
                        }
                        else
                        {
                            CreateLongAnimatedShark();
                        }
                        sharkWaitTime = 0;
                    }

                    delayCount++;
                    if (delayCount > 20)
                    {
                        CreateAnemone();
                        delayCount = 0;
                    }

                    delayCount2++;
                    if (delayCount2 > 2000)
                    {
                        CreateAnimatedCrab();
                        delayCount2 = 0;
                    }

                    base.Update(gameTime);
                }        
            }
        }

        private void CreateBubble()
        {
            Texture2D texBubble = game.Content.Load<Texture2D>("Image/bubbleSmall1");
            Vector2 positionBuble = new Vector2(r.Next(0, (int)Shared.stage.X), r.Next((int)Shared.stage.Y -250, (int)Shared.stage.Y));
            Vector2 speedBubble = new Vector2(1, r.Next(1, 3));
            AirBubbleBackground eatableBubble =
                    new AirBubbleBackground(game, spriteBatch, texBubble, positionBuble);
            this.Components.Add(eatableBubble);
            cm.AddBubble(eatableBubble);
        }

        private void CreateAnemone()
        {
            int y = r.Next((int)Shared.stage.Y - 170, (int)Shared.stage.Y);
            Vector2 position = new Vector2(Shared.stage.X + 100, y);
            Anemone anemone = new Anemone(game, spriteBatch, position, new Vector2(3, 0));
            this.Components.Add(anemone);
        }

        private void CreateAnimatedCrab()
        {
            int y = r.Next((int)Shared.stage.Y - 170, (int)Shared.stage.Y);
            Vector2 position = new Vector2(Shared.stage.X + 100, y);
            AnimatedCrab anemone = new AnimatedCrab(game, spriteBatch, position, new Vector2(1, 0));
            this.Components.Add(anemone);
        }

        private void CreateShark()
        {
            int y = r.Next(Shared.WAVE_HEIGHT, (int)Shared.stage.Y - 50);
            Vector2 position = new Vector2(Shared.stage.X + 100, y);

            Shark sk = new Shark(game, spriteBatch, position);
            this.Components.Add(sk);
            cm.AddShark(sk);
        }

        private void CreateAnimatedShark()
        {
            int y = r.Next(Shared.WAVE_HEIGHT, (int)Shared.stage.Y - 50);
            Vector2 position = new Vector2(Shared.stage.X + 100, y);

            AnimatedShark ask = new AnimatedShark(game, spriteBatch, position);
            this.Components.Add(ask);
            cm.AddAnimatedShark(ask);
        }

        private void CreateLongAnimatedShark()
        {
            int y = r.Next(Shared.WAVE_HEIGHT, (int)Shared.stage.Y - 50);
            Vector2 position = new Vector2(Shared.stage.X + 100, y);

            LongAnimatedShark ask = new LongAnimatedShark(game, spriteBatch, position);
            this.Components.Add(ask);
            cm.AddLongAnimatedShark(ask);
        }

        private void CreateRealShark()
        {
            int y = r.Next(Shared.WAVE_HEIGHT, (int)Shared.stage.Y - 50);
            Vector2 position = new Vector2(Shared.stage.X + 100, y);

            RealShark ask = new RealShark(game, spriteBatch, position);
            this.Components.Add(ask);
            cm.AddRealShark(ask);
        }

        public void ShowPassLevel(int newlevel)
        {
            string middleString = "Congratulations! You are now entering Level " + newlevel;
            Vector2 dimension = spriteFont.MeasureString(middleString);
            Vector2 posMiddle = new Vector2(Shared.stage.X / 2 - dimension.X / 2, Shared.stage.Y / 2 - dimension.Y / 2 - 250);
            middleLevelChangeString = 
                    new BlinkingString(game, spriteBatch, spriteFont, posMiddle, middleString, Color.Gold, 15, false);
            this.Components.Add(middleLevelChangeString);
        }

        private string strLevel;
        private SpriteFont spriteFont;

    }
}
