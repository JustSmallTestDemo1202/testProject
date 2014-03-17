using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using IceFire;

public class CharacterAnimation : MonoBehaviour, IStateManager<GameObject>
{
    Dictionary<int, IState<GameObject>> stateDict = new Dictionary<int, IState<GameObject>>();
    Animator animator;
    Dictionary<int, object> parms = new Dictionary<int, object>();

    int nextState = -1;
    int currentState;
    int currentAnim;

    void Awake()
    {
        animator = GetComponent<Animator>();

        AddState(new Idle());
        AddState(new Walk());
        AddState(new Run());
        AddState(new Jump());
        AddState(new Attack());
        AddState(new Skill());
        AddState(new GetHit());
        AddState(new Dead());

    }

    void Start()
    {
    }

    public void SetAnimator(string fileName)
    {
        animator.runtimeAnimatorController = Resources.Load(fileName) as RuntimeAnimatorController;
    }

    public object this[int key]
    {
        get
        {
            object p;
            if (parms.TryGetValue(key, out p))
            {
                return p;
            }
            else
            {
                return null;
            }
        }
    }

    void Update()
    {

        AnimatorStateInfo anim = animator.IsInTransition(0) ? animator.GetNextAnimatorStateInfo(0) : animator.GetCurrentAnimatorStateInfo(0);
        int hash = anim.tagHash;
        if (hash != currentState)
        {
            IState<GameObject> state;
            if (stateDict.TryGetValue(currentState, out state))
            {
                state.OnExit();
            }

            currentState = hash;
            if (currentState == nextState)
            {
                nextState = -1;
            }

            if (stateDict.TryGetValue(currentState, out state))
            {
                object param;
                if (parms.TryGetValue(currentState, out param))
                {
                    state.OnEnter(param);
                }
                else
                {
                    state.OnEnter(null);
                }
            }
        }

        if (anim.nameHash != currentAnim)
        {
            currentAnim = anim.nameHash;

            IState<GameObject> state;
            if (stateDict.TryGetValue(currentState, out state))
            {
                state.OnEvent((int)AnimEvent.AnimChange, anim);
            }
        }
  

        IState<GameObject> currState = GetCurrentState();
        if (currState != null)
        {
            currState.OnUpdate(Time.deltaTime);
        }

    }

    #region IStateManager<GameObject> 成员

    public GameObject Agent
    {
        get
        {
            return gameObject;
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public IState<GameObject> GetCurrentState()
    {
        IState<GameObject> state;
        if(stateDict.TryGetValue(currentState, out state ))
        {
            return state;
        }

        return null;
    }

    public void AddState(IState<GameObject> state)
    {
        int hash = Animator.StringToHash(state.Name);
    //    animator.ResetTrigger(hash);

        IState<GameObject> oldState;
        if (stateDict.TryGetValue(currentState, out oldState))
        {
            return;
        }

        state.Manager = this;
        state.OnInit();
        stateDict[hash] = state;
    }

    public void RemoveState(IState<GameObject> state)
    {
        throw new NotImplementedException();
    }
    
    public void ChangeState(string name, object param = null)
    {
        int hash = Animator.StringToHash(name);
        if (hash == currentState)
        {
            IState<GameObject> state;
            if (stateDict.TryGetValue(currentState, out state))
            {
                state.OnEvent((int)AnimEvent.Reset, param);
            }
        }
        else
        {

            if (nextState != hash)
            {
                parms[hash] = param;
                nextState = hash;
                animator.SetBool(nextState, true);
            }
    
        }

    }

    #endregion

    #region IStateManager<GameObject> 成员


    public void Change<K>()
    {
        String[] name = typeof(K).ToString().Split('.');
        ChangeState(name[name.Length - 1]);
    }

    #endregion
}

