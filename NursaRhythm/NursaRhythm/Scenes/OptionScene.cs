using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm.Scenes
{
    class OptionScene : GameScene
    {
        private GameSprite bg;
        private SpriteFont text;

        public OptionScene()
            : base("OptionScene")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("menu\\background");
            AddSceneObject(bg);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);

            text = contentmanager.Load<SpriteFont>("font\\font");
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);

            rendercontext.SpriteBatch.DrawString(text, "Music", new Vector2(100, 100), Color.White);
            rendercontext.SpriteBatch.DrawString(text, "SFX", new Vector2(100, 300), Color.White);
        }

        public override void ResetScene()
        {
        }

        public override bool BackPressed()
        {
            SceneManager.push.Play();
            return true;
        }
    }
}
