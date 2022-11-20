using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyBaseState CurrentState;

    private StateIdle StateIdle = new StateIdle();
    private StateFightIdle FightIdle = new StateFightIdle();
    private StatePatrol StatePatrol = new StatePatrol();
    private StateWalk StateWalk = new StateWalk();
    private StateAttack StateAttack = new StateAttack();
    private StateStartAnimaiton StartAnimaiton = new StateStartAnimaiton();

    void Start()
    {
        CurrentState = StartAnimaiton;
        CurrentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void StateSwitch(EnemyBaseState NextState)
    {
        CurrentState = NextState;
        NextState.EnterState(this);
    }
}
