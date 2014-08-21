using Microsoft.Xna.Framework;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm
{
    class Pipe : GameObject2D
    {
        private const float BackgroundSpeed = 5.0f;
        private GameSprite bg1, bg2;

        private string assetName;
        private float posY;

        public Pipe(string assetname, float posy) 
        {
            assetName = assetname;
            posY = posy;
        }

        public override void Initialize()
        {
            bg1 = new GameSprite(assetName);
            bg1.Translate(0, posY);
            bg1.Color = Color.White * 0.2f;
            AddChild(bg1);

            bg2 = new GameSprite(assetName);
            bg2.Translate(800, posY);
            bg2.Color = Color.White * 0.2f;
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

            bg1.Translate(objectPosX, posY);

            objectPosX = bg2.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            bg2.Translate(objectPosX, posY);

            base.Update(renderContext);
        }
    }
}
