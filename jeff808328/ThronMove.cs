using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronMove : CommonMove
{
    void Start()
    {
        SetData();

        SetComponent();
    }

    void Update()
    {
        FinalSpeed = new Vector2(HorizonSpeed, VerticalSpeed);
        Rd.velocity = FinalSpeed;
    }

    public void LaunchThron()
    {
        VerticalVelocity();
    }

    public void StopThron()
    {
        VerticalSpeed = 0;
    }
}
