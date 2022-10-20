using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject TargetObject;

    [Range(-5f, 5f)]
    public float OffsetX;

    [Range(-5f, 5f)]
    public float OffsetY;

    private int Direction = 1;

    void Update()
    {
        Direction = TargetObject.GetComponent<EnemyMoveDetect>().MoveDirection;

        Follow();
    }

    private void Follow()
    {
        transform.position = new Vector2(TargetObject.transform.position.x + OffsetX * Direction,
                                               TargetObject.transform.position.y + OffsetY);
    }    
}
