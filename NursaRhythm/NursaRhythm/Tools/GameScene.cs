using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursaRhythm.Tools
{
    class GameScene
    {
        public string SceneName { get; private set; }
        public List<GameObject2D> SceneObjects2D { get; private set; }
        public List<GameObject2D> HUDObjects2DBack { get; private set; }
        public List<GameObject2D> HUDObjects2DFront { get; private set; }
        //public List<ObjectWithParticle> objectsWithParticle { get; set; }
        //public BackgroundParticle bg_particle;

        public GameScene(string scenename)
        {
            SceneName = scenename;
            SceneObjects2D = new List<GameObject2D>();
            HUDObjects2DBack = new List<GameObject2D>();
            HUDObjects2DFront = new List<GameObject2D>();
            //objectsWithParticle = new List<ObjectWithParticle>();
        }

        public void AddSceneObject(GameObject2D sceneobject)
        {
            if (!SceneObjects2D.Contains(sceneobject))
            {
                sceneobject.Scene = this;
                SceneObjects2D.Add(sceneobject);
            }
        }

        public void RemoveSceneObject(GameObject2D sceneobject)
        {
            if (SceneObjects2D.Remove(sceneobject))
            {
                sceneobject.Scene = null;
            }
        }

        public void AddHUDObjectBack(GameObject2D hudObject)
        {
            if (!HUDObjects2DBack.Contains(hudObject))
            {
                hudObject.Scene = this;
                HUDObjects2DBack.Add(hudObject);
            }
        }

        public void RemoveHUDObjectBack(GameObject2D hudObject)
        {
            if (HUDObjects2DBack.Remove(hudObject))
            {
                hudObject.Scene = null;
            }
        }

        public void AddHUDObjectFront(GameObject2D hudObject)
        {
            if (!HUDObjects2DFront.Contains(hudObject))
            {
                hudObject.Scene = this;
                HUDObjects2DFront.Add(hudObject);
            }
        }

        public void RemoveHUDObjectFront(GameObject2D hudObject)
        {
            if (HUDObjects2DFront.Remove(hudObject))
            {
                hudObject.Scene = null;
            }
        }

        //public void AddObjectWithParticle(ObjectWithParticle hudObject)
        //{
        //    if (!objectsWithParticle.Contains(hudObject))
        //    {
        //        objectsWithParticle.Add(hudObject);
        //    }
        //}

        //public void RemoveObjectWithParticle(ObjectWithParticle hudObject)
        //{
        //    if (objectsWithParticle.Remove(hudObject))
        //    {
        //    }
        //}

        public virtual void Initialize()
        {
            SceneObjects2D.ForEach(sceneobject => sceneobject.Initialize());
            HUDObjects2DBack.ForEach(hudobject => hudobject.Initialize());
            HUDObjects2DFront.ForEach(hudobject => hudobject.Initialize());
        }

        public virtual void Draw(RenderContext rendercontext)
        {
            SceneObjects2D.ForEach(sceneobject => sceneobject.Draw(rendercontext));
        }

        public virtual void DrawHUDBack(RenderContext rendercontext)
        {
            HUDObjects2DBack.ForEach(hudobject => hudobject.Draw(rendercontext));
        }

        public virtual void DrawHUDFront(RenderContext rendercontext)
        {
            HUDObjects2DFront.ForEach(hudobject => hudobject.Draw(rendercontext));
        }

        //public virtual void DrawParticle(RenderContext rendercontext)
        //{
        //    objectsWithParticle.ForEach(objectwithparticle => objectwithparticle.DrawParticle(rendercontext));
        //}

        //public virtual void DrawBGParticle(RenderContext rendercontext)
        //{
        //    bg_particle.DrawParticle(rendercontext);
        //}

        public virtual void LoadContent(ContentManager contentmanager)
        {
            SceneObjects2D.ForEach(sceneobject => sceneobject.LoadContent(contentmanager));
            HUDObjects2DBack.ForEach(hudobject => hudobject.LoadContent(contentmanager));
            HUDObjects2DFront.ForEach(hudobject => hudobject.LoadContent(contentmanager));
        }

        //public virtual void LoadParticle(Microsoft.Xna.Framework.Content.ContentManager contentmanager, SpriteBatchRenderer particleRenderer)
        //{
        //    objectsWithParticle.ForEach(objectwithparticle => objectwithparticle.LoadParticle(contentmanager, particleRenderer));
        //}

        public virtual void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            SceneObjects2D.ForEach(sceneobject => sceneobject.Update(rendercontext));
            HUDObjects2DBack.ForEach(hudobject => hudobject.Update(rendercontext));
            HUDObjects2DFront.ForEach(hudobject => hudobject.Update(rendercontext));
        }

        public virtual void ResetScene() { }

        public virtual void DoSomething() { }

        public virtual bool BackPressed() { return false; }
    }
}
