using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackGroundData : MonoBehaviour
{
    #region PlayerDetect

    [HideInInspector] public bool FacePlayer;

    private Vector2 RayCastSourcePos;

    public Vector2 RayCastDirection;

    // ¦Ò¼{¥ÎLayerMask°»´ú

    #endregion

    #region CalDistance

    [SerializeField] private Transform PlayerPos;
    private float DistanceXray;
    [HideInInspector] public int MoveDirection;

    #endregion

    #region CheckAttack

    protected Vector2 BoxCenter;

    [SerializeField]
    private float BoxWideAdjust;

    [SerializeField]
    private float BoxHighAdjust;

    protected Vector2 BoxSize;

    [SerializeField]
    private float BoxWide;

    [SerializeField]
    private float BoxHeight;

    public bool AttackAble;

    public LayerMask PlayerLayer;

    #endregion


    void Start()
    {
        AttackAble = false;
    }

    void Update()
    {
        RayCastSourcePos = new Vector2(transform.position.x + (0.5f * RayCastDirection.x), transform.position.y + 1);

        SearchPlayer();

        CalDistance();

        CheckAttack();

        Debug.DrawRay(RayCastSourcePos, RayCastDirection, Color.red, 1000f);
    }

    private void SearchPlayer()
    {
        RaycastHit2D RH = Physics2D.Raycast(RayCastSourcePos, RayCastDirection, Mathf.Infinity);

        Debug.Log(RH.collider.name);

        if (RH.collider.tag != "Player")
        {
            FacePlayer = false;

            PlayerPos = null;
        }
        else if (RH.collider.tag == "Player")
        {
            FacePlayer = true;

            PlayerPos = RH.collider.gameObject.transform;
        }

    }

    private void CalDistance()
    {
        if (FacePlayer)
        {
            DistanceXray = Mathf.Abs(PlayerPos.transform.position.x - this.transform.position.x);

            MoveDirection = ((PlayerPos.transform.position.x - this.transform.position.x) < 0) ? -1 : 1;
        }
    }

    private void CheckAttack()
    {
        BoxCenter = new Vector2(transform.position.x + BoxWideAdjust * MoveDirection,
                           transform.position.y + BoxHighAdjust * transform.localScale.y);

        BoxSize = new Vector2(transform.lossyScale.x * BoxWide, transform.lossyScale.y * BoxHeight);


        AttackAble = Physics2D.OverlapBox(BoxCenter, BoxSize, 0, PlayerLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(BoxCenter, BoxSize);
    }


}
