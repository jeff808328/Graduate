using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : CommonAttack
{
    void Update()
    {
        UpdataCollision();

        if (Input.GetMouseButtonDown(0) && LastAttackTime + AttackCD <= Time.time)
        {
            Attack();

            Animator.SetTrigger("Attack");

            LastAttackTime = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(BoxCenter, BoxSize);
    }
}
