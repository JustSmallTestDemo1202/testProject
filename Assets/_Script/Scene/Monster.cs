using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IceFire;
using UnityEngine;

namespace IceFire
{
    public class Monster : Character
    {
        //protected CharacterAI ai;

        protected override void Awake()
        {
            base.Awake();

            autoAttack = false;
            speed = 3.0f;
        //    ai = gameObject.AddComponent<CharacterAI>();
 
        }

        void Update()
        {           
        }
    }

}