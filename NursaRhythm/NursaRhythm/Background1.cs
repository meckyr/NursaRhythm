using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
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
        private GameAnimatedSprite scenery1, scenery2, scenery3, scenery4, scenery5, scenery6, scenery7;
        private GameAnimatedSprite scenery8, scenery9, scenery10, scenery11, scenery12, scenery13, scenery14;

        public override void Initialize()
        {
            bg1 = new GameSprite("level1\\background1");
            AddChild(bg1);

            bg2 = new GameSprite("level1\\background2");
            bg2.Translate(800, 0);
            AddChild(bg2);

            scenery1 = new GameAnimatedSprite("level1\\scenery1", 8, 400, new Point(800, 234), 2);
            scenery1.PlayAnimation(true);
            scenery1.Translate(0, 250);
            scenery1.CanDraw = false;
            AddChild(scenery1);

            scenery2 = new GameAnimatedSprite("level1\\scenery1", 8, 400, new Point(800, 234), 2);
            scenery2.PlayAnimation(true);
            scenery2.Translate(800, 250);
            scenery2.CanDraw = false;
            AddChild(scenery2);

            scenery3 = new GameAnimatedSprite("level1\\scenery2", 8, 400, new Point(800, 234), 2);
            scenery3.PlayAnimation(true);
            scenery3.Translate(0, 250);
            scenery3.CanDraw = false;
            AddChild(scenery3);

            scenery4 = new GameAnimatedSprite("level1\\scenery2", 8, 400, new Point(800, 234), 2);
            scenery4.PlayAnimation(true);
            scenery4.Translate(800, 250);
            scenery4.CanDraw = false;
            AddChild(scenery4);

            scenery5 = new GameAnimatedSprite("level1\\scenery3", 8, 400, new Point(800, 234), 2);
            scenery5.PlayAnimation(true);
            scenery5.Translate(0, 250);
            scenery5.CanDraw = false;
            AddChild(scenery5);

            scenery6 = new GameAnimatedSprite("level1\\scenery3", 8, 400, new Point(800, 234), 2);
            scenery6.PlayAnimation(true);
            scenery6.Translate(800, 250);
            scenery6.CanDraw = false;
            AddChild(scenery6);

            scenery7 = new GameAnimatedSprite("level1\\scenery4", 8, 400, new Point(800, 234), 2);
            scenery7.PlayAnimation(true);
            scenery7.Translate(0, 250);
            scenery7.CanDraw = false;
            AddChild(scenery7);

            scenery8 = new GameAnimatedSprite("level1\\scenery4", 8, 400, new Point(800, 234), 2);
            scenery8.PlayAnimation(true);
            scenery8.Translate(800, 250);
            scenery8.CanDraw = false;
            AddChild(scenery8);

            scenery9 = new GameAnimatedSprite("level1\\scenery5", 8, 400, new Point(800, 234), 2);
            scenery9.PlayAnimation(true);
            scenery9.Translate(0, 250);
            scenery9.CanDraw = false;
            AddChild(scenery9);

            scenery10 = new GameAnimatedSprite("level1\\scenery5", 8, 400, new Point(800, 234), 2);
            scenery10.PlayAnimation(true);
            scenery10.Translate(800, 250);
            scenery10.CanDraw = false;
            AddChild(scenery10);

            scenery11 = new GameAnimatedSprite("level1\\scenery6", 8, 400, new Point(800, 234), 2);
            scenery11.PlayAnimation(true);
            scenery11.Translate(0, 250);
            scenery11.CanDraw = false;
            AddChild(scenery11);

            scenery12 = new GameAnimatedSprite("level1\\scenery6", 8, 400, new Point(800, 234), 2);
            scenery12.PlayAnimation(true);
            scenery12.Translate(800, 250);
            scenery12.CanDraw = false;
            AddChild(scenery12);

            scenery13 = new GameAnimatedSprite("level1\\scenery7", 8, 400, new Point(800, 234), 2);
            scenery13.PlayAnimation(true);
            scenery13.Translate(0, 250);
            scenery13.CanDraw = false;
            AddChild(scenery13);

            scenery14 = new GameAnimatedSprite("level1\\scenery7", 8, 400, new Point(800, 234), 2);
            scenery14.PlayAnimation(true);
            scenery14.Translate(800, 250);
            scenery14.CanDraw = false;
            AddChild(scenery14);

            base.Initialize();
        }

        public void DestroyScenery()
        {
            RemoveChild(scenery1);
            RemoveChild(scenery2);
            RemoveChild(scenery3);
            RemoveChild(scenery4);
            RemoveChild(scenery5);
            RemoveChild(scenery6);
            RemoveChild(scenery7);
            RemoveChild(scenery8);
            RemoveChild(scenery9);
            RemoveChild(scenery10);
            RemoveChild(scenery11);
            RemoveChild(scenery12);
            RemoveChild(scenery13);
            RemoveChild(scenery14);
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

            objectPosX = scenery1.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery1.Translate(objectPosX, 250);

            objectPosX = scenery2.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery2.Translate(objectPosX, 250);

            objectPosX = scenery3.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery3.Translate(objectPosX, 250);

            objectPosX = scenery4.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery4.Translate(objectPosX, 250);

            objectPosX = scenery5.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery5.Translate(objectPosX, 250);

            objectPosX = scenery6.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery6.Translate(objectPosX, 250);

            objectPosX = scenery7.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery7.Translate(objectPosX, 250);

            objectPosX = scenery8.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery8.Translate(objectPosX, 250);

            objectPosX = scenery9.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery9.Translate(objectPosX, 250);

            objectPosX = scenery10.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery10.Translate(objectPosX, 250);

            objectPosX = scenery11.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery11.Translate(objectPosX, 250);

            objectPosX = scenery12.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery12.Translate(objectPosX, 250);

            objectPosX = scenery13.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery13.Translate(objectPosX, 250);

            objectPosX = scenery14.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            scenery14.Translate(objectPosX, 250);

            TimingScenery();

            base.Update(renderContext);
        }

        private void TimingScenery()
        {
            if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(7.263))
            {
                if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(10.682))
                {
                    if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(14.129))
                    {
                        if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(17.515))
                        {
                            if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(27.717))
                            {
                                if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(38.136))
                                {
                                    if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(44.875))
                                    {
                                        if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(51.950))
                                        {
                                            if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(66.342))
                                            {
                                                if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(69.767))
                                                {
                                                    if (MediaPlayer.PlayPosition > TimeSpan.FromSeconds(73.172))
                                                    {


                                                        if (scenery13.LocalPosition.X >= 750)
                                                        {
                                                            scenery11.CanDraw = false;
                                                            scenery13.CanDraw = true;
                                                        }

                                                        if (scenery14.LocalPosition.X >= 750)
                                                        {
                                                            scenery12.CanDraw = false;
                                                            scenery14.CanDraw = true;
                                                        }

                                                        return;
                                                    }

                                                    if (scenery11.LocalPosition.X >= 750)
                                                    {
                                                        scenery9.CanDraw = false;
                                                        scenery11.CanDraw = true;
                                                    }

                                                    if (scenery12.LocalPosition.X >= 750)
                                                    {
                                                        scenery10.CanDraw = false;
                                                        scenery12.CanDraw = true;
                                                    }

                                                    return;
                                                }

                                                if (scenery9.LocalPosition.X >= 750)
                                                {
                                                    scenery7.CanDraw = false;
                                                    scenery9.CanDraw = true;
                                                }

                                                if (scenery10.LocalPosition.X >= 750)
                                                {
                                                    scenery8.CanDraw = false;
                                                    scenery10.CanDraw = true;
                                                }

                                                return;
                                            }

                                            if (scenery7.LocalPosition.X >= 750)
                                            {
                                                scenery5.CanDraw = false;
                                                scenery7.CanDraw = true;
                                            }

                                            if (scenery8.LocalPosition.X >= 750)
                                            {
                                                scenery6.CanDraw = false;
                                                scenery8.CanDraw = true;
                                            }

                                            return;
                                        }

                                        if (scenery5.LocalPosition.X >= 750)
                                        {
                                            scenery3.CanDraw = false;
                                            scenery5.CanDraw = true;
                                        }

                                        if (scenery6.LocalPosition.X >= 750)
                                        {
                                            scenery4.CanDraw = false;
                                            scenery6.CanDraw = true;
                                        }

                                        return;
                                    }

                                    if (scenery3.LocalPosition.X >= 750)
                                    {
                                        scenery1.CanDraw = false;
                                        scenery3.CanDraw = true;
                                    }

                                    if (scenery4.LocalPosition.X >= 750)
                                    {
                                        scenery2.CanDraw = false;
                                        scenery4.CanDraw = true;
                                    }

                                    return;
                                }

                                if (scenery1.LocalPosition.X >= 750)
                                {
                                    scenery7.CanDraw = false;
                                    scenery1.CanDraw = true;
                                }

                                if (scenery2.LocalPosition.X >= 750)
                                {
                                    scenery8.CanDraw = false;
                                    scenery2.CanDraw = true;
                                }

                                return;
                            }

                            if (scenery7.LocalPosition.X >= 750)
                            {
                                scenery5.CanDraw = false;
                                scenery7.CanDraw = true;
                            }

                            if (scenery8.LocalPosition.X >= 750)
                            {
                                scenery6.CanDraw = false;
                                scenery8.CanDraw = true;
                            }

                            return;
                        }

                        if (scenery5.LocalPosition.X >= 750)
                        {
                            scenery3.CanDraw = false;
                            scenery5.CanDraw = true;
                        }

                        if (scenery6.LocalPosition.X >= 750)
                        {
                            scenery4.CanDraw = false;
                            scenery6.CanDraw = true;
                        }

                        return;
                    }

                    if (scenery3.LocalPosition.X >= 750)
                    {
                        scenery1.CanDraw = false;
                        scenery3.CanDraw = true;
                    }

                    if (scenery4.LocalPosition.X >= 750)
                    {
                        scenery2.CanDraw = false;
                        scenery4.CanDraw = true;
                    }

                    return;
                }

                if (scenery1.LocalPosition.X >= 750)
                    scenery1.CanDraw = true;

                if (scenery2.LocalPosition.X >= 750)
                    scenery2.CanDraw = true;
            }
        }
    }
}
