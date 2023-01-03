using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackGroundData : MonoBehaviour
{
    #region PlayerDetect

    public bool FacePlayer;

    private Vector2 RayCastSourcePos;

    private Vector2 RayCastDirection;

    public bool FlipRayDirection;

    // ¦Ò¼{¥ÎLayerMask°»´ú

    #endregion

    #region CalDistance

    public Transform PlayerPos;
    [HideInInspector] public float DistanceXray;
    public int PlayerDirection;

    // ¶ZÂ÷­È

    // far¡Amid¡Anear¡Aramdon

    public float MidMini;
    private float MidMiniOri;

    public float MidMax;
    private float MidMaxOri;

    public float RamdonValue;
    public float Buffer;

    public int ActionIndex; //0,dash 1,jump 2,walk

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

    private EnemyStateManager StateManager;


    void Start()
    {
        AttackAble = false;

        MidMaxOri = MidMax;
        MidMiniOri = MidMini;

        RandonDistance();

        StateManager = this.GetComponent<EnemyStateManager>();

    }

    void Update()
    {
        RayCastSourcePos = new Vector2(transform.position.x + (1f * RayCastDirection.x), transform.position.y + 0.1f );

        BoxCenter = new Vector2(transform.position.x + BoxWideAdjust * PlayerDirection,
                        transform.position.y + BoxHighAdjust * transform.localScale.y);

        BoxSize = new Vector2(transform.lossyScale.x * BoxWide, transform.lossyScale.y * BoxHeight);


        SetRayDirection();

        SearchPlayer();

        CalDistance();

        CheckAttack();

        Debug.DrawRay(RayCastSourcePos, RayCastDirection, Color.yellow, 1000f);
    }

    private void SearchPlayer()
    {
        RaycastHit2D RH = Physics2D.Raycast(RayCastSourcePos, RayCastDirection, Mathf.Infinity);

      //  Debug.Log(RH.collider.name);

        if (RH.collider.tag != "Player")
        {
            FacePlayer = false;

            PlayerPos = null;

        //    Debug.Log("lose player");
        }
        else if (RH.collider.tag == "Player")
        {
            FacePlayer = true;

            PlayerPos = RH.collider.gameObject.transform;

         //   Debug.Log("find player" + RH.collider.name);
        }

    }

    private void CalDistance()
    {
        if (FacePlayer)
        {
            DistanceXray = Mathf.Abs(PlayerPos.transform.position.x - this.transform.position.x);

            if (DistanceXray > MidMax)
                ActionIndex = 0;
            else if (DistanceXray < MidMini)
                ActionIndex = 2;
            else
                ActionIndex = 1;

            PlayerDirection = ((PlayerPos.transform.position.x - this.transform.position.x) < 0) ? -1 : 1;
        }
    }

    private void CheckAttack()
    {


        AttackAble = Physics2D.OverlapBox(BoxCenter, BoxSize, 0, PlayerLayer);
    }

    private void SetRayDirection()
    {
        if (FlipRayDirection)
            RayCastDirection = new Vector2(-transform.lossyScale.x, 0);
        else
            RayCastDirection = new Vector2(transform.lossyScale.x, 0);
    }

    private void RandonDistance()
    {
        MidMax = MidMax + Random.Range(-RamdonValue - 1, RamdonValue + 1);
        MidMini = MidMini + Random.Range(-RamdonValue - 1, RamdonValue + 1);

        if(MidMax - MidMini < Buffer)
        {
            MidMax = MidMaxOri;
            MidMini = MidMiniOri;

            RandonDistance();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(BoxCenter, BoxSize);
    }

    // °O±o¸É¤WÀð¾À°»´ú
}
