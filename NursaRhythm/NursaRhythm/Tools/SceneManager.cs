using Microsoft.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace NursaRhythm.Tools
{
    class SceneManager
    {
        public static List<GameScene> GameScenes { get; private set; }
        public static GameScene ActiveScene { get; private set; }
        public static RenderContext RenderContext { get; private set; }
        public static GameTime gameTime;

        // kumpulan soundeffect
        public static SoundEffect push { get; private set; }
        public static SoundEffect pulse { get; private set; }
        public static SoundEffect pulse2 { get; private set; }
        public static SoundEffect sux { get; private set; }
        public static SoundEffect paper { get; private set; }
        public static SoundEffect star { get; private set; }
        public static SoundEffect failed { get; private set; }
        public static SoundEffect finish { get; private set; }

        // kumpulan music
        public static Song mainmusic;
        public static Song levelmusic;

        public static bool IsInitialized = false;
        public static bool IsMusicPlaying = false;

        public static VibrateController Vibrator = VibrateController.Default;

        static SceneManager()
        {
            GameScenes = new List<GameScene>();
            RenderContext = new RenderContext();

            MediaPlayer.Volume = 1.0f;
            SoundEffect.MasterVolume = 0.1f;
        }

        public static void AddGameScene(GameScene gamescene)
        {
            if (!GameScenes.Contains(gamescene))
                GameScenes.Add(gamescene);
        }

        public static void RemoveGameScene(GameScene gamescene)
        {
            GameScenes.Remove(gamescene);

            if (ActiveScene == gamescene)
                ActiveScene = null;
        }

        public static bool SetActiveScene(string name)
        {
            var chosenscene = GameScenes.FirstOrDefault(scene =>
                scene.SceneName.Equals(name));

            if (chosenscene != null)
                ActiveScene = chosenscene;

            return chosenscene != null;
        }

        public static void Initialize()
        {
            GameScenes.ForEach(scene => scene.Initialize());
        }

        public static void PlaySong(int songnumber)
        {
            if (IsMusicPlaying)
            {
                if (songnumber == 1)
                {
                    MediaPlayer.Play(mainmusic);
                    MediaPlayer.IsRepeating = true;
                }
                else if (songnumber == 2)
                {
                    MediaPlayer.Play(levelmusic);
                    MediaPlayer.IsRepeating = false;
                }
            }
            else
            {
                if (MediaPlayer.State == MediaState.Playing)
                {
                    MessageBoxResult Choice;
                    Choice = MessageBox.Show("Media is currently playing music, do you want to stop it?", "Background Music", MessageBoxButton.OKCancel);

                    if (Choice == MessageBoxResult.OK)
                    {
                        if (songnumber == 1)
                        {
                            MediaPlayer.Play(mainmusic);
                            MediaPlayer.IsRepeating = true;
                        }
                        else if (songnumber == 2)
                        {
                            MediaPlayer.Play(levelmusic);
                            MediaPlayer.IsRepeating = false;
                        }
                        IsMusicPlaying = true;
                    }

                }
                else
                {
                    if (songnumber == 1)
                    {
                        MediaPlayer.Play(mainmusic);
                        MediaPlayer.IsRepeating = true;
                    }
                    else if (songnumber == 2)
                    {
                        MediaPlayer.Play(levelmusic);
                        MediaPlayer.IsRepeating = false;
                    }
                    IsMusicPlaying = true;
                }
            }
        }

        public static void LoadContent(ContentManager contentmanager)
        {
            push = contentmanager.Load<SoundEffect>("sfx\\button");
            pulse = contentmanager.Load<SoundEffect>("sfx\\pulse");
            pulse2 = contentmanager.Load<SoundEffect>("sfx\\pulse2");
            sux = contentmanager.Load<SoundEffect>("sfx\\sux");
            paper = contentmanager.Load<SoundEffect>("sfx\\paper");
            star = contentmanager.Load<SoundEffect>("sfx\\star");
            failed = contentmanager.Load<SoundEffect>("sfx\\failed");
            finish = contentmanager.Load<SoundEffect>("sfx\\finish");

            mainmusic = contentmanager.Load<Song>("song\\menu");
            levelmusic = contentmanager.Load<Song>("song\\level1");

            GameScenes.ForEach(scene => scene.LoadContent(contentmanager));
            //GameScenes.ForEach(scene => scene.LoadParticle(contentmanager, RenderContext.particleRenderer));
        }

        public static void Update(GameTime gametime, ContentManager contentmanager)
        {
            if (ActiveScene != null)
            {
                RenderContext.GameTime = gametime;
                ActiveScene.Update(RenderContext, contentmanager);

                // initialisasi scene
                if (!IsInitialized)
                {
                    var chosenscene = GameScenes.FirstOrDefault(scene =>
                        scene.SceneName.Equals("MainMenu"));
                    chosenscene.Update(RenderContext, contentmanager);

                    chosenscene = GameScenes.FirstOrDefault(scene =>
                        scene.SceneName.Equals("TitleScreen"));
                    chosenscene.Update(RenderContext, contentmanager);

                    chosenscene = GameScenes.FirstOrDefault(scene =>
                        scene.SceneName.Equals("LevelSelect"));
                    chosenscene.Update(RenderContext, contentmanager);

                    chosenscene = GameScenes.FirstOrDefault(scene =>
                        scene.SceneName.Equals("Level1"));
                    chosenscene.Update(RenderContext, contentmanager);

                    chosenscene = GameScenes.FirstOrDefault(scene =>
                        scene.SceneName.Equals("Archive"));
                    chosenscene.Update(RenderContext, contentmanager);

                    IsInitialized = true;
                }
            }
            RenderContext.TouchPanelState = TouchPanel.GetState();
        }

        public static void Draw()
        {
            if (ActiveScene != null)
            {
                //draw bg particle
                //if (ActiveScene.bg_particle != null)
                //{
                //    ActiveScene.DrawBGParticle(RenderContext);
                //}

                //draw HUD Belakang
                RenderContext.SpriteBatch.Begin();
                ActiveScene.DrawHUDBack(RenderContext);
                RenderContext.SpriteBatch.End();

                if (CameraManager.getInstance().camera.Focus == null)
                {
                    RenderContext.SpriteBatch.Begin();
                    ActiveScene.Draw(RenderContext);
                    RenderContext.SpriteBatch.End();
                }
                else
                {
                    RenderContext.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, CameraManager.getInstance().camera.Transform);
                    ActiveScene.Draw(RenderContext);
                    RenderContext.SpriteBatch.End();
                }

                //draw HUD Depan
                RenderContext.SpriteBatch.Begin();
                ActiveScene.DrawHUDFront(RenderContext);
                RenderContext.SpriteBatch.End();

                //ActiveScene.DrawParticle(RenderContext);
            }
        }
    }
}
