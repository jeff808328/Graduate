using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool DoingAction;
    public bool CancelAble;
    private Animator Animator;

    private void Start()
    {
        DoingAction = false;
        CancelAble = true;

        Animator = this.gameObject.GetComponent<Animator>();
    }

    public void DoingActionSet(int State)
    {
        if (State == 1)
            DoingAction = true;
        else if (State == 0)
            DoingAction = false;
    }

    public void CancelSet(int State)
    {
        if (State == 1)
            CancelAble = true;
        else if (State == 0)
            CancelAble = false;
    }

    public void ResetAttack(float Length)
    {
        StartCoroutine(AttackAbleReset(Length));
    }

    public void ResetRoll(float Length)
    {
        StartCoroutine(RollReset(Length));
    }

    IEnumerator AttackAbleReset(float Length)
    {
        yield return new WaitForSeconds(Length);

        gameObject.GetComponent<PlayerAttack>().Combo = 1;

        DoingAction = false;

        Animator.ResetTrigger("Attack1");
        Animator.ResetTrigger("Attack2");
        Animator.ResetTrigger("Attack3");
    }

    IEnumerator RollReset(float Length)
    {
        CancelSet(0);

        yield return new WaitForSeconds(Length);

        CancelSet(1);
    }
}
