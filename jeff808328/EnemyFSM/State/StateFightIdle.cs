using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFightIdle : EnemyBaseState
{
    public override void EnterState(EnemyStateManager StateManager)
    {
        Debug.Log("In FightIdle");
    }

    public override void UpdateState(EnemyStateManager StateManager)
    {
        if (StateManager.EnemyBackGroundData.FacePlayer)
        {
            //switch (StateManager.EnemyBackGroundData.ActionIndex)
            //{
            //    case 0:
            //        {
            //            // dash
            //            StateManager.StateSwitch(StateManager.StateDash);
            //            break;
            //        }
            //    case 1:
            //        {
            //            // jump
            //            StateManager.StateSwitch(StateManager.StateJump);
            //            break;
            //        }
            //    case 2:
            //        {
            //            // walk
            //            StateManager.StateSwitch(StateManager.StateWalk);
            //            break;
            //        }
            //}

            StateManager.StateSwitch(StateManager.StateWalk);

        }
        else if (!StateManager.EnemyBackGroundData.FacePlayer && StateManager.LastFlipTime + StateManager.FlipCD < Time.time)
        {
            Debug.Log("flip");

            StateManager.StateManagerFlip();

            if (!StateManager.EnemyBackGroundData.FacePlayer)
            {
                StateManager.StateSwitch(StateManager.StateIdle);
            }
        }
    }
}
