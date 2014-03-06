using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Aegis;

[HideInInspector]
public class CharacterAnimation : MonoBehaviour, IStateManager<GameObject>
{
    Dictionary<int, IState<GameObject>> stateDict = new Dictionary<int, IState<GameObject>>();
    Animator animator;
    Dictionary<string, object> parms = new Dictionary<string, object>();
    int currentTag;
    int currentAnim;
    
    void Awake()
    {
        animator = GetComponent<Animator>();

        AddState(new IdleState());
        AddState(new Aegis.WalkState());
        AddState(new JumpState());
        AddState(new Aegis.AttackState());
        AddState(new SkillState());
        AddState(new GetHitState());
        AddState(new DeadState());

  //      animator.runtimeAnimatorController = Resources.Load("Animator/Ratkin Shaman") as RuntimeAnimatorController;
        //UnityEditorInternal.AnimatorController ctrl = animator.runtimeAnimatorController as UnityEditorInternal.AnimatorController;
       // ctrl.AddParameter("test", UnityEditorInternal.AnimatorControllerParameterType.Bool);
      //  animator.runtimeAnimatorController = ctrl;
    }

    void Start()
    {
    }

    public object this[string key]
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

        if (anim.tagHash != currentTag)
        {
            IState<GameObject> state;
            if (stateDict.TryGetValue(currentTag, out state))
            {
                state.OnExit();
            }

            currentTag = anim.tagHash;
 
            if (stateDict.TryGetValue(currentTag, out state))
            {
                object param;
                if (parms.TryGetValue(state.Name, out param))
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
            if (stateDict.TryGetValue(currentTag, out state))
            {
                state.OnEvent(new Aegis.AnimEvent(anim));
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
        if(stateDict.TryGetValue(currentTag, out state ))
        {
            return state;
        }

        return null;
    }

    public void AddState(IState<GameObject> state)
    {
        int hash = Animator.StringToHash(state.Name);
        animator.ResetTrigger(hash);

        IState<GameObject> oldState;
        if (stateDict.TryGetValue(currentTag, out oldState))
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
        if (hash == currentTag)
        {
            IState<GameObject> state;
            if (stateDict.TryGetValue(currentTag, out state))
            {
                state.OnEvent(new Aegis.ResetEvent(param));
            }
        }
        else
        {
            parms[name] = param;
            animator.SetBool(name, true);
        }

    }

    #endregion
}

