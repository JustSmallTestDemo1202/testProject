using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Aegis
{
    public class AnimEvent : EventArgs
    {
        public AnimatorStateInfo anim;
        public AnimEvent(AnimatorStateInfo a)
        {
            anim = a;
        }
    }

    public class ResetEvent : EventArgs
    {
        public object param;
        public ResetEvent(object p)
        {
            param = p;
        }
    }

    public class BaseState : State<GameObject>
    {
        public static int idleHash = Animator.StringToHash("Base Layer.Idle");
        protected int nameHash;

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

        public override void OnInit()
        {
            nameHash = Animator.StringToHash(Name);
        }

        public override void OnEnter(object param)
        {
            animator.SetBool(nameHash, false);
        //    Debug.Log(character.name + " enter state : " + Name + ", time : " + Time.time);
        }

        public override void OnExit()
        {
            animator.SetBool(nameHash, false);
        //    Debug.Log(character.name + " exit state " + Name + ", time : " + Time.time);
        }

    }


    public class IdleState : BaseState
    {
        public IdleState()
            : base("Idle")
        {
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);

        //    character.MoveSpeed = 0.0f;
            character.SetSpeed(0.0f);

        }
    }


    public class WalkState : BaseState
    {
        public WalkState()
            : base("Walk")
        {
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);

            character.SetSpeed(character.speed);
 
            if (param != null)
            {      
                //  character.navAgent.SetDestination((Vector3)param);
                character.TurnDirction((Vector3)param);
            }
        }

        public override void OnUpdate(float delta)
        {
            UpdatePath();
            base.OnUpdate(delta);
        }

        public override void OnExit()
        {        
            base.OnExit();
        }

        public override void OnEvent(EventArgs e)
        {
            if (e.GetType() == typeof(ResetEvent))
            {
                ResetEvent evt = e as ResetEvent;
                if (evt.param != null)
                {
                    //  character.navAgent.SetDestination((Vector3)param);
                    character.TurnDirction((Vector3)evt.param);
                }
               
            }
        }

        void UpdatePath()
        {
            Character c = character;

            if (c.Path.Count == 0)
            {
                return;
            }

            Vector3 dir = c.Path.Peek() - c.transform.position;
            dir.y = 0;
            float dis = dir.magnitude;
            if (dis > 0.5f)
            {
                dir = dir / dis;
                character.TurnDirction(dir);
            }
            else
            {
                c.Path.Dequeue();
              
                if (c.Path.Count == 0)
                {
                    c.OnIdle();
                }
            }
        }
    }

    public class JumpState : BaseState
    {
        public JumpState()
            : base("Jump")
        {
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);
        }
    }


    public class AttackState : BaseState
    {
        public AttackState()
            : base("Attack")
        {
        }

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);

            character.SetSpeed(0);

            if (param != null)
            {
                character.TurnDirction((Vector3)param);  
            }
       
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);
        }
    }
   
    public class SkillState : BaseState
    {
        public SkillState()
            : base("Skill")
        {
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);
        }
    }

    public class GetHitState : BaseState
    {
        float force = 10.0f;

        public GetHitState()
            : base("GetHit")
        {
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);

            if (param != null)
            {
                transform.rotation = Quaternion.LookRotation(-(Vector3)param); 
                character.movement.AddImpuse(force*(Vector3)param);
            }
  
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);
        }
    }

    public class DeadState : BaseState
    {
        public DeadState()
            : base("Dead")
        {            
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);

            character.SetSpeed(0.0f);
            character.gameObject.layer = LayerMask.NameToLayer("Dead");
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);
        }
    }



}