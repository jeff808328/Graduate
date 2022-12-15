using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMove : MonoBehaviour
{
    #region 功能啟用選項

    public bool UsingHorizonFlip;

    public bool UseCancel;

    //public bool Player;

    //public bool Ai;

    #endregion

    #region 水平速度控制
    private float HorizonSpeedMax = 0; //速度上限
    public float HorizonSpeed = 0; // 運算用 & 當前值

    public float AddSpeedAdjust; // 移動速度控制, 直接用 Time.DeltaTime 值太小
    private float OriginAddSpeedAdjust;

    [SerializeField] protected float MinusSpeedAdjust; // 移動速度控制, 直接用 Time.DeltaTime 值太小
    private float OriginMinusSpeedAdjust;

    private float AddSpeed; // 加速度初始值
    private float MinusSpeed; // 減速度初始值   
    #endregion

    #region 垂直速度控制
    protected float VerticalSpeedMax = 0; //速度上限
    public float VerticalSpeed = 0; // 運算用 & 當前值

    private float Gravity; // 重力初始值
    public float GravityAdjust; // 重力調整值 
    public float GravityMax; // 重力最大值

    protected int MaxJumpTimes; // 最大跳躍次數

    [SerializeField] protected bool GroundTouching; // 地板偵測

    protected int JumpTime;
    #endregion

    #region 組件

    protected CommonAnimator CommonAnimator;

    protected GroundAndWallDetect GroundAndWallDetect;

    public ChatacterData ChatacterData;

    protected Rigidbody2D Rd;

    // Common

    //protected EnemyBackGroundData EnemyBackGroundData;

    //protected EnemyMoveDetect EnemyMoveDetect;

    // Ai

    //protected PlayerState PlayerState;

    // Player


    #endregion

    #region 其他
    protected float RollCD;
    [HideInInspector] public int LastMoveDirection = 0; //角色最後朝向
    private Vector3 ScacleNow; // X值為角色當前朝向
    protected Vector2 FinalSpeed = new Vector2(0, 0); // 最終運動速度

    private float BeforeDashSpeed;
    private float BeforeDahsMoveDirection;
    public float DashAdjust;
    #endregion

    protected void SetData()
    {
        HorizonSpeedMax = ChatacterData.MaxMoveSpeed;
        VerticalSpeedMax = ChatacterData.JumpSpeed;

        AddSpeed = ChatacterData.AddSpeed;
        MinusSpeed = ChatacterData.MinusSpeed;

        Gravity = ChatacterData.Gravity;
        MaxJumpTimes = ChatacterData.AirJumpTimes;

        RollCD = ChatacterData.RollCD;

        OriginAddSpeedAdjust = AddSpeedAdjust;
        OriginMinusSpeedAdjust = MinusSpeedAdjust;
    }

    protected void SetComponent()
    {
        Rd = this.GetComponent<Rigidbody2D>();

        CommonAnimator = this.GetComponent<CommonAnimator>();

        GroundAndWallDetect = this.GetComponent<GroundAndWallDetect>();   
    }

    // start設定

    protected void HorizonVelocity(int Direction) // 水平速度控制(+1往右 -1往左)
    {
        if (Direction != LastMoveDirection && UsingHorizonFlip)
            HorizonFlip();

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*調整值
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // 避免超過速度上限

        LastMoveDirection = Direction; // 紀錄當前移動方向,轉向和減速用
    }
    protected void VerticalVelocity()
    {
        //Debug.Log("Vertical move work");

        VerticalSpeed = VerticalSpeedMax;
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
    protected void GravityEffect()
    {
        VerticalSpeed -= Gravity * Time.deltaTime * GravityAdjust;

        VerticalSpeed = Mathf.Clamp(VerticalSpeed, GravityMax, VerticalSpeedMax);
    }

    // 物理基本

    public void Brake() // 急煞 將速度與加速度歸0
    {
        Debug.Log("brake");
        HorizonSpeed = 0;
        AddSpeedAdjust = 0;
    }
    public void DashStart(int Direction)
    {
        Debug.Log("Dash");

        HorizonSpeedMax = 20;

        BeforeDashSpeed = HorizonSpeed;
        HorizonSpeed = 10 * DashAdjust * LastMoveDirection * Direction;

        if(!GroundAndWallDetect.GroundTouching)
            HorizonSpeed = 6 * DashAdjust * LastMoveDirection;


        // 短距離衝刺
        // 將水平速度變更為上限 
    }
    public void DashEnd()
    {
        if (LastMoveDirection == BeforeDahsMoveDirection)
            HorizonSpeed = BeforeDashSpeed;

        HorizonSpeedMax = ChatacterData.MaxMoveSpeed;
    }
    public void AddSpeedReset() // 急煞結束後用 將加速度回歸正常
    {
        AddSpeedAdjust = OriginAddSpeedAdjust;
    }
    public void MinusSpeedReset() // 短距離衝刺後用  
    {
        MinusSpeedAdjust = OriginMinusSpeedAdjust;
    }
    public void HorizonFlip()
    {
        ScacleNow = this.gameObject.transform.lossyScale;

        ScacleNow.x *= -1;

        this.gameObject.transform.localScale = ScacleNow;
    }

    // 特殊動作

    public IEnumerator Dash(int Direction,float DashTime)
    {

        HorizonSpeedMax = ChatacterData.MaxMoveSpeed * 2; ;

        BeforeDashSpeed = HorizonSpeed;
        HorizonSpeed = HorizonSpeedMax * 0.5f * DashAdjust * LastMoveDirection * Direction;

        yield return new WaitForSecondsRealtime(DashTime);

        if (LastMoveDirection == BeforeDahsMoveDirection)
            HorizonSpeed = BeforeDashSpeed;

        HorizonSpeedMax = ChatacterData.MaxMoveSpeed;

    }
}
