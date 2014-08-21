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
        private GameSprite light, logo;
        private GameAnimatedSprite scroll, scrollUp;
        private GameButton tap;
        private Background0 bg;

        public TitleScreen()
            : base("TitleScreen")
        {
        }

        public override void Initialize()
        {
            bg = new Background0();
            AddSceneObject(bg);

            light = new GameSprite("title\\lighting");
            AddSceneObject(light);

            scroll = new GameAnimatedSprite("title\\scroll", 6, 80, new Point(640, 384), 3);
            scroll.Scale(1.25f, 1.25f);
            scroll.PlayAnimation(true);
            AddSceneObject(scroll);

            scrollUp = new GameAnimatedSprite("title\\scrollup", 6, 80, new Point(640, 384), 3);
            scrollUp.Scale(1.25f, 1.25f);
            scrollUp.CanDraw = false;
            AddSceneObject(scrollUp);

            logo = new GameSprite("title\\logo");
            logo.Origin = new Vector2(275.5f, 74.5f);
            logo.Translate(400, 240);
            logo.Scale(0.8f, 0.8f);
            AddSceneObject(logo);

            tap = new GameButton("title\\tap", true, false, true);
            tap.Origin = new Vector2(127.5f, 32);
            tap.Translate(400, 400);
            tap.OnClick += () =>
            {
                SceneManager.whoosh.Play();
                bg.UpdateBackgroundSpeed(0);

                tap.CanDraw = false;
                scroll.CanDraw = false;
                logo.CanDraw = false;

                scrollUp.CanDraw = true;
                scrollUp.PlayAnimation(false);
            };
            AddSceneObject(tap);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            if (!scrollUp.IsPlaying)
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
