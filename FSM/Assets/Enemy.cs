using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private FSMSystem fsm;
    void Start()
    {
        InitFSM();
    }

    void InitFSM()
    {
        fsm = new FSMSystem();
        FSMState patrolState = new PatrolState(fsm);
        patrolState.AddTranstion(Transition.SeePlayer,StateID.Chase);
        FSMState chaseState = new ChaseState(fsm);
        chaseState.AddTranstion(Transition.LostPlayer,StateID.Patrol);
        fsm.addState(patrolState);
        fsm.addState((chaseState));
    }

    // Update is called once per frame
    void Update()
    {
        fsm.Update(this.gameObject);
    }
}
