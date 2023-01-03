using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairHurt : LongHairBaseState
{
    public override void EnterState(LongHairStateManager StateManager)
    {
        Debug.Log("Hurt Triggered");
    }

    public override void UpdateState(LongHairStateManager StateManager)
    {

    }
}
