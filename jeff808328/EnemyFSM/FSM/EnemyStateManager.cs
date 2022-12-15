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

    #endregion

    #region Component
    [HideInInspector] public EnemyBackGroundData EnemyBackGroundData;
    [HideInInspector] public EnemyMove EnemyMove;
    [HideInInspector] public GroundAndWallDetect GroundAndWallDetect;
    [HideInInspector] public EnemyAttack EnemyAttack;
    #endregion

    #region Value
    public int MoveDirection; // 移動朝向

    [HideInInspector] public float LastFlipTime;
    public float FlipCD; // 翻轉間隔 

    [HideInInspector] public float LastAttackTime;
    public float AttackCD; // 翻轉間隔 
    #endregion

    #region Control
    public bool UsePatrol;
    #endregion

    void Start()
    {
        EnemyBackGroundData = this.GetComponent<EnemyBackGroundData>();
        EnemyMove = this.GetComponent<EnemyMove>();
        GroundAndWallDetect = this.GetComponent<GroundAndWallDetect>();
        EnemyAttack = this.GetComponent<EnemyAttack>();

        CurrentState = StateIdle;
        CurrentState.EnterState(this);

        MoveDirection = 0;
    }

    void FixedUpdate()
    {
        CurrentState.UpdateState(this);

        if(GroundAndWallDetect.WallTouching && LastFlipTime + FlipCD < Time.time)
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
