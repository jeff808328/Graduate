using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveDetect : MonoBehaviour
{
    private GameObject NowDetect; // ��e��m�a������ // �V�U�o�gRayCast

    public GameObject TurnDetect; // �e�i��V�a������ // �V�U�o�gRayCast

    public int MoveDirection;
    public float RaycastLenghth;

    void Start()
    {
        MoveDirection = this.GetComponent<EnemyMove>().LastMoveDirection;
    }

    void Update()
    {
        ShootBottomRaycast(TurnDetect);
    }

    private void ShootBottomRaycast(GameObject RaySource)
    {
        RaycastHit2D RH =  Physics2D.Raycast(RaySource.transform.position,Vector2.down, RaycastLenghth);

        if(!RH.collider)
        {
            MoveDirection *= -1;
        }

    }

}
