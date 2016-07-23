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
    public class Nemo : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 zeroSpeed = Vector2.Zero;
        private Texture2D previousImage;
        private Texture2D move;
        private Texture2D moveBackDown;
        private Texture2D moveBackUp;
        private Texture2D moveDown;
        private Texture2D moveUp;
        private Texture2D moveBack;
        private Game game;
        private ScrollingSeaBackground scrollingSeaBackground;



        public Nemo(Game game,
            SpriteBatch spriteBatch,
            ScrollingSeaBackground scrollingSeaBackground,
            Texture2D tex)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = new Vector2(Shared.stage.X / 2 - tex.Width / 2, Shared.stage.Y / 2 - tex.Height / 2);
            speed = new Vector2(4, 4);
            this.scrollingSeaBackground = scrollingSeaBackground;
            move = game.Content.Load<Texture2D>("Image/80Move");
            previousImage = move;
            moveBackDown = game.Content.Load<Texture2D>("Image/80MoveBackDown");
            moveBackUp = game.Content.Load<Texture2D>("Image/80MoveBackUp");
            moveDown = game.Content.Load<Texture2D>("Image/80MoveDown");
            moveUp = game.Content.Load<Texture2D>("Image/80MoveUp");
            moveBack = game.Content.Load<Texture2D>("Image/80MoveBack");
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
            KeyboardState ks = Keyboard.GetState();
            Keys[] pressedKeysArray = ks.GetPressedKeys();

            if (pressedKeysArray.Count() == 1 && (pressedKeysArray[0] == Keys.Right || pressedKeysArray[0] == Keys.D))
            {
                previousImage = move;
                tex = previousImage;
                position.X += speed.X;
                if (position.X > Shared.stage.X - tex.Width)
                {
                    position.X = Shared.stage.X - tex.Width;
                }
            }

            else if (pressedKeysArray.Count() == 1 && (pressedKeysArray[0] == Keys.Left || pressedKeysArray[0] == Keys.A))
            {
                previousImage = moveBack;
                tex = previousImage;
                position.X -= speed.X + scrollingSeaBackground.Speed.X;
                if (position.X < 0)
                {
                    position.X = 0;
                }
            }

            else if (pressedKeysArray.Count() == 1 && (pressedKeysArray[0] == Keys.Up || pressedKeysArray[0] == Keys.W))
            {
                if (previousImage == move)
                {
                    tex = moveUp;
                }
                else
                {
                    tex = moveBackUp;
                }

                position.Y -= speed.Y;
                if (position.Y < Shared.WAVE_HEIGHT)
                {
                    position.Y = 143;
                }
            }

            else if (pressedKeysArray.Count() == 1 && (pressedKeysArray[0] == Keys.Down || pressedKeysArray[0] == Keys.S))
            {
                if (previousImage == move)
                {
                    tex = moveDown;
                }
                else
                {
                    tex = moveBackDown;
                }

                position.Y += speed.Y;
                if (position.Y > Shared.stage.Y - tex.Height)
                {
                    position.Y = Shared.stage.Y - tex.Height;
                }
            }

            if (!(pressedKeysArray.Count() > 0) || pressedKeysArray.Count() > 1)
            {
                tex = previousImage;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, //长方形横坐标
                    (int)position.Y, //长方形纵坐标
                    tex.Width - 5, tex.Height - 5);
        }
    }
}














////if (ks.IsKeyDown(Keys.Right))
////{
////    scrollingSeaBackground.Speed = speed;                
////}

//if (ks.GetPressedKeys().Count() > 0 && 
//        ks.GetPressedKeys()[0] == Keys.Right)
//{
//    scrollingSeaBackground.Speed = speed;                
//}

//if (!(ks.GetPressedKeys().Count() > 0))
//{
//    scrollingSeaBackground.Speed = zeroSpeed;
//}

//if (ks.GetPressedKeys().Count() > 0 &&
//        ks.GetPressedKeys()[0] == Keys.Left)
//{
//    scrollingSeaBackground.Speed = -speed;
//}

////if (ks.IsKeyUp(Keys.Right))
////{
////    scrollingSeaBackground.Speed = zeroSpeed;
////}

////if (ks.IsKeyDown(Keys.Left))
////{
////    scrollingSeaBackground.Speed = -speed;                
////}

////if (ks.IsKeyUp(Keys.Left))
////{
////    scrollingSeaBackground.Speed = zeroSpeed;
////}