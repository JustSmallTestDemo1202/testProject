using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using UnityEngine;

public interface IState<T>
{
    string Name{get;}
    IStateManager<T> Manager { get; set; }

    void OnInit();
    void OnEnter(object param);
    void OnExit();
    void OnUpdate(float deltaTime);
    void OnEvent(int evt, object param);

}

public interface IStateManager<T>
{
    T Agent { get; set; }
    IState<T> GetCurrentState();
    void AddState(IState<T> state);
    void RemoveState(IState<T> state);
    void ChangeState(string name, object param = null);
    void Change<K>();
}

public class State<T> : IState<T>
{
    IStateManager<T> manager_;
    string name_;

    public State()
    {
    }
    
    public State(string name)
    {
        name_ = name;
    }

    public string Name
    {
        get
        {
            return name_;
        }
        set
        {
            name_ = value;
        }
    }

    public IStateManager<T> Manager
    {
        get
        {
            return manager_;
        }

        set
        {
            manager_ = value;
        }
    }

    public virtual void OnInit()
    {
    }

    public virtual void OnEnter(object param)
    { 
    }

    public virtual void OnExit()
    {
    }

    public virtual void OnUpdate(float deltaTime)
    {
    }

    public virtual void OnEvent(int evt, object param)
    {
    }


}

public class ProxyState<T>: State<T> 
{
    public Action<float> Updating;
    public Action Starting;
    public Action Finishing;

    public ProxyState(string name)
        : base(name)
    {
    }

    public override void OnEnter(object param)
    {
        base.OnEnter(param);

        if (Starting != null)
        {
            Starting();
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        if (Finishing != null)
        {
            Finishing();
        }
    }

    public override void OnUpdate(float delta)
    {
        if (Updating != null)
        {
            Updating(delta);
        }

        base.OnUpdate(delta);
    }


}

public class StateMachine<T> : State<T>, IStateManager<T>
{
    T holder_;
    System.Collections.Generic.SortedDictionary<string, IState<T>> stateDict_;
    IState<T> currentState_;
    IState<T> nextState_;
    bool inited_ = false;

    public StateMachine(string name) : base(name)
    {
        stateDict_ = new System.Collections.Generic.SortedDictionary<string, IState<T>>();
    }

    public StateMachine(T holder)
    {
        holder_ = holder;
        stateDict_ = new System.Collections.Generic.SortedDictionary<string, IState<T>>();
    }

    public T Agent
    {
        get
        {
            if (holder_ != null)
            {
                return holder_;
            }

            if (Manager != null)
            {
                return Manager.Agent;
            }

            return holder_;
        }
        set
        {
            holder_ = value;
        }
    }

    public IState<T> GetCurrentState()
    {
        return currentState_;
    }

    public string CurrentState
    {
        get { return currentState_ == null ? "" : currentState_.Name; }
    }

    public void AddState(IState<T> state)
    {
        if (stateDict_.ContainsKey(state.Name))
        {
         //   Debug.Assert(false, "state with nam \"" + state.Name + "\"already exist.");
        }

        stateDict_[state.Name] = state;
        state.Manager = this;

        if (inited_)
        {
            state.OnInit();
        }
    }

    public void RemoveState(IState<T> state)
    {
        stateDict_.Remove(state.Name);
    }

    public void ChangeState(string name, object param = null)
    {
        IState<T> state;
        if(stateDict_.TryGetValue(name, out state))
        {
            ChangeState(state, param);
        }
    }

    public void ChangeState(IState<T> state, object param = null)
    {
        if (state != currentState_)
        {
            if (currentState_ != null)
            {
                currentState_.OnExit();
            }

            currentState_ = state;
            nextState_ = state;

            if (currentState_ != null)
            {
                currentState_.OnEnter(param);
            }
        }

  
    }

    #region State interface

    public override void OnInit()
    {
        foreach (IState<T> state in stateDict_.Values)
        {
            state.OnInit();
        }

        inited_ = true;
    }

    public override void OnEnter(object param)
    {
        if (currentState_ != null)
        {
            currentState_.OnEnter(param);
        }
    }

    public override void OnExit()
    {
        if (currentState_ != null)
        {
            currentState_.OnExit();
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        if (currentState_ != null)
        {
            currentState_.OnUpdate(deltaTime);
        }
    }

    public override void OnEvent(int evt, object param)
    {
        if (currentState_ != null)
        {
            currentState_.OnEvent(evt, param);
        }
    }

    #endregion

    #region IStateManager<T> ≥…‘±


    public void Change<K>()
    {
        String[] name = typeof(K).ToString().Split('.');
        ChangeState(name[name.Length - 1]);
    }

    #endregion
}

