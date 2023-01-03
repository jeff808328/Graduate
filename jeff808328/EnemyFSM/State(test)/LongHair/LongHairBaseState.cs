using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LongHairBaseState
{
    public abstract void EnterState(LongHairStateManager StateManager);

    public abstract void UpdateState(LongHairStateManager StateManager);
}
