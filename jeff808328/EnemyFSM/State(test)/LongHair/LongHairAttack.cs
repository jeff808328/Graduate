using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairAttack : LongHairBaseState
{
    public override void EnterState(LongHairStateManager StateManager)
    {
        Debug.Log("In Attack");
    }

    public override void UpdateState(LongHairStateManager StateManager)
    {

    }
}
