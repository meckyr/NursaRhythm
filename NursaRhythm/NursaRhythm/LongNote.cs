using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NursaRhythm
{
    class LongNote : GameObject2D
    {
        private Note noteIn;
        private Note noteOut;
        private GameSprite block;

        private Vector2 position;
        private Vector2 scale;
        private Vector2 targetPos;

        public NoteState CurrentState = NoteState.Normal;
        public enum NoteState
        {
            Normal,
            Enter,
            Passed
        }

        public LongNote(Vector2 pos, Vector2 scl)
        {
            position = pos;
            scale = scl;
        }

        public override void Initialize()
        {
            block = new GameSprite("level1\\block");
            block.Origin = new Vector2(0, 25);
            block.CreateBoundingRect(50, 50, new Vector2(0, -25), false);
            block.Scale(scale);
            block.Translate(position);
            block.CanDraw = false;
            AddChild(block);

            noteIn = new Note(position, "level1\\note");
            AddChild(noteIn);

            noteOut = new Note(position + scale * new Vector2(50, 0), "level1\\noteend");
            AddChild(noteOut);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
        }

        public void UpdateTarget(Vector2 pos)
        {
            targetPos = pos;
        }

        public override void Update(RenderContext renderContext)
        {
            noteIn.UpdateTarget(targetPos);
            noteOut.UpdateTarget(targetPos);

            if ((position.X <= (targetPos.X + 800) && position.X >= (targetPos.X - 1600)))
            {
                block.CanDraw = true;
            }
            else
            {
                block.CanDraw = false;
            }

            if (CurrentState == NoteState.Enter)
            {
                block.Color = Color.SaddleBrown;

                if (!block.HitTest(targetPos) && !noteIn.Sprite.HitTest(targetPos) && !noteOut.Sprite.HitTest(targetPos))
                {
                    CurrentState = NoteState.Passed;
                    SceneManager.ActiveScene.DoSomething();
                }
            }
            else
                block.Color = Color.White;

            base.Update(renderContext);
        }

        public override void Draw(RenderContext renderContext)
        {
            base.Draw(renderContext);
        }

        public int CheckCollision(GameObject2D sprite) 
        {
            if (CurrentState == NoteState.Normal)
            {
                if (noteIn.CheckCollision(sprite) > 0)
                {
                    CurrentState = NoteState.Enter;
                    return 1;
                }
                return 0;
            }
            return 0;
        }

        public int CheckOut(GameObject2D sprite)
        {
            if (CurrentState == NoteState.Enter)
            {
                CurrentState = NoteState.Passed;
                return noteOut.CheckCollision(sprite);
            }
            return -1;
        }
    }
}
