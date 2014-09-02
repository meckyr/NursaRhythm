using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm.Scenes
{
    class Archive : GameScene
    {
        private GameSprite bg;
        private GameSprite jukungcodex, babicodex, pinangcodex;
        private GameButton jukung, babi, pinang;

        private bool isCodexUp = false;

        public Archive()
            : base("Archive")
        {
        }

        public override void Initialize()
        {
            bg = new GameSprite("archive\\kalsel");
            AddSceneObject(bg);

            jukung = new GameButton("archive\\jukung", false, false, true);
            jukung.Origin = new Vector2(120.5f, 153.5f);
            jukung.Translate(150, 300);
            jukung.OnClick += () =>
            {
                if (!isCodexUp)
                {
                    SceneManager.push.Play();
                    isCodexUp = true;
                    jukungcodex.CanDraw = true;
                }
            };
            AddSceneObject(jukung);

            babi = new GameButton("archive\\babi", false, false, true);
            babi.Origin = new Vector2(120.5f, 153.5f);
            babi.Translate(410, 300);
            babi.OnClick += () =>
            {
                if (!isCodexUp)
                {
                    SceneManager.push.Play();
                    isCodexUp = true;
                    babicodex.CanDraw = true;
                }
            };
            AddSceneObject(babi);

            pinang = new GameButton("archive\\pinang", false, false, true);
            pinang.Origin = new Vector2(120.5f, 153.5f);
            pinang.Translate(660, 300);
            pinang.OnClick += () =>
            {
                if (!isCodexUp)
                {
                    SceneManager.push.Play();
                    isCodexUp = true;
                    pinangcodex.CanDraw = true;
                }
            };
            AddSceneObject(pinang);

            jukungcodex = new GameSprite("archive\\jukungcodex");
            jukungcodex.CanDraw = false;
            AddSceneObject(jukungcodex);

            babicodex = new GameSprite("archive\\babicodex");
            babicodex.CanDraw = false;
            AddSceneObject(babicodex);

            pinangcodex = new GameSprite("archive\\pinangcodex");
            pinangcodex.CanDraw = false;
            AddSceneObject(pinangcodex);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
        }

        public override void ResetScene()
        {
            isCodexUp = false;
        }

        public override bool BackPressed()
        {
            if (isCodexUp)
            {
                SceneManager.push.Play();
                isCodexUp = false;

                jukungcodex.CanDraw = false;
                babicodex.CanDraw = false;
                pinangcodex.CanDraw = false;

                return false;
            }
            else
                return true;
        }
    }
}
