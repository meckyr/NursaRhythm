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
        private GameButton play, setting, score, codex;
        private GameSprite bg;
        private GameAnimatedSprite setting_a, score_a, play_a, codex_a, ship1, ship2;
        private SpriteFont text;

        // pesawat
        private const double ShipAnimationDelay = 1;
        private double delay = ShipAnimationDelay;
        private int acceleration = 1;

        public MainMenu()
            : base("MainMenu")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("menu\\background");
            AddSceneObject(bg);

            play = new GameButton("menu\\playbutton", true, false, false);
            play.Translate(120, 100);
            play.CanDraw = false;
            play.OnClick += () =>
            {
                SceneManager.push.Play();
                SceneManager.SetActiveScene("LevelSelect");
                SceneManager.ActiveScene.ResetScene();

                play_a.CanDraw = true;
                play.CanDraw = false;
            };
            play.OnEnter += () =>
            {
                play_a.CanDraw = false;
                play.CanDraw = true;
            };
            play.OnLeave += () =>
            {
                play_a.CanDraw = true;
                play.CanDraw = false;
            };
            AddSceneObject(play);

            play_a = new GameAnimatedSprite("menu\\playbuttonanimated", 4, 150, new Point(250, 100), 1);
            play_a.Translate(120, 100);
            play_a.PlayAnimation(true);
            AddSceneObject(play_a);

            setting = new GameButton("menu\\settingbutton", true, false, false);
            setting.Translate(120, 210);
            setting.CanDraw = false;
            setting.OnClick += () =>
            {
                SceneManager.push.Play();
                SceneManager.SetActiveScene("OptionScene");
                SceneManager.ActiveScene.ResetScene();

                setting_a.CanDraw = true;
                setting.CanDraw = false;
            };
            setting.OnEnter += () =>
            {
                setting_a.CanDraw = false;
                setting.CanDraw = true;
            };
            setting.OnLeave += () =>
            {
                setting_a.CanDraw = true;
                setting.CanDraw = false;
            };
            AddSceneObject(setting);

            setting_a = new GameAnimatedSprite("menu\\settingbuttonanimated", 4, 150, new Point(250, 100), 1);
            setting_a.Translate(120, 210);
            setting_a.PlayAnimation(true);
            AddSceneObject(setting_a);

            score = new GameButton("menu\\scorebutton", true, false, false);
            score.Translate(120, 300);
            score.CanDraw = false;
            score.OnClick += () =>
            {
                SceneManager.whoosh.Play();

                score_a.CanDraw = true;
                score.CanDraw = false;
            };
            score.OnEnter += () =>
            {
                score_a.CanDraw = false;
                score.CanDraw = true;
            };
            score.OnLeave += () =>
            {
                score_a.CanDraw = true;
                score.CanDraw = false;
            };
            AddSceneObject(score);

            score_a = new GameAnimatedSprite("menu\\scorebuttonanimated", 4, 150, new Point(250, 100), 1);
            score_a.Translate(120, 300);
            score_a.PlayAnimation(true);
            AddSceneObject(score_a);

            codex = new GameButton("menu\\scorebutton", true, false, false);
            codex.Translate(120, 380);
            codex.CanDraw = false;
            codex.OnClick += () =>
            {
                SceneManager.whoosh.Play();

                codex_a.CanDraw = true;
                codex.CanDraw = false;
            };
            codex.OnEnter += () =>
            {
                codex_a.CanDraw = false;
                codex.CanDraw = true;
            };
            codex.OnLeave += () =>
            {
                codex_a.CanDraw = true;
                codex.CanDraw = false;
            };
            AddSceneObject(codex);

            codex_a = new GameAnimatedSprite("menu\\scorebuttonanimated", 4, 150, new Point(250, 100), 1);
            codex_a.Translate(120, 380);
            codex_a.PlayAnimation(true);
            AddSceneObject(codex_a);

            ship1 = new GameAnimatedSprite("menu\\ship1", 4, 180, new Point(300, 300), 1);
            ship1.Translate(450, 100);
            ship1.PlayAnimation(false);
            ship1.CanDraw = false;
            AddSceneObject(ship1);

            ship2 = new GameAnimatedSprite("menu\\ship2", 4, 80, new Point(300, 300), 1);
            ship2.Translate(450, 100);
            ship2.PlayAnimation(false);
            AddSceneObject(ship2);

            base.Initialize();
        }

        public void AnimateShip(RenderContext rendercontext)
        {
            if (delay > 0 && !ship1.IsPlaying && !ship2.IsPlaying)
                delay -= rendercontext.GameTime.ElapsedGameTime.TotalSeconds;

            if (delay <= 0)
            {
                if (ship1.CanDraw && !ship1.IsPlaying)
                {
                    ship1.CanDraw = false;
                    ship2.CanDraw = true;
                    ship2.PlayAnimation(false);
                }
                else if (ship2.CanDraw && !ship2.IsPlaying)
                {
                    ship1.CanDraw = true;
                    ship2.CanDraw = false;
                    ship1.PlayAnimation(false);
                }

                delay = ShipAnimationDelay;
            }

            var newPos = ship1.LocalPosition.Y + (0.5f * acceleration);

            if (newPos >= 120)
                acceleration = -1;

            if (newPos <= 80)
                acceleration = 1;

            ship1.Translate(ship1.LocalPosition.X, newPos);
            ship2.Translate(ship2.LocalPosition.X, newPos);
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);

            text = contentmanager.Load<SpriteFont>("font\\font");
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            AnimateShip(rendercontext);

            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);

            rendercontext.SpriteBatch.DrawString(text, "Music", new Vector2(1000, 100), Color.White);
            rendercontext.SpriteBatch.DrawString(text, "SFX", new Vector2(1000, 300), Color.White);
        }

        public override void ResetScene()
        {
            play.BackToNormal();
            setting.BackToNormal();
            score.BackToNormal();
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
