using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm.Scenes
{
    class LevelSelect : GameScene
    {
        private GameSprite bg;
        private GameAnimatedSprite level1;
        private GameObject2D pole;
        private SpriteFonts text;

        private const float Acceleration = -1.5f;

        public LevelSelect()
            : base("LevelSelect")
        {
        }

        public override void Initialize()
        {
            TouchPanel.EnabledGestures = GestureType.FreeDrag;

            bg = new GameSprite("select\\background");
            AddSceneObject(bg);

            pole = new GameObject2D();
            pole.Translate(400, 240);
            AddSceneObject(pole);

            level1 = new GameAnimatedSprite("select\\obstacle", 4, 80, new Point(80, 80));
            level1.Translate(1000, 1000);
            level1.PlayAnimation(true);
            AddSceneObject(level1);

            text = new SpriteFonts("font\\font");
            text.Translate(10, 10);
            text.Text = "Select Level";
            AddHUDObject(text);
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
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
            SceneManager.push.Play();
            return true;
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
