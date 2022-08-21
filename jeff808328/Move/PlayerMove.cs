using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CommonMove
{
    // Update is called once per frame
    void Update()
    {
        GroundTouching = this.GetComponent<MoveStateDetect>().GroundTouching;

        if (Input.GetKey(KeyCode.A))
            HorizonVelocity(-1);
        else if (Input.GetKey(KeyCode.D))
            HorizonVelocity(1);
        else
            MiunsSpeed();

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            CommonAnimator.JumpTrigger();
            VerticalVelocity();          
        }

        GravityEffect();

        if (GroundTouching)
            VerticalSpeed = Mathf.Clamp(VerticalSpeed, 0, VerticalSpeedMax);

        FinalSpeed = new Vector2(HorizonSpeed, VerticalSpeed);
        Rd.velocity = FinalSpeed;
    }
}
