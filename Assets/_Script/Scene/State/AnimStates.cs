using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IceFire
{
    public enum AnimEvent : int
    {
        AnimChange,
        Reset
    }

    public class AnimState : BaseState
    {
        protected int nameHash;
        public override void OnInit()
        {
            nameHash = Animator.StringToHash(Name);
        }

        public override void OnEnter(object param)
        {
            animator.SetBool(nameHash, false);
        //       Debug.Log(character.name + " enter state : " + Name + ", time : " + Time.time);
        }

        public override void OnExit()
        {
            animator.SetBool(nameHash, false);
        //        Debug.Log(character.name + " exit state " + Name + ", time : " + Time.time);
        }

        public virtual void OnReset(object param)
        {
        }

        public virtual void OnAnimation(AnimatorStateInfo a)
        {
        }

        public override void OnEvent(int evt, object param)
        {
            if (evt == (int)AnimEvent.Reset)
            {
                OnReset(param);
            }
            else if (evt == (int)AnimEvent.AnimChange)
            {
                OnAnimation((AnimatorStateInfo)param);
            }
        }
    }

    public class Idle : AnimState
    {
        public override void OnEnter(object param)
        {
            base.OnEnter(param);

            character.SetSpeed(0.0f);

        }
    }


    public class Walk : AnimState
    {
        public override void OnEnter(object param)
        {
            base.OnEnter(param);

            character.SetSpeed(character.speed);
            
            if (param != null)
            {
             //   character.navAgent.SetDestination((Vector3)param);
            //    character.navAgent.velocity = character.speed;
                Vector3 dir = (Vector3)param - transform.position;
                dir.y = 0;
                character.TurnDirction(dir);
            }
        }

        public override void OnExit()
        {
//             character.navAgent.Stop();
//             character.navAgent.SetDestination(transform.position);
            base.OnExit();
        }

        public override void OnUpdate(float delta)
        {
//             if (character.navAgent.remainingDistance < 0.1f)
//             {
//                 character.OnIdle();
//             }
            UpdatePath();
            base.OnUpdate(delta);
        }

        public override void OnReset( object param)
        {
            if (param != null)
            {
            //    character.navAgent.SetDestination((Vector3)param);
                Vector3 dir = (Vector3)param - transform.position;
                dir.y = 0;
                character.TurnDirction(dir);
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

    public class Run : Walk
    {
        public override void OnEnter(object param)
        {
            base.OnEnter(param);
        }

        public override void OnUpdate(float delta)
        {
            if (character.autoAttack)
            {
                if (character.SeeEnemy)
                {
                    Manager.Change<Attack>();
                    return;
                }

            }

            base.OnUpdate(delta);
        }
    }

    public class Jump : AnimState
    {
        public override void OnEnter(object param)
        {
            base.OnEnter(param);
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);
        }
    }


    public class Attack : AnimState
    {
        public override void OnEnter(object param)
        {
            base.OnEnter(param);

            character.SetSpeed(0);

            if (param != null)
            {
                character.TurnDirction((Vector3)param);
            }
       
        }

    }

    public class Skill : AnimState
    {
        public override void OnEnter(object param)
        {
            base.OnEnter(param);
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);
        }
    }

    public class GetHit : AnimState
    {
        float force = 10.0f;
        public override void OnEnter(object param)
        {
            base.OnEnter(param);
            character.SetSpeed(0.0f);
            if (param != null)
            {
                transform.rotation = Quaternion.LookRotation(-(Vector3)param);
                character.movement.AddImpulse(force * (Vector3)param);
            }
  
        }

        public override void OnReset(object param)
        {
            if (param != null)
            {
                transform.rotation = Quaternion.LookRotation(-(Vector3)param);
                character.movement.AddImpulse(force * (Vector3)param);
            }
           
        }
    }

    public class Dead : AnimState
    {
        public override void OnEnter(object param)
        {
            base.OnEnter(param);

            character.SetSpeed(0.0f);
            character.gameObject.layer = LayerMask.NameToLayer("Dead");
        }
    }



}