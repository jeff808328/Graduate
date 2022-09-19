using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CommonMove
{
    private int JumpTime;

    void Update()
    {
        GroundTouching = GroundAndWallDetect.GroundTouching;

        if (Input.GetKey(KeyCode.A))
            HorizonVelocity(-1);
        else if (Input.GetKey(KeyCode.D))
            HorizonVelocity(1);
        else
            MiunsSpeed();

        if (Input.GetKeyDown(KeyCode.Space) && JumpTime < MaxJumpTimes)
        { 
            CommonAnimator.JumpTrigger();
            VerticalVelocity();

            JumpTime++;
        }

        GravityEffect();

        if (GroundTouching)
        {
            VerticalSpeed = Mathf.Clamp(VerticalSpeed, 0, VerticalSpeedMax);
            JumpTime = 0;
        }
            

        FinalSpeed = new Vector2(HorizonSpeed, VerticalSpeed);
        Rd.velocity = FinalSpeed;
    }
}
