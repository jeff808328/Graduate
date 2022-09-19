using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAnimator : MonoBehaviour
{
    private Animator Animator;
    private CommonMove MoveDetail;
    private GroundAndWallDetect MoveStateDetect;

    private string JumpTriggerName = "VerticalSpeed";
    private string RunTriggerName = "HorizonSpeed";


    void Start()
    {
        Animator = this.GetComponent<Animator>();

        MoveDetail = this.GetComponent<CommonMove>();

        MoveStateDetect = this.GetComponent<GroundAndWallDetect>();
    }


    void Update()
    {
        MoveSpeed();

        LandingTrigger();
    }

    public void JumpTrigger()
    {
        Animator.SetTrigger("Jump");
    }

    protected void LandingTrigger()
    {
        Animator.SetBool("GroundTouching", MoveStateDetect.GroundTouching);
    }

    public void AttackTrigger()
    {
        Animator.SetTrigger("Attack");
    }

    protected void MoveSpeed()
    {
        Animator.SetFloat(JumpTriggerName, MoveDetail.VerticalSpeed);
        Animator.SetFloat(RunTriggerName, Mathf.Abs(MoveDetail.HorizonSpeed));
    }

}
