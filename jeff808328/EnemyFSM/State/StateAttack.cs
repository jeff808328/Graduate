using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : EnemyBaseState
{

    public override void EnterState(EnemyStateManager StateManager)
    {
        Debug.Log("In Attack");
    }

    public override void UpdateState(EnemyStateManager StateManager)
    {
        StateManager.MoveDirection = 0;
        StateManager.StateManagerAttack();// ����
        Debug.Log("attack");
  
        StateManager.StateSwitch(StateManager.StateFightIdle);// �������A
        
    }


}
