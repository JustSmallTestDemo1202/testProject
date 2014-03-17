using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IceFire
{
    public static class MyExtensions
    {
        public static Character GetCharacter(this GameObject go)
        {
            return go.GetComponent<Character>();
        }

        public static Character GetCharacter(this AIRuntimeController ai)
        {
            return ai.GetComponent<Character>();
        }

        public static Transform GetChild(this GameObject go, string name)
        {
            return GetChildRecurse(go.transform, name);
        }

        public static Transform GetChild(this MonoBehaviour comp, string name)
        {
            return GetChildRecurse(comp.transform, name);
        }

        public static Transform GetChild(this Character comp, string name)
        {
            return GetChildRecurse(comp.transform, name);
        }

        public static Transform GetChildRecurse(Transform trans, string name)
        {
            if (trans.name == name)
            {
                return trans;
            }

            foreach (Transform child in trans.transform)
            {
                Transform t = GetChildRecurse(child, name);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

    }
}