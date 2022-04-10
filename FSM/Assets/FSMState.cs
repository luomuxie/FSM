using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transition
{
    NUllTransition = 0,
    SeePlayer,
    LostPlayer,
}

public enum StateID
{
    NullStateID = 0,
    Patrol,
    Chase,
}


public abstract class FSMState
{
    protected StateID _stateID;
    public  StateID ID
    {
        get { return _stateID; }
    }

    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();

    protected FSMSystem fsm;

    public FSMState(FSMSystem fsm)
    {
        this.fsm = fsm;
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="id"></param>
    public void AddTranstion(Transition trans, StateID id)
    {
        if (trans == Transition.NUllTransition)
        {
            Debug.LogError("不允许NUllTransition");
            return;
        }

        if (id == StateID.NullStateID)
        {
            Debug.LogError("不允许NullStateID");
            return;
        }
        if (map.ContainsKey(trans))
        {
            Debug.LogError("添加转换条件的时候，"+trans+"已经存在于map中");
            return;
        }
        map.Add(trans,id);
    }

    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="trans"></param>
    public void DeleteTransition(Transition trans)
    {
        if (trans == Transition.NUllTransition)
        {
            Debug.LogError("不允许NUllTransition");
            return;
        }

        if (map.ContainsKey(trans) == false)
        {
            Debug.LogError("删除条件的时候，"+trans+"不存在于map中");
        }

        map.Remove(trans);
    }

    /// <summary>
    /// 根据条件判断触发状态
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public StateID GetOutPutState(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }
        return StateID.NullStateID;
    }
    
    /// <summary>
    /// 离开前
    /// </summary>
    public virtual void DoBeforeEntering(){}
    /// <summary>
    /// 离开后
    /// </summary>
    public virtual  void DoAfterLeaving(){}
    /// <summary>
    /// 处理当前状态行为
    /// </summary>
    /// <param name="npc"></param>
    public abstract void Act(GameObject npc);
    /// <summary>
    /// 判断转换条件
    /// </summary>
    public abstract void Reason(GameObject npc);
}
