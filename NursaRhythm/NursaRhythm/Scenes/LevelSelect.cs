using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NursaRhythm.Scenes
{
    class LevelSelect : GameScene
    {
        private GameSprite bg;
        private GameButton play;
        private GameAnimatedSprite play_a;
        private GameAnimatedSprite level1, level2;
        private GameButton level1_b, level2_b;
        private GameObject2D pole;
        private SpriteFonts text;

        private const float Acceleration = -1.5f;
        private bool isLevelSelected = false;

        public LevelSelect()
            : base("LevelSelect")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("select\\background");
            AddSceneObject(bg);

            pole = new GameObject2D();
            pole.Translate(400, 240);
            AddSceneObject(pole);

            level1 = new GameAnimatedSprite("select\\obstacle", 4, 80, new Point(80, 80));
            level1.Translate(1000, 1000);
            level1.PlayAnimation(true);
            AddSceneObject(level1);

            level1_b = new GameButton("select\\obstaclebutton", false, true, false);
            level1_b.Translate(1000, 1000);
            level1_b.CanDraw = false;
            level1_b.OnClick += () =>
            {
                if (!isLevelSelected)
                {
                    SceneManager.whoosh.Play();

                    CameraManager.getInstance().camera.Focus = level1_b;
                    level1.CanDraw = false;
                    level1_b.CanDraw = true;

                    play_a.CanDraw = true;
                    play.CanDraw = false;

                    isLevelSelected = true;
                }
            };
            level1_b.OnEnter += () =>
            {
            };
            level1_b.OnLeave += () =>
            {
            };
            AddSceneObject(level1_b);

            level2 = new GameAnimatedSprite("select\\obstacle", 4, 80, new Point(80, 80));
            level2.Translate(500, 500);
            level2.PlayAnimation(true);
            AddSceneObject(level2);

            level2_b = new GameButton("select\\obstaclebutton", false, true, false);
            level2_b.Translate(500, 500);
            level2_b.CanDraw = false;
            level2_b.OnClick += () =>
            {
                if (!isLevelSelected)
                {
                    SceneManager.whoosh.Play();

                    CameraManager.getInstance().camera.Focus = level2_b;
                    level2.CanDraw = false;
                    level2_b.CanDraw = true;

                    play_a.CanDraw = true;
                    play.CanDraw = false;

                    isLevelSelected = true;
                }
            };
            level2_b.OnEnter += () =>
            {
            };
            level2_b.OnLeave += () =>
            {
            };
            AddSceneObject(level2_b);

            text = new SpriteFonts("font\\font");
            text.Translate(10, 10);
            text.Text = "Select Level";
            AddHUDObjectFront(text);

            play = new GameButton("select\\playbutton", true, false, false);
            play.Translate(500, 350);
            play.CanDraw = false;
            play.OnClick += () =>
            {
                if (isLevelSelected)
                {
                    SceneManager.push.Play();
                    SceneManager.SetActiveScene("Level1");
                    SceneManager.ActiveScene.ResetScene();
                    SceneManager.PlaySong(2);

                    play_a.CanDraw = true;
                    play.CanDraw = false;
                }
            };
            play.OnEnter += () =>
            {
                if (isLevelSelected)
                {
                    play_a.CanDraw = false;
                    play.CanDraw = true;
                }
            };
            play.OnLeave += () =>
            {
                if (isLevelSelected)
                {
                    play_a.CanDraw = true;
                    play.CanDraw = false;
                }
            };
            AddHUDObjectFront(play);

            play_a = new GameAnimatedSprite("select\\playbuttonanimated", 4, 150, new Point(250, 100), 1);
            play_a.Translate(500, 350);
            play_a.PlayAnimation(true);
            play_a.CanDraw = false;
            AddHUDObjectFront(play_a);
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            TouchPanel.EnabledGestures = GestureType.FreeDrag;

            UpdateInput();

            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
        }

        public override void ResetScene()
        {
            CameraManager.getInstance().camera.Focus = pole;
        }

        public override bool BackPressed()
        {
            if (!isLevelSelected)
            {
                SceneManager.push.Play();
                return true;
            }
            else
            {
                SceneManager.push.Play();

                play_a.CanDraw = false;
                play.CanDraw = false;

                level1_b.CanDraw = false;
                level1.CanDraw = true;
                level2_b.CanDraw = false;
                level2.CanDraw = true;

                isLevelSelected = false;

                CameraManager.getInstance().camera.Focus = pole;
                CameraManager.getInstance().camera.IsIgnoreY = false;
                CameraManager.getInstance().camera.ResetScreenCenter();

                return false;
            }
        }

        private void UpdateInput()
        {
            while (TouchPanel.IsGestureAvailable)
            {
                Vector2 dragDelta = Vector2.Zero;
                GestureSample gs = TouchPanel.ReadGesture();
                switch (gs.GestureType)
                {
                    case GestureType.FreeDrag:
                        dragDelta = gs.Delta;
                        break;
                }

                if (!isLevelSelected)
                {
                    float x = pole.Position.X + gs.Delta.X * Acceleration;
                    float y = pole.Position.Y + gs.Delta.Y * Acceleration;

                    if (x < 400)
                        x = 400;
                    else if (x > bg.Width - 400)
                        x = bg.Width - 400;

                    if (y < 240)
                        y = 240;
                    else if (y > bg.Height - 240)
                        y = bg.Height - 240;

                    pole.Translate(x, y);
                }
            }
        }
    }
}
