using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NursaRhythm
{
    class MainCharacter : GameObject2D
    {
        private GameAnimatedSprite body, ripple;
        private Indicator indicator;
        public GameSprite Sprite { get { return body; } }

        public CharacterState CurrentState = CharacterState.Normal;
        public enum CharacterState
        {
            Normal, Ready
        }

        public const float Acceleration = 5.0f;
        public const float MoveSpeed = 5.0f;

        private float up;
        private float down;
        private float limitUp;
        private float limitDown;

        private float timer = 3;

        public MainCharacter(float up, float down, Indicator indicator)
        {
            this.up = up;
            this.down = down;
            this.indicator = indicator;
        }

        public void SetBoundary(float up, float down)
        {
            this.up = up;
            this.down = down;
        }

        public override void Initialize()
        {
            ripple = new GameAnimatedSprite("level1\\ripple", 2, 15, new Point(135, 135));
            ripple.Origin = new Vector2(67.5f, 67.5f);
            ripple.CanDraw = false;
            ripple.PlayAnimation(true);

            body = new GameAnimatedSprite("level1\\body", 2, 80, new Point(50, 50));
            body.Origin = new Vector2(25, 25);
            body.CreateBoundingRect(50, 50, true);
            body.Translate(0, 240);

            body.AddChild(ripple);
            AddChild(body);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
        }

        public override void Draw(RenderContext renderContext)
        {
            base.Draw(renderContext);
        }

        public override void Update(RenderContext renderContext)
        {
            limitUp = up + (body.Height / 2);
            limitDown = down - (body.Height / 2);

            // Pesawat jalan
            var objectSpeed = renderContext.GameSpeed * Acceleration;
            objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            var objectPosX = body.Position.X + objectSpeed;
            body.Translate(objectPosX, body.LocalPosition.Y);

            if (timer > 0)
                timer -= 0.5f;
            else
                LightOff();

            // check shield
            if (!ripple.IsPlaying)
                ripple.CanDraw = false;

            if (CurrentState == CharacterState.Ready)
            {
                TouchPanel.EnabledGestures = GestureType.VerticalDrag;

                // check gesture
                if (!TouchPanel.IsGestureAvailable)
                {
                }
                else
                {
                    Vector2 dragPos = Vector2.Zero;
                    Vector2 dragDelta = Vector2.Zero;

                    float oldPosition = body.LocalPosition.Y;
                    float nextPosition = 0f;

                    while (TouchPanel.IsGestureAvailable)
                    {
                        GestureSample gs = TouchPanel.ReadGesture();
                        switch (gs.GestureType)
                        {
                            case GestureType.VerticalDrag:
                                dragPos = gs.Position;
                                dragDelta = gs.Delta;
                                break;
                        }

                        if (dragPos.X <= 400)
                        {
                            nextPosition = oldPosition + (dragDelta.Y * MoveSpeed);

                            if (nextPosition <= limitUp)
                                nextPosition = limitUp;
                            else if (nextPosition >=  limitDown)
                                nextPosition = limitDown;

                            body.Translate(body.LocalPosition.X, nextPosition);
                        }
                    }

                    if (dragDelta.Y > 0)
                        indicator.Down();
                    else if (dragDelta.Y < 0)
                        indicator.Up();
                }
            }

            base.Update(renderContext);
        }

        public void Reset()
        {
            ripple.CanDraw = false;
            body.Translate(0, 240);

            CameraManager.getInstance().camera.Focus = body;
            CameraManager.getInstance().camera.SetScreenCenter(1200, 2);
        }

        public void RippleUp()
        {
            ripple.CanDraw = true;
            ripple.PlayAnimation(false);
        }

        public void LightOnForever()
        {
            body.CurrentFrame = 1;
            timer = 99;
        }

        public void LightOn()
        {
            body.CurrentFrame = 1;
            timer = 3;
        }

        public void LightOff()
        {
            body.CurrentFrame = 0;
            timer = 0;
        }
    }
}
