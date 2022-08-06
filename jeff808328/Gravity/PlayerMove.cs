using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CommonMove
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            HorizonVelocity(-1);
        else if (Input.GetKey(KeyCode.D))
            HorizonVelocity(1);
        else
            MiunsSpeed();

        if (Input.GetKeyDown(KeyCode.Space))
            VerticalVelocity();

        GravityEffect();


        FinalSpeed = new Vector2(HorizonSpeed, VerticalSpeed);
        Rd.velocity = FinalSpeed;

    }
}
