using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace NursaRhythm.Scenes
{
    class TitleScreen : GameScene
    {
        private GameSprite bg;
        private GameAnimatedSprite logo, text;

        private const double Delay = 2.5f;
        private double delay = Delay;

        public TitleScreen()
            : base("TitleScreen")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("title\\background");
            AddSceneObject(bg);

            logo = new GameAnimatedSprite("title\\logo", 4, 120, new Point(650, 250), 2);
            logo.PlayAnimation(false);
            logo.Translate(75, 100);
            AddSceneObject(logo);

            text = new GameAnimatedSprite("title\\text", 4, 100, new Point(250, 100), 1);
            text.PlayAnimation(true);
            text.Translate(290, 350);
            AddSceneObject(text);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            if ((delay > 0) && (!logo.IsPlaying))
                delay -= rendercontext.GameTime.ElapsedGameTime.TotalSeconds;

            if (delay <= 0)
            {
                logo.PlayAnimation(false);
                delay = Delay;
            }

            if (rendercontext.TouchPanelState.Count > 0 && rendercontext.TouchPanelState[0].State == TouchLocationState.Released)
            {
                SceneManager.push.Play();
                SceneManager.SetActiveScene("MainMenu");
                SceneManager.ActiveScene.ResetScene();
            }

            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
        }

        public override void ResetScene()
        {
        }

        public override bool BackPressed()
        {
            SceneManager.push.Play();
            return true;
        }
    }
}
