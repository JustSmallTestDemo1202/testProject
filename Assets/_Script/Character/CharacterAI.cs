using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Aegis
{

    public class CharacterAI : MonoBehaviour
    {
        StateMachine<GameObject> state;

        void Awake()
        {
            state = new StateMachine<GameObject>(gameObject);
            state.AddState(new Thinking());
            state.AddState(new Follow());
            state.AddState(new Patrol());
            state.AddState(new Attack());
        }

        void Update()
        {
            state.OnUpdate(Time.deltaTime);
        }
    }


    public class AIState : BaseState
    {
        float time_;

        public float ActiveTime
        {
            get { return time_; }
        }

        public AIState(string name)
            : base(name)
        {
        }

        public override void OnEnter(object param)
        {
            time_ = 0.0f;

            base.OnEnter(param);
        }

        public override void OnUpdate(float delta)
        {
            time_ += delta;
        }

    }

    public class Thinking : AIState
    {
        public Thinking()
            : base("Thinking")
        {
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);
        }

    }

    public class Follow : AIState
    {
        public Follow()
            : base("Follow")
        {
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);
        }

    }


    public class Patrol : AIState
    {
        public Patrol()
            : base("Patrol")
        {
        }

        public override void OnEnter(object param)
        {
             base.OnEnter(param);
        }

    }


    public class Attack : AIState
    {
        public Attack()
            : base("Attack")
        {
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);
        }

    }
}