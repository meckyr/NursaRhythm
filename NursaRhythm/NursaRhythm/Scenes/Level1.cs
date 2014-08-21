using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using NursaRhythm.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace NursaRhythm.Scenes
{
    class Level1 : GameScene
    {
        private Background1 bg;
        private MainCharacter mainChar;
        private List<Note> notes;
        private List<LongNote> longnotes;
        private Pipe pipe;
        private GameButton fire;
        private SpriteFonts text;
        private GameAnimatedSprite sux, meh, gr8, leet;

        private int score = 0;

        private const double Delay = 1;
        private double delay = Delay;

        public Level1()
            : base("Level1")
        {
        }

        public override void Initialize()
        {
            // set game speed
            SceneManager.RenderContext.GameSpeed = 100;

            notes = new List<Note>();
            longnotes = new List<LongNote>();

            bg = new Background1();
            AddHUDObjectBack(bg);

            pipe = new Pipe("level1\\pipe", 50);
            AddHUDObjectBack(pipe);

            text = new SpriteFonts("font\\font");
            text.Translate(580, 10);
            AddHUDObjectBack(text);

            fire = new GameButton("level1\\invisbutton", false, false, false);
            fire.CanDraw = false;
            fire.Translate(400, 0);
            fire.OnEnter += () =>
            {
                SceneManager.Vibrator.Start(TimeSpan.FromMilliseconds(100));

                Debug.WriteLine(mainChar.Sprite.Position.X);
                if (LongNoteCheck() == 0)
                {
                    //Debug.WriteLine(mainChar.Sprite.Position.X);
                    SpawnScore(NoteCheck());
                }
                else
                {
                    //Debug.WriteLine(mainChar.Sprite.Position.X);
                    mainChar.RippleUp();
                    SceneManager.pulse2.Play();
                }
            };
            fire.OnClick += () =>
            {
                //Debug.WriteLine(mainChar.Sprite.Position.X);
                SpawnScore(NoteCheckOut());
            };
            AddHUDObjectBack(fire);

            InitiateScoring();
            InitiateNote();
            InitiateLongNote();

            mainChar = new MainCharacter(50, 300);
            AddSceneObject(mainChar);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);
        }

        public int NoteCheck()
        {
            int num = 0;
            foreach (Note n in notes)
            {
                num = n.CheckCollision(mainChar.Sprite);

                if (num != 0)
                    break;
            }

            return num;
        }

        public int NoteCheckOut()
        {
            int num = -1;
            foreach (LongNote n in longnotes)
            {
                num = n.CheckOut(mainChar.Sprite);

                if (num != -1)
                    break;
            }

            return num;
        }

        public int LongNoteCheck()
        {
            int num = 0;
            foreach (LongNote n in longnotes)
            {
                num = n.CheckCollision(mainChar.Sprite);

                if (num != 0)
                    break;
            }

            return num;
        }

        public void SpawnScore(int num)
        {
            if (mainChar.CurrentState == MainCharacter.CharacterState.Ready)
            {

                switch (num)
                {
                    case 0:
                        SceneManager.pulse.Play();
                        sux.CanDraw = true;
                        sux.PlayAnimation(false);
                        mainChar.RippleUp();
                        break;
                    case 1:
                        SceneManager.pulse.Play();
                        leet.CanDraw = true;
                        leet.PlayAnimation(false);
                        mainChar.RippleUp();
                        score += 100;
                        break;
                    case 2:
                        SceneManager.pulse.Play();
                        meh.CanDraw = true;
                        meh.PlayAnimation(false);
                        mainChar.RippleUp();
                        score += 25;
                        break;
                    case 3:
                        SceneManager.pulse.Play();
                        gr8.CanDraw = true;
                        gr8.PlayAnimation(false);
                        mainChar.RippleUp();
                        score += 50;
                        break;
                }
            }
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            if (delay > 0)
            {
                delay -= rendercontext.GameTime.ElapsedGameTime.TotalSeconds;
            }

            if (delay <= 0)
            {
                mainChar.CurrentState = MainCharacter.CharacterState.Ready;
                CameraManager.getInstance().camera.IsIgnoreY = true;
                UpdateScore();
                UpdateTarget();
            }

            text.Text = "Score: " + score;

            base.Update(rendercontext, contentmanager);
        }

        public void UpdateTarget()
        {
            foreach (LongNote note in longnotes)
                note.UpdateTarget(mainChar.Sprite.Position);

            foreach (Note note in notes)
                note.UpdateTarget(mainChar.Sprite.Position);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
        }

        public override void ResetScene()
        {
            mainChar.Reset();
        }

        public void UpdateScore()
        {
            if (!sux.IsPlaying)
                sux.CanDraw = false;
            if (!meh.IsPlaying)
                meh.CanDraw = false;
            if (!gr8.IsPlaying)
                gr8.CanDraw = false;
            if (!leet.IsPlaying)
                leet.CanDraw = false;

            sux.Translate(mainChar.Sprite.LocalPosition - new Vector2(-120, 80));
            meh.Translate(mainChar.Sprite.LocalPosition - new Vector2(-120, 80));
            gr8.Translate(mainChar.Sprite.LocalPosition - new Vector2(-120, 80));
            leet.Translate(mainChar.Sprite.LocalPosition - new Vector2(-120, 80));

            if (sux.LocalPosition.Y <= 50)
            {
                sux.Translate(mainChar.Sprite.LocalPosition - new Vector2(-120, -80));
                meh.Translate(mainChar.Sprite.LocalPosition - new Vector2(-120, -80));
                gr8.Translate(mainChar.Sprite.LocalPosition - new Vector2(-120, -80));
                leet.Translate(mainChar.Sprite.LocalPosition - new Vector2(-120, -80));
            }
        }

        public void InitiateScoring()
        {
            sux = new GameAnimatedSprite("level1\\sux", 5, 40, new Point(200, 70), 1);
            sux.Origin = new Vector2(100, 35);
            sux.Translate(500, 200);
            sux.CanDraw = false;
            AddSceneObject(sux);

            meh = new GameAnimatedSprite("level1\\meh", 5, 40, new Point(200, 70), 1);
            meh.Origin = new Vector2(100, 35);
            meh.Translate(500, 200);
            meh.CanDraw = false;
            AddSceneObject(meh);

            gr8 = new GameAnimatedSprite("level1\\gr8", 5, 40, new Point(200, 70), 1);
            gr8.Origin = new Vector2(100, 35);
            gr8.Translate(500, 200);
            gr8.CanDraw = false;
            AddSceneObject(gr8);

            leet = new GameAnimatedSprite("level1\\leet", 5, 40, new Point(200, 70), 1);
            leet.Origin = new Vector2(100, 35);
            leet.Translate(500, 200);
            leet.CanDraw = false;
            AddSceneObject(leet);
        }

        public override void DoSomething()
        {
            SpawnScore(0);
        }

        public List<float> ReadFile(string filename)
        {
            string data = null;
            List<float> location = new List<float>();

            using (var stream = TitleContainer.OpenStream(filename))
            {
                using (var reader = new StreamReader(stream))
                    data = reader.ReadToEnd();
            }

            var text = data.Split('\n');

            foreach (string s in text)
            {
                location.Add(float.Parse(s));
            }

            return location;
        }

        public void InitiateLongNote()
        {
            float pos = 0;
            foreach (float value in ReadFile("level1longnote.txt"))
            {
                if (value > 10000)
                    pos = value;
                else
                {
                    LongNote note = new LongNote(new Vector2(pos, 240), new Vector2(value, 1));
                    longnotes.Add(note);
                    AddSceneObject(note);
                }
            }
        }

        public void InitiateNote()
        {
            foreach (float location in ReadFile("level1note.txt"))
            {
                Note note = new Note(new Vector2(location, 240));
                notes.Add(note);
                AddSceneObject(note);
            }
        }
    }
}
