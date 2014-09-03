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
        private List<LongNote> longNotes;
        private Pipe pipe;
        private GameButton fire, next, retry;
        private SpriteFonts text, note;
        private GameAnimatedSprite sux, meh, gr8, leet, star1, star2, star3;
        private Indicator indicator;
        private GameSprite failed, finished;

        private int score = 0;

        private const double Delay = 1;
        private double delay = Delay;
        private int health = 255;
        private Random random;

        private bool isFailed = false;
        private bool isFinished = false;

        public Level1()
            : base("Level1")
        {
        }

        public override void Initialize()
        {
            // set game speed
            SceneManager.RenderContext.GameSpeed = 100;
            random = new Random();

            notes = new List<Note>();
            longNotes = new List<LongNote>();

            bg = new Background1();
            AddHUDObjectBack(bg);

            pipe = new Pipe("level1\\pipe", 50);
            AddHUDObjectBack(pipe);

            indicator = new Indicator();
            AddHUDObjectFront(indicator);

            text = new SpriteFonts("font\\font");
            text.Color = Color.SaddleBrown;
            text.Translate(580, 10);
            AddHUDObjectBack(text);

            fire = new GameButton("level1\\invisbutton", false, false, false);
            fire.CanDraw = false;
            fire.Translate(400, 0);
            fire.OnEnter += () =>
            {
                if (!isFailed && !isFinished)
                {
                    SceneManager.Vibrator.Start(TimeSpan.FromMilliseconds(100));
                    indicator.Pushed();
                    //Debug.WriteLine(MediaPlayer.PlayPosition);
                    if (LongNoteCheck() == 0)
                    {
                        //Debug.WriteLine(mainChar.Sprite.Position.X);
                        SpawnScore(NoteCheck());
                    }
                    else
                    {
                        //Debug.WriteLine(mainChar.Sprite.Position.X);
                        mainChar.LightOnForever();
                        SceneManager.pulse2.Play();
                    }
                }
            };
            fire.OnClick += () =>
            {
                if (!isFailed && !isFinished)
                {
                    //Debug.WriteLine(mainChar.Sprite.Position.X);
                    indicator.Pulled();
                    SpawnScore(NoteCheckOut());
                }
            };
            fire.OnLeave += () =>
            {
                indicator.Pulled();
            };
            AddHUDObjectBack(fire);

            InitiateScoring();
            InitiateNote();
            InitiateLongNote();

            mainChar = new MainCharacter(50, 300, indicator);
            AddSceneObject(mainChar);

            failed = new GameSprite("level1\\boxfailed");
            failed.Origin = new Vector2(181, 149.5f);
            failed.Translate(400, -300);
            failed.CanDraw = false;
            AddHUDObjectFront(failed);

            finished = new GameSprite("level1\\boxfinished");
            finished.Origin = new Vector2(230, 181);
            finished.Translate(400, -300);
            finished.CanDraw = false;
            AddHUDObjectFront(finished);

            note = new SpriteFonts("font\\fontsmall");
            note.Color = Color.Goldenrod;
            note.Translate(265, 280);
            AddHUDObjectFront(note);

            star1 = new GameAnimatedSprite("level1\\star", 12, 40, new Point(73, 73), 6);
            star1.Origin = new Vector2(36.5f, 36.5f);
            star1.Translate(400, -290);
            star1.CanDraw = false;
            AddHUDObjectFront(star1);

            star2 = new GameAnimatedSprite("level1\\star", 12, 40, new Point(73, 73), 6);
            star2.Origin = new Vector2(36.5f, 36.5f);
            star2.Translate(470, -290);
            star2.CanDraw = false;
            AddHUDObjectFront(star2);

            star3 = new GameAnimatedSprite("level1\\star", 12, 40, new Point(73, 73), 6);
            star3.Origin = new Vector2(36.5f, 36.5f);
            star3.Translate(330, -290);
            star3.CanDraw = false;
            AddHUDObjectFront(star3);

            next = new GameButton("level1\\next", true, false, false);
            next.CanDraw = false;
            next.Translate(420, -215);
            next.OnClick += () =>
            {
                SceneManager.push.Play();
                SceneManager.SetActiveScene("LevelSelect");
                SceneManager.ActiveScene.ResetScene();
                SceneManager.PlaySong(1);
            };
            AddHUDObjectFront(next);

            retry = new GameButton("level1\\retry", true, false, false);
            retry.CanDraw = false;
            retry.Translate(295, -215);
            retry.OnClick += () =>
            {
            };
            AddHUDObjectFront(retry);

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
            foreach (LongNote n in longNotes)
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
            foreach (LongNote n in longNotes)
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
                        SceneManager.sux.Play();
                        sux.CanDraw = true;
                        sux.PlayAnimation(false);
                        mainChar.RippleUp();
                        mainChar.LightOff();
                        SetHealth(-25);
                        break;
                    case 1:
                        SceneManager.pulse.Play();
                        leet.CanDraw = true;
                        leet.PlayAnimation(false);
                        mainChar.RippleUp();
                        mainChar.LightOn();
                        SetHealth(50);
                        score += 100;
                        break;
                    case 2:
                        SceneManager.pulse.Play();
                        meh.CanDraw = true;
                        meh.PlayAnimation(false);
                        mainChar.RippleUp();
                        mainChar.LightOn();
                        SetHealth(10);
                        score += 25;
                        break;
                    case 3:
                        SceneManager.pulse.Play();
                        gr8.CanDraw = true;
                        gr8.PlayAnimation(false);
                        mainChar.RippleUp();
                        mainChar.LightOn();
                        SetHealth(25);
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
                if (!isFailed && !isFinished)
                {
                    CameraManager.getInstance().camera.IsIgnoreY = true;
                    mainChar.CurrentState = MainCharacter.CharacterState.Ready;
                }

                if (isFailed)
                {
                    if (!failed.CanDraw)
                        SceneManager.failed.Play();

                    failed.CanDraw = true;
                    next.CanDraw = true;
                    retry.CanDraw = true;
                    MediaPlayer.Pause();

                    float pos1 = failed.LocalPosition.Y;
                    float pos2 = next.LocalPosition.Y;
                    float pos3 = retry.LocalPosition.Y;
                    if (pos1 < 250)
                    {
                        pos1 += 10;
                        pos2 += 10;
                        pos3 += 10;

                        failed.Translate(failed.LocalPosition.X, pos1);
                        next.Translate(next.LocalPosition.X, pos2);
                        retry.Translate(retry.LocalPosition.X, pos3);
                    }
                }

                if (isFinished)
                {
                    if (!finished.CanDraw)
                        SceneManager.finish.Play();

                    finished.CanDraw = true;
                    next.CanDraw = true;
                    retry.CanDraw = true;
                    star1.CanDraw = true;
                    star2.CanDraw = true;
                    star3.CanDraw = true;
                    MediaPlayer.Pause();

                    float pos1 = finished.LocalPosition.Y;
                    float pos2 = next.LocalPosition.Y;
                    float pos3 = retry.LocalPosition.Y;
                    float pos4 = star1.LocalPosition.Y;
                    if (pos1 < 220)
                    {
                        pos1 += 10;
                        pos2 += 10;
                        pos3 += 10;
                        pos4 += 10;

                        finished.Translate(finished.LocalPosition.X, pos1);
                        next.Translate(next.LocalPosition.X, pos2);
                        retry.Translate(retry.LocalPosition.X, pos3);
                        star1.Translate(star1.LocalPosition.X, pos4);
                        star2.Translate(star2.LocalPosition.X, pos4);
                        star3.Translate(star3.LocalPosition.X, pos4);
                    }
                    else
                        CheckScore();
                }

                UpdateScore();
                UpdateTarget();

                if (MediaPlayer.PlayPosition >= TimeSpan.FromSeconds(145.0) && !isFinished)
                {
                    mainChar.CurrentState = MainCharacter.CharacterState.Normal;
                    isFinished = true;

                    DestroyAllNote();
                    bg.DestroyScenery();

                    next.Translate(420, -180);
                    retry.Translate(295, -180);
                }
            }

            text.Text = "Score: " + score;

            base.Update(rendercontext, contentmanager);
        }

        public void DestroyAllNote()
        {
            foreach (Note note in notes)
                RemoveSceneObject(note);
            foreach (LongNote note in longNotes)
                RemoveSceneObject(note);
        }

        public void UpdateTarget()
        {
            foreach (LongNote note in longNotes)
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
        }

        public void InitiateScoring()
        {
            sux = new GameAnimatedSprite("level1\\sux", 2, 80, new Point(542, 200), 1);
            sux.Origin = new Vector2(271, 100);
            sux.Translate(400, 200);
            sux.CanDraw = false;
            AddHUDObjectBack(sux);

            meh = new GameAnimatedSprite("level1\\meh", 2, 80, new Point(542, 200), 1);
            meh.Origin = new Vector2(271, 100);
            meh.Translate(400, 200);
            meh.CanDraw = false;
            AddHUDObjectBack(meh);

            gr8 = new GameAnimatedSprite("level1\\gr8", 2, 80, new Point(542, 200), 1);
            gr8.Origin = new Vector2(271, 100);
            gr8.Translate(400, 200);
            gr8.CanDraw = false;
            AddHUDObjectBack(gr8);

            leet = new GameAnimatedSprite("level1\\leet", 2, 80, new Point(542, 200), 1);
            leet.Origin = new Vector2(271, 100);
            leet.Translate(400, 200);
            leet.CanDraw = false;
            AddHUDObjectBack(leet);
        }

        public void CheckScore()
        {
            if (star3.CurrentFrame != 11)
            {
                if (!star3.IsPlaying)
                    SceneManager.star.Play();
                
                star3.PlayAnimation(false);
            }
            else
                star3.PauseAnimation();

            if (score > 3000 && star3.IsPaused)
            {
                if (star1.CurrentFrame != 11)
                {
                    if (!star1.IsPlaying)
                        SceneManager.star.Play();

                    star1.PlayAnimation(false);
                }
                else
                    star1.PauseAnimation();
            }
            else if (star3.IsPaused)
            {
                note.CanDraw = true;
                note.Text = "One new MANDRAGUNA ARCHIVE \n                         unlocked!";
            }

            if (score > 6000 && star3.IsPaused && star1.IsPaused)
            {
                if (star2.CurrentFrame != 11)
                {
                    if (!star2.IsPlaying)
                        SceneManager.star.Play();

                    star2.PlayAnimation(false);
                }
                else
                {
                    star2.PauseAnimation();
                    note.CanDraw = true;
                    note.Text = "Three new MANDRAGUNA ARCHIVE \n                         unlocked!";
                }
            }
            else if (star3.IsPaused && star1.IsPaused)
            {
                note.CanDraw = true;
                note.Text = "Two new MANDRAGUNA ARCHIVE \n                         unlocked!";
            }
        }

        public void SetHealth(int value)
        {
            health += value;

            if (health >= 255)
                health = 255;
            if (health <= 0)
            {
                isFailed = true;
                health = 0;

                DestroyAllNote();
                bg.DestroyScenery();
                mainChar.CurrentState = MainCharacter.CharacterState.Normal;
            }

            pipe.SetHealth((byte)health);
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
                    int next = random.Next(75, 275);

                    LongNote note = new LongNote(new Vector2(pos, next), new Vector2(value, 1));
                    longNotes.Add(note);
                    AddSceneObject(note);
                }
            }
        }

        public void InitiateNote()
        {
            foreach (float location in ReadFile("level1note.txt"))
            {
                int next = random.Next(75, 275);

                Note note = new Note(new Vector2(location, next), "level1\\note");
                notes.Add(note);
                AddSceneObject(note);
            }
        }
    }
}
