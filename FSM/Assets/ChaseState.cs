using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : FSMState
{
    private Transform playerTrans;
    public ChaseState(FSMSystem fsm) : base(fsm)
    {
        _stateID = StateID.Chase;
        playerTrans = GameObject.Find("Player").transform;
    }
    
    /// <summary>
    /// 处理当前状态行为
    /// </summary>
    /// <param name="npc"></param>
    public override void Act(GameObject npc)
    {
        npc.transform.LookAt((playerTrans.position));
        npc.transform.Translate(Vector3.forward*2*Time.deltaTime);
    }

    /// <summary>
    /// 判断转换条件
    /// </summary>
    public override void Reason(GameObject npc)
    {
        if (Vector3.Distance(playerTrans.position, npc.transform.position) > 6)
        {
            fsm.preformTransition(Transition.LostPlayer);
        }    
    }
}
