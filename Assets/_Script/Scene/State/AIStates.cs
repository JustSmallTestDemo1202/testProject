using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IceFire.AI
{
    public class AIState : BaseState
    {
        float time_;

        public float ActiveTime
        {
            get { return time_; }
        }

        public override void OnEnter(object param)
        {
            time_ = 0.0f;

         //   Debug.Log(character.name + " enter state : " + Name + ", time : " + Time.time);

            character.StartCoroutine(this.Start());
        }

        public virtual System.Collections.IEnumerator Start()
        {
           yield  return null;
        }

        public override void OnExit()
        {
         //   Debug.Log(character.name + " exit state " + Name + ", time : " + Time.time);
        }

        public override void OnUpdate(float delta)
        {
            time_ += delta;
        }

    }

    public class Thinking : AIState
    {
        public override void OnEnter(object param)
        {
            base.OnEnter(param);
        }

        public override void OnUpdate(float delta)
        {
            SceneObject obj = character.GetEnemy()[0];
            character.Target = null;
           
            if (obj != null)
            {
                Vector3 dis = obj.transform.position - transform.position;
                dis.y = 0;
                if (dis.magnitude < 8.0f)
                {
                    character.Target = obj.gameObject;
                }
            }

            if (target != null)
            {
                Manager.ChangeState("Follow");
            }
            else
            {
                if (ActiveTime > 2)
                {
                    Manager.ChangeState("Patrol");
                }

            }

            base.OnUpdate(delta);
        }

    }

    public class Follow : AIState
    {
        public override void OnUpdate(float delta)
        {
            Vector3 dis = target.transform.position - transform.position;
            dis.y = 0;
            float len = dis.magnitude;
            if (len < character.attackRange)
            {
                Manager.ChangeState("Attack");
            }
            else if (len < 8)
            {
                character.SetDestination(target.transform.position);
            }
            else
            {
                Manager.ChangeState("Thinking");
            }

            base.OnUpdate(delta);


        }

    }


    public class Patrol : Thinking
    {
        public float moveRange = 10.0f;
        public float maxMoveRange = 12;
        public float viewRange = 6;
        public Vector3 initPos;

        public override void OnInit()
        {
            initPos = transform.position;
        }

        public override void OnEnter(object param)
        {
            base.OnEnter(param);

            Vector2 move = UnityEngine.Random.insideUnitCircle * moveRange;
            Vector3 pos = new Vector3(initPos.x + move.x, initPos.y, initPos.z + move.y);
            if (pos.x < 1)
            {
                pos.x = 1;
            }

            if (pos.z < 1)
            {
                pos.z = 1;
            }

            character.SetDestination(pos);
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);
        }
    }


    public class Attack : AIState
    {
        bool exit = false;
        public override System.Collections.IEnumerator Start()
        {
            exit = false;
            character.OnIdle();
            yield return new WaitForSeconds(2.0f);

            while (true)
            {
                yield return new WaitForSeconds(2.0f);

                if (exit || target == null)
                {
                    break;
                }

                Vector3 dis = target.transform.position - transform.position;
                dis.y = 0;

                if (dis.magnitude > character.attackRange)
                {
                    break;
                }

                character.Attack(dis);

            }

   
            yield return null;
        }

        public override void OnUpdate(float delta)
        {

            Vector3 dis = target.transform.position - transform.position;
            dis.y = 0;
            if (dis.magnitude > character.attackRange)
            {
                Manager.ChangeState("Follow");
                exit = true;
            }

            base.OnUpdate(delta);
        }

    }
}
