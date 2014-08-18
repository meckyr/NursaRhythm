using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NursaRhythm.Scenes
{
    class SplashScreen : GameScene
    {
        private GameSprite bg;

        private int alphaValue = 255;
        private int fadeIncrement = 20;
        private double fadeDelay = 2;

        public SplashScreen()
            : base("SplashScreen")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("splash\\background");
            AddSceneObject(bg);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            if (fadeDelay > 0)
                fadeDelay -= rendercontext.GameTime.ElapsedGameTime.TotalSeconds;

            if (fadeDelay <= 0)
            {
                alphaValue -= fadeIncrement;
                bg.Color = bg.Color * 0.8f;
            }

            if (alphaValue <= 0)
            {
                SceneManager.SetActiveScene("TitleScreen");
                SceneManager.ActiveScene.ResetScene();
                SceneManager.PlaySong(1);
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
    }
}
