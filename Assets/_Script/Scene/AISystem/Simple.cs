using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISystem;
using AISystem.States;
using UnityEngine;

namespace AISystem.States
{
    [Category("Character")]
    [System.Serializable]
    public class Simple : State
    {
        public bool applyRootMotion;
        protected Animator animator;
        protected UnityEngine.NavMeshAgent agent;
        public override void OnAwake()
        {
            animator = owner.GetComponent<Animator>();
            agent = owner.GetComponent<UnityEngine.NavMeshAgent>();
        }

        public override void OnEnter()
        {
            Debug.Log(agent.name + " enter state : " + name + ", time : " + Time.time);
        }

        public override void OnExit() 
        {
            Debug.Log(agent.name + " exit state : " + name + ", time : " + Time.time);
        }

        public override void OnAnimatorMove()
        {
            agent.updateRotation = !applyRootMotion;
            if (applyRootMotion)
            {
                owner.transform.rotation = animator.rootRotation;
                if (agent != null)
                {
                    agent.velocity = animator.deltaPosition / Time.deltaTime;
                }
            }
        }
    }
}
