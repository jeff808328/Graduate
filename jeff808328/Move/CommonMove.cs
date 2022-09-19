using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMove : MonoBehaviour
{
    public ChatacterData ChatacterData;
    protected Rigidbody2D Rd;

    [SerializeField] protected Vector2 FinalSpeed = new Vector2(0, 0); // �̲׹B�ʳt��

    #region �����t�ױ���
    private float HorizonSpeedMax = 0; //�t�פW��
    public float HorizonSpeed = 0; // �B��� & ��e��

    public float AddSpeedAdjust; // ���ʳt�ױ���, ������ Time.DeltaTime �ȤӤp
    public float MinusSpeedAdjust ; // ���ʳt�ױ���, ������ Time.DeltaTime �ȤӤp

    private float AddSpeed = 0;
    private float MinusSpeed = 0;
    [HideInInspector]public int LastMoveDirection = 1;

    private Vector3 ScacleNow;
    #endregion

    #region �����t�ױ���
    protected float VerticalSpeedMax = 0; //�t�פW��
    public float VerticalSpeed = 0; // �B��� & ��e��

    private float Gravity;
    public float GravityAdjust;
    public float GravityMax;

    protected int MaxJumpTimes;
    #endregion

    protected bool GroundTouching;

    #region �ե�

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
        //  �P�}btn�Y����

        if (Direction != LastMoveDirection)
            HorizonFlip();

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*�վ��
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // �קK�W�L�t�פW��

        LastMoveDirection = Direction; // ������e���ʤ�V,��V�M��t��
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


        // ��t �̤j�γ̤p��0
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
