using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : EnemyBaseState
{
    private int MoveDirection;

    public override void EnterState(EnemyStateManager StateManager)
    {
        Debug.Log("Is Patrol");
      //  StateManager.EnemyMove.MoveDirection = StateManager.InitDirection;
    }

    public override void UpdateState(EnemyStateManager StateManager)
    {



        if (!StateManager.EnemyBackGroundData.FacePlayer)
        {
        //    StateManager.EnemyBackGroundData.RayCastDirection.x *= -1;
       //     StateManager.EnemyMove.MoveDirection *= -1;
        }
    }
}
