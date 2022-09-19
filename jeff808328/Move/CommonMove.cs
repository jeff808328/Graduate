using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMove : MonoBehaviour
{
    public ChatacterData ChatacterData;
    protected Rigidbody2D Rd;

    [SerializeField] protected Vector2 FinalSpeed = new Vector2(0, 0); // 最終運動速度

    #region 水平速度控制
    private float HorizonSpeedMax = 0; //速度上限
    public float HorizonSpeed = 0; // 運算用 & 當前值

    public float AddSpeedAdjust; // 移動速度控制, 直接用 Time.DeltaTime 值太小
    public float MinusSpeedAdjust ; // 移動速度控制, 直接用 Time.DeltaTime 值太小

    private float AddSpeed = 0;
    private float MinusSpeed = 0;
    [HideInInspector]public int LastMoveDirection = 1;

    private Vector3 ScacleNow;
    #endregion

    #region 垂直速度控制
    protected float VerticalSpeedMax = 0; //速度上限
    public float VerticalSpeed = 0; // 運算用 & 當前值

    private float Gravity;
    public float GravityAdjust;
    public float GravityMax;

    protected int MaxJumpTimes;
    #endregion

    protected bool GroundTouching;

    #region 組件

    protected CommonAnimator CommonAnimator;

    protected EnemyMoveDetect EnemyMoveDetect;

    protected GroundAndWallDetect GroundAndWallDetect;

    #endregion


    void Start()
    {
        HorizonSpeedMax = ChatacterData.MaxMoveSpeed;
        VerticalSpeedMax = ChatacterData.JumpSpeed;

        AddSpeed = ChatacterData.AddSpeed;
        MinusSpeed = ChatacterData.MinusSpeed;

        Gravity = ChatacterData.Gravity;
        MaxJumpTimes = ChatacterData.AirJumpTimes;

        Rd = this.GetComponent<Rigidbody2D>();

        CommonAnimator = this.GetComponent<CommonAnimator>();

        EnemyMoveDetect = this.GetComponent<EnemyMoveDetect>();

        GroundAndWallDetect = this.GetComponent<GroundAndWallDetect>();
    }

    protected void HorizonVelocity(int Direction)
    {
        //  Debug.Log("Horizon move work");

        // HorizonSpeed = HorizonSpeedMax * Direction;
        //  鬆開btn即停止

        if (Direction != LastMoveDirection)
            HorizonFlip();

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*調整值
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // 避免超過速度上限

        LastMoveDirection = Direction; // 紀錄當前移動方向,轉向和減速用
    }

    protected void HorizonFlip()
    {
        ScacleNow = this.gameObject.transform.lossyScale;

        ScacleNow.x *= -1;

        this.gameObject.transform.localScale = ScacleNow;
    }

    protected void MiunsSpeed()
    {
      //  Debug.Log("minus speed is working");

        HorizonSpeed -= MinusSpeed * LastMoveDirection * Time.deltaTime * MinusSpeedAdjust;

        if (LastMoveDirection == 1)
        {
            HorizonSpeed = Mathf.Clamp(HorizonSpeed, 0, HorizonSpeedMax);
        }
        else if (LastMoveDirection == -1)
        {
            HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, 0);
        }


        // 減速 最大或最小為0
    }

    protected void VerticalVelocity()
    {
        //Debug.Log("Vertical move work");

        VerticalSpeed = VerticalSpeedMax;
    }

    protected void GravityEffect()
    {
        VerticalSpeed += Gravity * Time.deltaTime * GravityAdjust;

        VerticalSpeed = Mathf.Clamp(VerticalSpeed, GravityMax, VerticalSpeedMax);
    }

}
