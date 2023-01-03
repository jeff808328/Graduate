using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAnimator : MonoBehaviour
{
    private Animator Animator;
    private CommonMove CommonMove;
    private GroundAndWallDetect GroundAndWallDetect;
    private PlayerState PlayerState;

    private string JumpTriggerName = "VerticalSpeed";
    private string RunTriggerName = "HorizonSpeed";


    void Start()
    {
        Animator = this.GetComponent<Animator>();

        CommonMove = this.GetComponent<CommonMove>();

        GroundAndWallDetect = this.GetComponent<GroundAndWallDetect>();

        PlayerState = this.GetComponent<PlayerState>();
    }

    void Update()
    {
        MoveSpeed();
        DirectionSet();
        WallTouchingTrigger();
        LandingTrigger();

        if (CommonMove.UseCancel)
            CancelAble();
    }

    public void JumpTrigger()
    {
        Animator.SetTrigger("Jump");
    }

    protected void LandingTrigger()
    {
        Animator.SetBool("GroundTouching", GroundAndWallDetect.GroundTouching);
    }

    protected void WallTouchingTrigger()
    {
        Animator.SetBool("WallTouching", GroundAndWallDetect.WallTouching);
    }

    protected void DirectionSet()
    {
        Animator.SetInteger("Direction", CommonMove.LastMoveDirection);
    }

    public void AttackTrigger(int i)
    {
        Animator.SetTrigger("Attack" + i.ToString());

        // Debug.Log("attack" + i);
    }

    public void AttackTrigger()
    {
        Animator.SetTrigger("Attack");
    }

    protected void MoveSpeed()
    {
        Animator.SetFloat(JumpTriggerName, CommonMove.VerticalSpeed);
        Animator.SetFloat(RunTriggerName, Mathf.Abs(CommonMove.HorizonSpeed));
    }

    protected void CancelAble()
    {
        Animator.SetBool("CancelAble", PlayerState.CancelAble);
    }

    public void RollTrigger()
    {
        Animator.SetTrigger("Roll");
    }

}
