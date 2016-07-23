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
    public class CollisionManager : Microsoft.Xna.Framework.GameComponent
    {
        private Game1 game;
        private Nemo nemo;
        private SoundEffect bubbleSound;
        private Song sharkSound;
        private List<AirBubbleBackground> bubbles;
        private List<Shark> sharks;
        private List<AnimatedShark> animatedSharks;
        private List<LongAnimatedShark> longAnimatedShark;
        private List<RealShark> realSharks;
        private SpriteBatch spriteBatch;
        private ActionScene actionScene;

        public CollisionManager(Game1 game,
                Nemo nemo, SpriteBatch spriteBatch, ActionScene actionScene)
            : base(game)
        {
            this.game = game;
            this.nemo = nemo;
            this.actionScene = actionScene;
            this.spriteBatch = spriteBatch;
            bubbleSound = game.Content.Load<SoundEffect>("Sound/ding");
            sharkSound = game.Content.Load<Song>("Sound/1");

            bubbles = new List<AirBubbleBackground>();
            sharks = new List<Shark>();
            animatedSharks = new List<AnimatedShark>();
            longAnimatedShark = new List<LongAnimatedShark>();
            realSharks = new List<RealShark>();
        }

        public void AddBubble(AirBubbleBackground bb)
        {
            bubbles.Add(bb);
        }

        public void RemoveBubble(AirBubbleBackground bb)
        {
            bubbles.Remove(bb);
        }


        public void AddRealShark(RealShark ask)
        {
            realSharks.Add(ask);
        }

        public void AddAnimatedShark(AnimatedShark ask)
        {
            animatedSharks.Add(ask);
        }

        public void AddLongAnimatedShark(LongAnimatedShark ask)
        {
            longAnimatedShark.Add(ask);
        }

        public void AddShark(Shark sk)
        {
            sharks.Add(sk);
        }

        public void RemoveShark(Shark sk)
        {
            sharks.Remove(sk);
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
            Rectangle nemoRect = nemo.getBounds();

            foreach (Shark sk in sharks)
            {
                if (nemoRect.Intersects(sk.getBounds()))
                {
                    actionScene.showGameOverMessage();                    
                    MediaPlayer.Play(sharkSound);
                    MediaPlayer.IsRepeating = false;
                    for (int i = 0; i < actionScene.Components.Count; i++)
                    {
                        actionScene.Components[i].Enabled = false;
                    }
                    Shared.isGameOver = true;
                    return;
                }
            }

            foreach (AnimatedShark ask in animatedSharks)
            {
                if (nemoRect.Intersects(ask.getBounds()))
                {
                    actionScene.showGameOverMessage();
                    MediaPlayer.Play(sharkSound);
                    MediaPlayer.IsRepeating = false;
                    for (int i = 0; i < actionScene.Components.Count; i++)
                    {
                        actionScene.Components[i].Enabled = false;
                    }
                    Shared.isGameOver = true;
                    return;
                }
            }

            foreach (LongAnimatedShark ask in longAnimatedShark)
            {
                if (nemoRect.Intersects(ask.getBounds()))
                {
                    actionScene.showGameOverMessage();
                    MediaPlayer.Play(sharkSound);
                    MediaPlayer.IsRepeating = false;
                    for (int i = 0; i < actionScene.Components.Count; i++)
                    {
                        actionScene.Components[i].Enabled = false;
                    }
                    Shared.isGameOver = true;
                    return;
                }
            }

            foreach (RealShark ask in realSharks)
            {
                if (nemoRect.Intersects(ask.getBounds()))
                {
                    actionScene.showGameOverMessage();
                    MediaPlayer.Play(sharkSound);
                    MediaPlayer.IsRepeating = false;
                    for (int i = 0; i < actionScene.Components.Count; i++)
                    {
                        actionScene.Components[i].Enabled = false;
                    }
                    Shared.isGameOver = true;
                    return;
                }
            }

            AirBubbleBackground hitBb = null;
            foreach (AirBubbleBackground ab in bubbles)
            {
                if (nemoRect.Intersects(ab.getBounds()))
                {
                    bubbleSound.Play();
                    game.Score++;
                    hitBb = ab;
                    break;
                }
            }

            if (hitBb != null)
            {
                hitBb.Visible = false;
                hitBb.Enabled = false;
                bubbles.Remove(hitBb);
            }

            base.Update(gameTime);
        }
    }
}