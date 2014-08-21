using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm.Tools
{
    class GameButton : GameSprite
    {
        private bool isSpriteSheet;
        private bool isOutside;
        private bool isOriginCenter;
        private Rectangle? normalRect, pressedRect;
        private bool isPressed;
        private int touchId;

        private Vector2 offset;

        private Vector2 center = new Vector2(400, 240);

        public event Action OnClick;
        public event Action OnEnter;
        public event Action OnLeave;

        public GameButton(string assetfile, bool isspritesheet, bool isoutside, bool isorigincenter)
            : base(assetfile)
        {
            isSpriteSheet = isspritesheet;
            isOutside = isoutside;
            isOriginCenter = isorigincenter;
        }

        public GameButton(string assetfile, bool isspritesheet, bool isoutside, bool isorigincenter, Vector2 offset)
            : base(assetfile)
        {
            isSpriteSheet = isspritesheet;
            isOutside = isoutside;
            isOriginCenter = isorigincenter;

            this.offset = offset;
        }

        public void BackToNormal()
        {
            DrawRect = normalRect;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            if (isSpriteSheet)
            {
                CreateBoundingRect((int)Width, (int)Height / 2, offset, isOriginCenter);
                normalRect = new Rectangle(0, 0, (int)Width, (int)(Height / 2f));
                pressedRect = new Rectangle(0, (int)(Height / 2f), (int)Width, (int)(Height / 2f));
            }
            else
                CreateBoundingRect((int)Width, (int)Height, offset, isOriginCenter);
        }

        public override void Update(RenderContext renderContext)
        {
            base.Update(renderContext);

            var touchStates = renderContext.TouchPanelState;
            if (!isPressed)
            {
                DrawRect = normalRect;

                foreach (var location in touchStates)
                {
                    var touchPosition = location.Position;

                    if (isOutside)
                        touchPosition = location.Position + (CameraManager.getInstance().camera.Position - center);

                    if (HitTest(touchPosition, false))
                    {
                        isPressed = true;
                        touchId = location.Id;

                        //Entered
                        if (OnEnter != null)
                            OnEnter();
                        DrawRect = pressedRect;
                        break;
                    }
                }
            }
            else
            {
                var location = touchStates.FirstOrDefault(tloc => tloc.Id == touchId);
                var touchPosition = location.Position;

                if (isOutside)
                    touchPosition = location.Position + (CameraManager.getInstance().camera.Position - center);

                if (location == null || !HitTest(touchPosition, false))
                {
                    touchId = -1;
                    isPressed = false;

                    //Left
                    if (OnLeave != null)
                        OnLeave();
                }
                else
                {
                    if (location.State == TouchLocationState.Released)
                    {
                        touchId = -1;
                        isPressed = false;

                        //Clicked
                        if (OnClick != null)
                            OnClick();
                    }
                }
            }
        }
    }
}
