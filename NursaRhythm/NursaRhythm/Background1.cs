using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm
{
    class Background1 : GameObject2D
    {
        private const float BackgroundSpeed = 5.0f;
        private GameSprite bg1, bg2;

        public override void Initialize()
        {
            bg1 = new GameSprite("level1\\background1");
            AddChild(bg1);

            bg2 = new GameSprite("level1\\background2");
            bg2.Translate(800, 0);
            AddChild(bg2);

            base.Initialize();
        }

        public override void Update(RenderContext renderContext)
        {
            // pastiin frame-rate independent
            // background
            var objectSpeed = renderContext.GameSpeed * BackgroundSpeed;
            objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            var objectPosX = bg1.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            bg1.Translate(objectPosX, 0);

            objectPosX = bg2.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            bg2.Translate(objectPosX, 0);

            base.Update(renderContext);
        }
    }
}
