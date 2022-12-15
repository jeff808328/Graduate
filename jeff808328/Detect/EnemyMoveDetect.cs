using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveDetect : MonoBehaviour
{
    private GameObject NowDetect; // 當前位置地版偵測 // 向下發射RayCast

    public GameObject TurnDetect; // 前進方向地版偵測 // 向下發射RayCast

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

    } // 地板偵測

}
