using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairStateManager : MonoBehaviour
{
    #region State

    private LongHairBaseState CurrentState;

    public LongHairIdle Idle;
    public LongHairWalk Walk;
    public LongHairDash Dash;
    public LongHairAttack Attack;
    public LongHairSingleThron SingleThron;
    public LongHairMultipleThron MultipleThron;
    public LongHairAirUmi AirUmi;
    public LongHairGroundUmi GroundUmi;
    public LongHairHurt Hurt;

    #endregion

    #region Component

    [HideInInspector] public EnemyBackGroundData EnemyBackGroundData;
    [HideInInspector] public EnemyMove EnemyMove;
    [HideInInspector] public GroundAndWallDetect GroundAndWallDetect;
    [HideInInspector] public EnemyAttack EnemyAttack;
    [HideInInspector] public CommonAnimator CommonAnimator;

    #endregion

    #region Value
    public int MoveDirection; // 移動朝向

    [HideInInspector] public float LastFlipTime;
    public float FlipCD; // 翻轉間隔 

    [HideInInspector] public float LastAttackTime;
    public float AttackCD; // 攻擊間隔 
    #endregion

    private void Start()
    {
        ComponentSetting();

        InitSetting();

        CurrentState = Idle;
        CurrentState.EnterState(this);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void StateSwitch(LongHairBaseState NextState)
    {
        CurrentState = NextState;
        NextState.EnterState(this);
    }

    private void InitSetting()
    {
        LastFlipTime = Time.time;
        LastAttackTime = Time.time;
    }

    private void ComponentSetting()
    {
        EnemyBackGroundData = this.GetComponent<EnemyBackGroundData>();
        EnemyMove = this.GetComponent<EnemyMove>();
        GroundAndWallDetect = this.GetComponent<GroundAndWallDetect>();
        EnemyAttack = this.GetComponent<EnemyAttack>();
        CommonAnimator = this.GetComponent<CommonAnimator>();
    }

}
