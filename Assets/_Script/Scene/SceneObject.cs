using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IceFire
{


    public class SceneObject : MonoBehaviour
    {
        protected static List<SceneObject> sceneObjects = new List<SceneObject>();

        public int id;
        protected float life = 100;

        public bool isDead
        {
            get { return life <= 0; }
        }

        public virtual void Init()
        {
        }

        protected virtual void OnEnable()
	    {
            sceneObjects.Add(this);
	    }

        protected virtual void OnDisable()
        {
            sceneObjects.Remove(this);
        }

        public static List<SceneObject> GetByTag(string tag)
        {
            return sceneObjects.FindAll(x => x.tag == tag);
        }

        public static List<SceneObject> GetByType<T>()
        {
            return sceneObjects.FindAll(x => x.GetType() == typeof(T));
        }

        public static List<SceneObject> GetByType(Type type)
        {
            return sceneObjects.FindAll(x => x.GetType() == type);
        }
    }
}
