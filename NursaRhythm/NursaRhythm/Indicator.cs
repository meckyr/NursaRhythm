using Microsoft.Xna.Framework;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm
{
    class Indicator : GameObject2D
    {
        private GameAnimatedSprite indicator1, indicator2;

        private float timer = 3;

        public override void Initialize()
        {
            indicator1 = new GameAnimatedSprite("level1\\indicator2", 3, 80, new Point(73, 250));
            indicator1.Translate(0, 50);
            AddChild(indicator1);

            indicator2 = new GameAnimatedSprite("level1\\indicator1", 2, 80, new Point(73, 250));
            indicator2.Translate(727, 50);
            AddChild(indicator2);

            base.Initialize();
        }

        public void Pushed()
        {
            indicator2.CurrentFrame = 1;
        }

        public void Pulled()
        {
            indicator2.CurrentFrame = 0;
        }

        public void Up()
        {
            indicator1.CurrentFrame = 1;
            timer = 3;
        }

        public void Down()
        {
            indicator1.CurrentFrame = 2;
            timer = 3;
        }

        public void Normal()
        {
            indicator1.CurrentFrame = 0;
        }

        public override void Update(RenderContext renderContext)
        {
            if (timer > 0)
                timer -= 0.5f;
            else
                Normal();

            base.Update(renderContext);
        }
    }
}
