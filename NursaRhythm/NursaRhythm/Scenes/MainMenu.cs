using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm.Scenes
{
    class MainMenu : GameScene
    {
        private GameButton play, setting, codex;
        private GameSprite bg, scroll;
        private GameAnimatedSprite decor1, decor2;

        private float alphaRate = 1.5f;

        public MainMenu()
            : base("MainMenu")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("menu\\background");
            AddSceneObject(bg);

            scroll = new GameSprite("menu\\scroll");
            AddSceneObject(scroll);

            play = new GameButton("menu\\explore", true, false, true);
            play.Color = Color.White * 0.1f;
            play.Origin = new Vector2(216.5f, 55);
            play.Translate(400, 130);
            play.OnClick += () =>
            {
                SceneManager.push.Play();
                SceneManager.SetActiveScene("LevelSelect");
                SceneManager.ActiveScene.ResetScene();
            };
            AddSceneObject(play);

            setting = new GameButton("menu\\settings", true, false, true);
            setting.Color = Color.White * 0.1f;
            setting.Origin = new Vector2(148, 37.5f);
            setting.Translate(400, 235);
            setting.OnClick += () =>
            {
                SceneManager.push.Play();
                SceneManager.SetActiveScene("OptionScene");
                SceneManager.ActiveScene.ResetScene();
            };
            AddSceneObject(setting);

            codex = new GameButton("menu\\archive", true, false, true);
            codex.Color = Color.White * 0.1f;
            codex.Origin = new Vector2(148, 46);
            codex.Translate(400, 350);
            codex.OnClick += () =>
            {
                SceneManager.push.Play();
                SceneManager.SetActiveScene("Archive");
                SceneManager.ActiveScene.ResetScene();
            };
            AddSceneObject(codex);

            decor1 = new GameAnimatedSprite("menu\\decor", 9, 80, new Point(441, 39), 1);
            decor1.Color = Color.White * 0.1f;
            decor1.Origin = new Vector2(220.5f, 19.5f);
            decor1.Translate(400, 50);
            decor1.PlayAnimation(true);
            AddSceneObject(decor1);

            decor2 = new GameAnimatedSprite("menu\\decor", 9, 80, new Point(441, 39), 1);
            decor2.Color = Color.White * 0.1f;
            decor2.Origin = new Vector2(220.5f, 19.5f);
            decor2.Translate(400, 430);
            decor2.PlayAnimation(true);
            AddSceneObject(decor2);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            play.Color *= alphaRate;
            setting.Color *= alphaRate;
            codex.Color *= alphaRate;
            decor1.Color *= alphaRate;
            decor2.Color *= alphaRate;

            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
        }

        public override void ResetScene()
        {
            play.BackToNormal();
            setting.BackToNormal();
            codex.BackToNormal();

            CameraManager.getInstance().camera.Focus = null;
        }

        public override bool BackPressed()
        {
            SceneManager.push.Play();
            return true;
        }
    }
}
