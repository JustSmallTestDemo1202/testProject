using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IceFire
{

    public class BaseState : State<GameObject>
    {
        public BaseState()
        {
            String []name = this.GetType().ToString().Split('.');
            Name = name[name.Length - 1];
        }

        public BaseState(string name)
        {
            Name = name;
        }

        public Character character
        {
            get { return Manager.Agent.GetComponent<Character>(); }
        }

        public Animator animator
        {
            get { return Manager.Agent.GetComponent<Animator>(); }
        }

        public Transform transform
        {
            get { return Manager.Agent.transform; }
        }

        public GameObject target
        {
            get { return character.Target; }
        }

    }

}
