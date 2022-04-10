using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : FSMState
{
    private List<Transform> path = new List<Transform>();
    private int index = 0;
    private Transform playerTrans;
    public PatrolState(FSMSystem fsm) : base(fsm)
    {
        _stateID = StateID.Patrol;
        Transform pathParent = GameObject.Find("Path").transform;
        Transform[] children = pathParent.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child != pathParent)
            {
                path.Add(child);
            }
        }

        playerTrans = GameObject.Find("Player").transform;
    }

    public override void Act(GameObject npc)
    {
        npc.transform.LookAt(path[index]);
        npc.transform.Translate((Vector3.forward*Time.deltaTime*3));
        if (Vector3.Distance(npc.transform.position, path[index].position) < 1)
        {
            index++;
            index %= path.Count;
        }
    }
    public override void Reason (GameObject npc)
    {
        if (Vector3.Distance(playerTrans.position, npc.transform.position) < 3)
        {
            fsm.preformTransition(Transition.SeePlayer);
        }
    }
}
