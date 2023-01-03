using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronAttack : CommonAttack
{
    void Start()
    {
        SetComponent();
    }
   
    void Update()
    {
        UpdataCollision();

        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        Gizmos.DrawWireCube(BoxCenter, BoxSize);
    }
}
