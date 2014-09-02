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
    class SplashScreen1 : GameScene
    {
        private GameSprite bg;

        private int alphaValue = 255;
        private int fadeIncrement = 20;
        private double fadeDelay = 2;

        public SplashScreen1()
            : base("SplashScreen1")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("splash\\logo");
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
                SceneManager.SetActiveScene("SplashScreen2");
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
    }
}
