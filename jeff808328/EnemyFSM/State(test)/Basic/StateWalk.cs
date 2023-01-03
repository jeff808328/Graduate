using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalk : EnemyBaseState
{
    public override void EnterState(EnemyStateManager StateManager)
    {
        Debug.Log("In Walk");
    }

    public override void UpdateState(EnemyStateManager StateManager)
    {
        // §ðÀ»§P©w 
        if (StateManager.EnemyBackGroundData.AttackAble&StateManager.LastAttackTime + StateManager.AttackCD < Time.time)
        {
            StateManager.StateSwitch(StateManager.StateAttack);
        }

        // ´Â¦V§P©w

        if (!StateManager.EnemyBackGroundData.FacePlayer && StateManager.LastFlipTime + StateManager.FlipCD < Time.time)
        {
            StateManager.StateManagerFlip();
            StateManager.MoveDirection = StateManager.EnemyBackGroundData.PlayerDirection;
        }

        StateManager.MoveDirection = StateManager.EnemyBackGroundData.PlayerDirection;
    }
}
