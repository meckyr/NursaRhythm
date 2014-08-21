using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm
{
    class MainCharacter : GameObject2D
    {
        private GameAnimatedSprite body, ripple;
        public GameSprite Sprite { get { return body; } }

        public CharacterState CurrentState = CharacterState.Normal;
        public enum CharacterState
        {
            Normal, Ready, Destroyed
        }

        public const float Acceleration = 5.0f;
        public const float MoveSpeed = 1.5f;

        private float up;
        private float down;

        public MainCharacter(float up, float down)
        {
            this.up = up;
            this.down = down;
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

            body = new GameAnimatedSprite("level1\\body", 10, 80, new Point(55, 58));
            body.Color = Color.AliceBlue;
            body.Origin = new Vector2(27.5f, 29);
            body.PlayAnimation(true);
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

            body.Draw(renderContext);
        }

        public override void Update(RenderContext renderContext)
        {
            // Pesawat jalan
            var objectSpeed = renderContext.GameSpeed * Acceleration;
            objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            var objectPosX = body.Position.X + objectSpeed;
            body.Translate(objectPosX, body.LocalPosition.Y);

            if (CurrentState == CharacterState.Ready)
            {
                TouchPanel.EnabledGestures = GestureType.VerticalDrag;

                // check shield
                if (!ripple.IsPlaying)
                    ripple.CanDraw = false;

                // check gesture
                if (!TouchPanel.IsGestureAvailable)
                {
                }
                else
                {
                    while (TouchPanel.IsGestureAvailable)
                    {
                        Vector2 dragPos = Vector2.Zero;
                        Vector2 dragDelta = Vector2.Zero;
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
                            Vector2 nextPosition = body.LocalPosition + (dragDelta * MoveSpeed);

                            if (nextPosition.Y <= up + body.Height / 2)
                                nextPosition.Y = up + body.Height / 2;
                            else if (nextPosition.Y >= down - body.Height / 2)
                                nextPosition.Y = down - body.Height / 2;

                            body.Translate(nextPosition);
                        }
                    }
                }
            }

            base.Update(renderContext);
        }

        public void Reset()
        {
            ripple.CanDraw = false;
            body.Translate(0, 240);

            CameraManager.getInstance().camera.Focus = body;
            CameraManager.getInstance().camera.SetScreenCenter(6, 2);
        }

        public void RippleUp()
        {
            ripple.CanDraw = true;
            ripple.PlayAnimation(false);
        }
    }
}
