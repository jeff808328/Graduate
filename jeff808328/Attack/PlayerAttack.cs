using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : CommonAttack
{
    public int Combo;
    [SerializeField] private bool ComboAble;
    public float ComboLimitTime;

    void Update()
    {
        UpdataCollision();

        if (Input.GetMouseButtonDown(0))
        {
            if (ComboAble & Combo < 4 & Time.time > LastAttackTime + ComboLimitTime)
            {
                ComboSet(0);
                Animator.AttackTrigger(Combo);
                Combo++;
            }
        }

        //if (!this.gameObject.GetComponent<PlayerState>().DoingAction)
        //{
        //    if (Combo > 4)
        //    {
        //       gameObject.GetComponent<PlayerState>().ResetAttack(AttackCD);
        //    }
        //}

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(BoxCenter, BoxSize);
    }

    public void ComboSet(int State)
    {
        if (State == 1)
            ComboAble = true;
        else if (State == 0)
            ComboAble = false;
    }

 
}
