using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.CharacterController))]
public class Character : MonoBehaviour
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
        animator.SetFloat("Speed", 0);
        state.ChangeState("Idle");
    }

    public void OnWalk(Vector3 pos)
    {
        path.Clear();
        path.Enqueue(pos);
        state.ChangeState("Walk", pos);
    }

    public void OnJump()
    {
        state.ChangeState("Jump");
    }

    public void OnAttack(Vector3 dir)
    {
        state.ChangeState("Attack", dir);
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
        state.ChangeState(name);
    }

    public void OnMove(Vector3 dir)
    {
        state.ChangeState("Walk", dir);
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
        state.ChangeState("Walk", pos);
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

    public int id;
    public float attackRange = 4f;
    public float attackAngle = 180.0f;
    protected float life = 100;
 
    public bool isDead
    {
        get { return life <= 0; }
    }

    void OnAttacked(object param)
    {
        Debug.Log(name + " OnAttacked:" + param);
       // life -= 10;

        GameObject fx = EffectManager.Instance.SpawnFx("Prefab/Effect/hitEffect1");//, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        fx.transform.parent = this.GetChild("Bip01 Neck");
        fx.transform.localPosition = Vector3.zero;
        fx.transform.localRotation = Quaternion.identity;

        GameObject attacker = param as GameObject;
        fx.transform.parent = transform;
        if (life <= 0)
        {
            SendMessage("OnDead", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Vector3 dir = transform.position - attacker.transform.position;
            dir.y = 0;
            dir.Normalize();
            //rigidbody.AddForce(dir * attackRange, ForceMode.Impulse);
            SendMessage("OnGetHit", dir, SendMessageOptions.DontRequireReceiver);
        }
        /*
        AIRuntimeController behaviour = GetComponent<AIRuntimeController>();
        //Check if we hit the enemy
        if (behaviour)
        {
            //Yes we hit the enemy, so lets get the health attribute
            BaseAttribute defenderAttribute = behaviour.GetAttribute("Health");
            //Is the health attribute defined in the AI Editor?
            if (defenderAttribute != null)
            {
                //Yes the attribute is defined in the AI Editor,
                //so lets consume the damage from it.
                defenderAttribute.Consume(1);
            }
        }*/


    }

#region  Key Event


    void AttackEvent(int type)
    {
        string enemyTag = (this.tag == "Player" ? "Monster" : "Player");
        GameObject[] targets = GameObject.FindGameObjectsWithTag(enemyTag);
        if (targets != null)
        {
            foreach (GameObject obj in targets)
            {
                Character ctrl = obj.GetComponent<Character>();
                if (ctrl && ctrl.isDead)
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
            Vector3 []points = path.ToArray();
            for (int i = 0; i < points.Length - 1; i++)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
            }

        }
       
    }
}

