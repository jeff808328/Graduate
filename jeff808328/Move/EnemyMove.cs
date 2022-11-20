using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : CommonMove
{

 
    private void Update()
    {

        GravityEffect();

        GroundTouching = GroundAndWallDetect.GroundTouching;

        if (GroundTouching)
        {
            VerticalSpeed = Mathf.Clamp(VerticalSpeed, 0, VerticalSpeedMax);
        }

        FinalSpeed = new Vector2(HorizonSpeed, VerticalSpeed);
        Rd.velocity = FinalSpeed;
    }

}
