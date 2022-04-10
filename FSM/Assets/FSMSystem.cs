using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem 
{
    private Dictionary<StateID, FSMState> states = new Dictionary<StateID, FSMState>();
    private StateID curStateID;
    private FSMState curState;

    public void addState(FSMState s)
    {
        if (s == null)
        {
            Debug.LogError("FSMSate不能为空");
            return;
        }

        if (curState == null)
        {
            curState = s;
            curStateID = s.ID;
        }

        if (states.ContainsKey(s.ID))
        {
            Debug.LogError("状态"+s.ID+"已经存在，无法重复添加");
            return;
        }
        states.Add(s.ID,s);
    }

    public void DeleteState(StateID id)
    {
        if (id == StateID.NullStateID)
        {
            Debug.LogError("无法删除空状态");
            return;
        }

        if (states.ContainsKey(id) == false)
        {
            Debug.LogError("无法删除不存在的状态"+id);
            return;
        }

        states.Remove((id));
    }

    public void preformTransition(Transition trans)
    {
        if (trans == Transition.NUllTransition)
        {
            Debug.LogError("无法执行空的转换条件");
            return;
        }

        StateID id = curState.GetOutPutState((trans));
        if (id == StateID.NullStateID)
        {
            Debug.LogWarning("当前状态"+curStateID+"无法根据转换条件"+trans+"发生转换");
            return;
        }

        if (states.ContainsKey(id) == false)
        {
            Debug.LogError("在状态机里面不存在状态"+id+"无法进行状态转换");
            return;
        }

        FSMState state = states[id];
        curState.DoAfterLeaving();
        curState = state;
        curStateID = id;
        curState.DoBeforeEntering();
    }

    public void Update(GameObject npc)
    {
        curState.Act(npc);
        curState.Reason(npc);
    }
}