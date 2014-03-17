using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IceFire
{
    public class Character : SceneObject
    {
        public float speed = 10.0f;

        [HideInInspector]
        public CharacterAnimation state;
        [HideInInspector]
        public CharacterMovement movement;
        [HideInInspector]
        public CharacterEffect fx;
        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public NavMeshAgent navAgent;


        protected Queue<Vector3> path = new Queue<Vector3>();
        public Queue<Vector3> Path
        {
            get { return path; }
        }

        protected GameObject target;
        public GameObject Target
        {
            get { return target; }
            set { target = value; }
        }

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();

            state = gameObject.AddComponent<CharacterAnimation>();
            movement = gameObject.AddComponent<CharacterMovement>();
            fx = gameObject.AddComponent<CharacterEffect>();
            navPath = new NavMeshPath();
        }


        public void OnIdle()
        {
            state.ChangeState("Idle");
        }

        public void OnWalk()
        {
            path.Clear();
            state.ChangeState("Walk");
        }

        public void OnJump()
        {
            state.ChangeState("Jump");
        }

        public void OnAttack()
        {
            state.ChangeState("Attack");
        }

        public void OnGetHit(Vector3 dir)
        {
            state.ChangeState("GetHit", dir);
        }

        public void OnDead()
        {
            state.ChangeState("Dead");
        }

        public void OnSkill(int type)
        {
            state.ChangeState("Skill" + type);
        }

        public void GoAndAttack(Vector3 pos)
        {
            path.Clear();
            path.Enqueue(pos);
            state.ChangeState("Run", pos);
        }

        public void Attack(Vector3 dir)
        {
            state.ChangeState("Attack", dir);
        }

        NavMeshPath navPath;
        public void SetDestination(Vector3 pos)
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(pos, out hit, 100, 255);
            NavMesh.CalculatePath(transform.position, pos, 255, navPath);
            path.Clear();

            foreach (Vector3 v in navPath.corners)
            {
                path.Enqueue(v);
            }

            state.ChangeState("Run", pos);
            //    navAgent.SetDestination(hit.position);
        }

        public void SetSpeed(float speed)
        {
            animator.SetFloat("Speed", speed);
            movement.moveSpeed = speed;

        }

        public void TurnDirction(Vector3 dir)
        {
            transform.rotation = Quaternion.LookRotation((Vector3)dir);
            movement.moveDirection = transform.forward;
        }


        public float attackRange = 3f;
        public float attackAngle = 180.0f;
        public bool autoAttack = true;

        void OnAttacked(object param)
        {
           // Debug.Log(name + " OnAttacked:" + param);
            // life -= 10;

            GameObject fx = EffectManager.Instance.SpawnFx("Prefab/Effect/hitEffect1");//, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            fx.transform.parent = this.GetChild("Bip01 Neck");
            fx.transform.localPosition = Vector3.zero;
            fx.transform.localRotation = Quaternion.identity;
    // fx.transform.parent = transform;
            GameObject attacker = param as GameObject;
       
            if (life <= 0)
            {
                SendMessage("OnDead", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Vector3 dir = transform.position - attacker.transform.position;
                dir.y = 0;
                dir.Normalize();

                SendMessage("ApplyAttributeDamage", new object[] { "Health", 1 }, SendMessageOptions.DontRequireReceiver);
                //rigidbody.AddForce(dir * attackRange, ForceMode.Impulse);
                SendMessage("OnGetHit", dir, SendMessageOptions.DontRequireReceiver);
            }


        }

        public List<SceneObject> GetVisbleEnemy()
        {
            List<SceneObject> objs = new List<SceneObject>();
            foreach (SceneObject obj in sceneObjects)
            {
                if (obj.isDead)
                {
                    continue;
                }

                if (tag == obj.tag)
                {
                    continue;
                }

                Vector3 dis = obj.transform.position - transform.position;
                float targetAngle = Vector3.Angle(dis, transform.forward);
                if (Mathf.Abs(targetAngle) > attackAngle / 2.0f)
                {
                    continue;
                }

                float len = dis.magnitude;
                if (len > attackRange)
                {
                    continue;
                }

                objs.Add(obj);
            }

            return objs;
        }

        public bool SeeEnemy
        {
            get { return GetVisbleEnemy().Count > 0; }
        }

        public List<SceneObject> GetEnemy()
        {
            Type enemyType = (this.GetType() == typeof(Player) ? typeof(Monster) : typeof(Player));
            List<SceneObject> targets = GetByType(enemyType);
            return targets;
        }

        public SceneObject GetClosestEnemy()
        {
            SceneObject enemy = null;
            float dis = 0;
            foreach (SceneObject obj in sceneObjects)
            {
                if (obj.isDead)
                {
                    continue;
                }

                if (tag == obj.tag)
                {
                    continue;
                }

                if (enemy == null)
                {
                    enemy = obj;
                    dis = Vector3.Distance(enemy.transform.position, transform.position);
                }
                else
                {
                    float d = Vector3.Distance(obj.transform.position, transform.position);
                    if (d < dis)
                    {
                        dis = d;
                        enemy = obj;
                    }

                }
            }

            return enemy;
        }

        #region  Key Event


        void AttackEvent(int type)
        {
            Type enemyType = (this.GetType() == typeof(Player) ? typeof(Monster) : typeof(Player));
            List<SceneObject> targets = GetByType(enemyType);
            if (targets != null)
            {
                foreach (SceneObject obj in targets)
                {
                    if (obj && obj.isDead)
                    {
                        continue;
                    }

                    Vector3 dis = obj.transform.position - transform.position;
                    float targetAngle = Vector3.Angle(dis, transform.forward);
                    if (Mathf.Abs(targetAngle) > attackAngle / 2.0f)
                    {
                        continue;
                    }

                    float len = dis.magnitude;
                    if (len > attackRange)
                    {
                        continue;
                    }

                    obj.SendMessage("OnAttacked", gameObject, SendMessageOptions.DontRequireReceiver);

                }
            }
        }

        void SendDamage(int param)
        {
            AttackEvent(param);
        }

        #endregion

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            if (path.Count > 1)
            {  
                Vector3[] points = path.ToArray();
                Gizmos.DrawLine(transform.position, points[0]);
              
                for (int i = 0; i < points.Length - 1; i++)
                {
                    Gizmos.DrawLine(points[i], points[i + 1]);
                }

            }

        }
    }


}