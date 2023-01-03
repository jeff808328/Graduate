using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    #region State
    private EnemyBaseState CurrentState;

    public StateIdle StateIdle = new StateIdle();
    public StateFightIdle StateFightIdle = new StateFightIdle();
    public StatePatrol StatePatrol = new StatePatrol();
    public StateWalk StateWalk = new StateWalk();
    public StateAttack StateAttack = new StateAttack();
    public StateJump StateJump = new StateJump();
    public StateDash StateDash = new StateDash();

    public StateMutipleThron StateMutipleThron = new StateMutipleThron();
    public StateSingleThron StateSingleThron = new StateSingleThron();

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

    #region Control
    public bool UsePatrol;
    #endregion

    public GameObject Thron;


    public void ComponentSet()
    {
        EnemyBackGroundData = this.GetComponent<EnemyBackGroundData>();
        EnemyMove = this.GetComponent<EnemyMove>();
        GroundAndWallDetect = this.GetComponent<GroundAndWallDetect>();
        EnemyAttack = this.GetComponent<EnemyAttack>();
        CommonAnimator = this.GetComponent<CommonAnimator>();
    }

    public void InitSet()
    {
        CurrentState = StateIdle;
        CurrentState.EnterState(this);

        MoveDirection = 0;
    }

    private void Start()
    {
        ComponentSet();

        InitSet();
    }

    void FixedUpdate()
    {
        CurrentState.UpdateState(this);

        if (GroundAndWallDetect.WallTouching && LastFlipTime + FlipCD < Time.time)
        {
            StateManagerFlip();
        }
    }

    public void StateSwitch(EnemyBaseState NextState)
    {
        CurrentState = NextState;
        NextState.EnterState(this);
    }

    public void StateManagerFlip()
    {
        EnemyMove.HorizonFlip();
        LastFlipTime = Time.time;
    }

    public void StateManagerAttack()
    {
        EnemyAttack.Attack();
        LastAttackTime = Time.time;
    }
}
