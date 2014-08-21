using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm
{
    class Background0 : GameObject2D
    {
        private float backgroundSpeed = 10.0f;
        private GameSprite bg1, bg2;

        public override void Initialize()
        {
            bg1 = new GameSprite("title\\background1");
            AddChild(bg1);

            bg2 = new GameSprite("title\\background2");
            bg2.Translate(0, -480);
            AddChild(bg2);

            base.Initialize();
        }

        public void UpdateBackgroundSpeed(float speed)
        {
            backgroundSpeed = speed;
        }

        public override void Update(RenderContext renderContext)
        {
            // pastiin frame-rate independent
            // background
            var objectSpeed = renderContext.GameSpeed * backgroundSpeed;
            objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            var objectPosY = bg1.LocalPosition.Y + objectSpeed;
            if (objectPosY > 480)
                objectPosY -= 960;

            bg1.Translate(0, objectPosY);

            objectPosY = bg2.LocalPosition.Y + objectSpeed;
            if (objectPosY > 480)
                objectPosY -= 960;

            bg2.Translate(0, objectPosY);

            base.Update(renderContext);
        }
    }
}
