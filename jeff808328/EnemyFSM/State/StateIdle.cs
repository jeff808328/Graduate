using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : EnemyBaseState
{
    
    public override void EnterState(EnemyStateManager StateManager)
    {
        Debug.Log("In Idle");

        StateManager.LastFlipTime = Time.time;

        StateManager.LastAttackTime = Time.time;
    }

    public override void UpdateState(EnemyStateManager StateManager)
    {
        if (StateManager.EnemyBackGroundData.FacePlayer)
        {
            StateManager.StateSwitch(StateManager.StateFightIdle);
        }
        else if (!StateManager.EnemyBackGroundData.FacePlayer && StateManager.LastFlipTime + StateManager.FlipCD < Time.time)
        {        
            Debug.Log("flip");

            StateManager.StateManagerFlip();
        }
    }

}
