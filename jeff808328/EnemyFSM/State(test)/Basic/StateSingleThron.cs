using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSingleThron : EnemyBaseState
{
    private float FirstScanDistance;
    private float SecondScanDistance;

    private float AttackDelay;
    private float LastScanTime;

    private Transform PlayerPos;
    private Vector3 GeneratePoint;
    private float AdjustXray;
    private float Direction;

    private GameObject Thron;


    public override void EnterState(EnemyStateManager StateManager)
    {

        AttackDelay = StateManager.AttackCD;//掃描間隔設定
        LastScanTime = Time.time;

        AdjustXray = 5;

        FirstScanDistance = StateManager.EnemyBackGroundData.DistanceXray;// 掃描第一次
    }

    public override void UpdateState(EnemyStateManager StateManager)
    {


        if (Time.time > LastScanTime + AttackDelay)
        {
            SecondScanDistance = StateManager.EnemyBackGroundData.DistanceXray;// 掃描第二次

            PlayerPos = StateManager.EnemyBackGroundData.PlayerPos;

            Direction = StateManager.MoveDirection;

            Thron = StateManager.Thron;

            ThronGeneratePoint();// 判定

            StateManager.StateSwitch(StateManager.StateFightIdle);// 切換狀態
        }


    }

    private void ThronGeneratePoint()
    {


        if (FirstScanDistance < SecondScanDistance)
        {
            AdjustXray *= Direction * -1;
        }
        else if (FirstScanDistance > SecondScanDistance)
        {
            AdjustXray = Direction;
        }
        else
        {
            AdjustXray = 0;
        }

        if (PlayerPos != null)
            GeneratePoint = new Vector3(PlayerPos.transform.position.x + AdjustXray, PlayerPos.transform.position.y - 0.5f, PlayerPos.transform.position.z);

        MonoBehaviour.Instantiate(Thron, GeneratePoint, Quaternion.identity);

    }
}
