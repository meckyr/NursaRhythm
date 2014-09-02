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

        // box info
        private SpriteFonts text, region, song;
        private GameSprite box, star1, star2, star3, line1, line2;
        private GameButton play;
        
        //level
        private GameAnimatedSprite level1;
        private GameButton level1_b;
        private GameSprite level2, level3, level4;
        
        private GameObject2D pole;

        private const float Acceleration = -1.5f;
        private bool isLevelSelected = false;

        public LevelSelect()
            : base("LevelSelect")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("select\\background");
            bg.Scale(0.5f, 0.5f);
            AddSceneObject(bg);

            pole = new GameObject2D();
            pole.Translate(400, 240);
            AddSceneObject(pole);

            level1 = new GameAnimatedSprite("select\\levelunlockanimated", 4, 80, new Point(78, 76));
            level1.Origin = new Vector2(39, 38);
            level1.Translate(811, 597);
            AddSceneObject(level1);

            level2 = new GameSprite("select\\levellock");
            level2.Origin = new Vector2(39, 38);
            level2.Translate(800, 778);
            AddSceneObject(level2);

            level3 = new GameSprite("select\\levellock");
            level3.Origin = new Vector2(39, 38);
            level3.Translate(693, 755);
            AddSceneObject(level3);

            level4 = new GameSprite("select\\levellock");
            level4.Origin = new Vector2(39, 38);
            level4.Translate(1083, 831);
            AddSceneObject(level4);

            level1_b = new GameButton("select\\levelunlock", false, true, true);
            level1_b.Origin = new Vector2(39, 38);
            level1_b.Translate(811, 597);
            level1_b.OnClick += () =>
            {
                if (!isLevelSelected)
                {
                    SceneManager.push.Play();
                    CameraManager.getInstance().camera.SetScreenCenter(4, 2);
                    CameraManager.getInstance().camera.Focus = level1_b;

                    level1.PlayAnimation(true);
                    SetBoxDraw(true);

                    isLevelSelected = true;
                }
            };
            AddSceneObject(level1_b);

            InitiateBox();
        }

        public void SetBoxDraw(bool canDraw)
        {
            play.CanDraw = canDraw;
            box.CanDraw = canDraw;
            star1.CanDraw = canDraw;
            star2.CanDraw = canDraw;
            star3.CanDraw = canDraw;
            line1.CanDraw = canDraw;
            line2.CanDraw = canDraw;
            region.CanDraw = canDraw;
            song.CanDraw = canDraw;
        }

        public void InitiateBox()
        {
            box = new GameSprite("select\\infobox");
            box.Origin = new Vector2(182.5f, 182.5f);
            box.Translate(550, 220);
            box.CanDraw = false;
            AddHUDObjectFront(box);

            star1 = new GameSprite("select\\star1");
            star1.Origin = new Vector2(36.5f, 36.5f);
            star1.Translate(550, 290);
            star1.CanDraw = false;
            AddHUDObjectFront(star1);

            star2 = new GameSprite("select\\star1");
            star2.Origin = new Vector2(36.5f, 36.5f);
            star2.Translate(480, 290);
            star2.CanDraw = false;
            AddHUDObjectFront(star2);

            star3 = new GameSprite("select\\star1");
            star3.Origin = new Vector2(36.5f, 36.5f);
            star3.Translate(620, 290);
            star3.CanDraw = false;
            AddHUDObjectFront(star3);

            song = new SpriteFonts("font\\fontbold");
            song.Translate(435, 95);
            song.Color = Color.SaddleBrown;
            song.Text = "AMPAR-AMPAR\n         PISANG";
            song.CanDraw = false;
            AddHUDObjectFront(song);

            line1 = new GameSprite("select\\line");
            line1.Origin = new Vector2(132, 4.5f);
            line1.Translate(550, 180);
            line1.CanDraw = false;
            AddHUDObjectFront(line1);

            region = new SpriteFonts("font\\font");
            region.Translate(440, 195);
            region.Color = Color.SaddleBrown;
            region.Text = "South Kalimantan";
            region.CanDraw = false;
            AddHUDObjectFront(region);

            line2 = new GameSprite("select\\line");
            line2.Origin = new Vector2(132, 4.5f);
            line2.Translate(550, 240);
            line2.CanDraw = false;
            AddHUDObjectFront(line2);

            text = new SpriteFonts("font\\font");
            text.Translate(15, 15);
            text.Color = Color.SaddleBrown;
            text.Text = "Select Level/Region...";
            AddHUDObjectFront(text);

            play = new GameButton("select\\playbutton", true, false, true);
            play.Origin = new Vector2(90.5f, 44.5f);
            play.Translate(550, 385);
            play.CanDraw = false;
            play.OnClick += () =>
            {
                if (isLevelSelected)
                {
                    SceneManager.push.Play();
                    SceneManager.SetActiveScene("Level1");
                    SceneManager.ActiveScene.ResetScene();
                    SceneManager.PlaySong(2);

                    play.CanDraw = false;
                }
            };
            AddHUDObjectFront(play);
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            TouchPanel.EnabledGestures = GestureType.FreeDrag;

            UpdateInput();

            if (bg.LocalScale.X <= 1 && bg.LocalScale.Y <= 1)
            {
                bg.LocalScale += new Vector2(0.05f, 0.05f);
            }

            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
        }

        public override void ResetScene()
        {
            CameraManager.getInstance().camera.Focus = pole;
            bg.Scale(0.5f, 0.5f);
            pole.Translate(400, 240);
        }

        public override bool BackPressed()
        {
            if (!isLevelSelected)
                return true;
            else
            {
                SceneManager.push.Play();

                SetBoxDraw(false);

                level1.StopAnimation();

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
