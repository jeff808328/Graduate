using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMove : MonoBehaviour
{
    #region �\��ҥοﶵ

    public bool UsingHorizonFlip;

    public bool UseCancel;

    //public bool Player;

    //public bool Ai;

    #endregion

    #region �����t�ױ���
    private float HorizonSpeedMax = 0; //�t�פW��
    public float HorizonSpeed = 0; // �B��� & ��e��

    public float AddSpeedAdjust; // ���ʳt�ױ���, ������ Time.DeltaTime �ȤӤp
    private float OriginAddSpeedAdjust;

    [SerializeField] protected float MinusSpeedAdjust; // ���ʳt�ױ���, ������ Time.DeltaTime �ȤӤp
    private float OriginMinusSpeedAdjust;

    private float AddSpeed; // �[�t�ת�l��
    private float MinusSpeed; // ��t�ת�l��   
    #endregion

    #region �����t�ױ���
    protected float VerticalSpeedMax = 0; //�t�פW��
    public float VerticalSpeed = 0; // �B��� & ��e��

    private float Gravity; // ���O��l��
    public float GravityAdjust; // ���O�վ�� 
    public float GravityMax; // ���O�̤j��

    protected int MaxJumpTimes; // �̤j���D����

    [SerializeField] protected bool GroundTouching; // �a�O����

    protected int JumpTime;
    #endregion

    #region �ե�

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

    #region ��L
    protected float RollCD;
    [HideInInspector] public int LastMoveDirection = 0; //����̫�¦V
    private Vector3 ScacleNow; // X�Ȭ������e�¦V
    protected Vector2 FinalSpeed = new Vector2(0, 0); // �̲׹B�ʳt��

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

    // start�]�w

    protected void HorizonVelocity(int Direction) // �����t�ױ���(+1���k -1����)
    {
        if (Direction != LastMoveDirection && UsingHorizonFlip)
            HorizonFlip();

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*�վ��
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // �קK�W�L�t�פW��

        LastMoveDirection = Direction; // ������e���ʤ�V,��V�M��t��
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


        // ��t �̤j�γ̤p��0
    }
    protected void GravityEffect()
    {
        VerticalSpeed -= Gravity * Time.deltaTime * GravityAdjust;

        VerticalSpeed = Mathf.Clamp(VerticalSpeed, GravityMax, VerticalSpeedMax);
    }

    // ���z��

    public void Brake() // ��� �N�t�׻P�[�t���k0
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


        // �u�Z���Ĩ�
        // �N�����t���ܧ󬰤W�� 
    }
    public void DashEnd()
    {
        if (LastMoveDirection == BeforeDahsMoveDirection)
            HorizonSpeed = BeforeDashSpeed;

        HorizonSpeedMax = ChatacterData.MaxMoveSpeed;
    }
    public void AddSpeedReset() // ��ٵ������ �N�[�t�צ^�k���`
    {
        AddSpeedAdjust = OriginAddSpeedAdjust;
    }
    public void MinusSpeedReset() // �u�Z���Ĩ���  
    {
        MinusSpeedAdjust = OriginMinusSpeedAdjust;
    }
    public void HorizonFlip()
    {
        ScacleNow = this.gameObject.transform.lossyScale;

        ScacleNow.x *= -1;

        this.gameObject.transform.localScale = ScacleNow;
    }

    // �S��ʧ@

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
