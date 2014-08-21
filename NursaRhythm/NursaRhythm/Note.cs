using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm
{
    class Note : GameObject2D
    {
        private GameAnimatedSprite note;
        public GameSprite Sprite { get { return note; } }

        private Vector2 position;
        private Vector2 targetPos;

        public NoteState CurrentState = NoteState.Normal;
        public enum NoteState
        {
            Normal,
            Passed
        }

        public Note(Vector2 pos)
        {
            position = pos;
        }

        public override void Initialize()
        {
            note = new GameAnimatedSprite("level1\\note", 4, 80, new Point(80, 80));
            note.Origin = new Vector2(40, 40);
            note.Translate(position);
            note.PlayAnimation(true);
            note.CreateBoundingRect(50, 50, true);
            note.CanDraw = false;
            AddChild(note);

            base.Initialize();
        }

        public void UpdateTarget(Vector2 pos)
        {
            targetPos = pos;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
        }

        public override void Update(RenderContext renderContext)
        {
            if ((position.X <= (targetPos.X + 800) && position.X >= (targetPos.X - 400)))
            {
                note.CanDraw = true;
            }
            else
            {
                note.CanDraw = false;
            }

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
                if (note.HitTest(sprite))
                {
                    // Find the bounds of the rectangle intersection
                    float top = Math.Max(note.BoundingRect.Value.Top, sprite.BoundingRect.Value.Top);
                    float bottom = Math.Min(note.BoundingRect.Value.Bottom, sprite.BoundingRect.Value.Bottom);
                    float left = Math.Max(note.BoundingRect.Value.Left, sprite.BoundingRect.Value.Left);
                    float right = Math.Min(note.BoundingRect.Value.Right, sprite.BoundingRect.Value.Right);

                    float width = right - left;
                    float height = bottom - top;

                    float area = width * height;

                    if (area > 300)
                    {
                        if (area > 800)
                        {
                            if (area >= 1600)
                            {
                                CurrentState = NoteState.Passed;
                                return 1;
                            }
                            CurrentState = NoteState.Passed;
                            return 3;
                        }
                        CurrentState = NoteState.Passed;
                        return 2;
                    }
                    return 0;
                }
                return 0;
            }
            return 0;
        }
    }
}
