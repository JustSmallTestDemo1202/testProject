using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IceFire
{

    public class CharacterAI : MonoBehaviour
    {
        StateMachine<GameObject> state;
 
        void Awake()
        {
            state = new StateMachine<GameObject>(gameObject);
            state.AddState(new AI.Thinking());
            state.AddState(new AI.Follow());
            state.AddState(new AI.Attack());
            state.AddState(new AI.Patrol());
            ChangeState<AI.Thinking>();
        }

        public void ChangeState<T>()
        {
            String[] name = typeof(T).ToString().Split('.');
            state.ChangeState(name[name.Length - 1]);
        }

        void Update()
        {
           state.OnUpdate(Time.deltaTime);
        }
    }

}