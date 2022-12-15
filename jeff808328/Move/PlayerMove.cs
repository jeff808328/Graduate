using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CommonMove
{
    private PlayerState PlayerState;

    private void Start()
    {
        SetData();

        SetComponent();

        PlayerState = this.GetComponent<PlayerState>();
    }

    void Update()
    {
        GroundTouching = GroundAndWallDetect.GroundTouching;
        // 地板偵測更新

        if (GroundTouching)
        {
            VerticalSpeed = Mathf.Clamp(VerticalSpeed, 0, VerticalSpeedMax);
            JumpTime = 0;
        }
        // 跳躍狀態重置

        if (!PlayerState.DoingAction)
        {
            if (Input.GetKey(KeyCode.A))
                HorizonVelocity(-1);
            else if (Input.GetKey(KeyCode.D))
                HorizonVelocity(1);
            else
                MiunsSpeed(); //沒按按鍵就開始減速
                              // 左右走加轉向

            if (Input.GetKeyDown(KeyCode.Space) && JumpTime < MaxJumpTimes)
            {
                CommonAnimator.JumpTrigger();
                VerticalVelocity();

                JumpTime++;
            }
            // 跳躍
        }

        if (Input.GetKey(KeyCode.R) && PlayerState.CancelAble)
        {
            Debug.Log("ROLL");


            CommonAnimator.RollTrigger();

            PlayerState.ResetRoll(RollCD);
        }
        // 迴避，翻滾，動作取消

        GravityEffect();
        // 重力計算

        FinalSpeed = new Vector2(HorizonSpeed, VerticalSpeed);
        Rd.velocity = FinalSpeed;
        // 移動速度計算
    }
}
