using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAndWallDetect : MonoBehaviour
{
    #region state

    public bool GroundTouching;
    public bool WallTouching;

    #endregion

    #region detect center point

    private Vector2 BottomDetectPos;
    private Vector2 LeftDetectPos;
    private Vector2 RightDetectPos;

    private Vector2 RLDetectSize;
    private Vector2 BotDetectSize;

    #endregion

    [HideInInspector] public Collider2D GroundDetect;
    Collider2D LeftWallDetect;
    Collider2D RightWallDetect;

    #region RLbox data

    [SerializeField]
    private float RLBoxWide;

    [SerializeField]
    private float RLBoxHeight;

    [SerializeField]
    private float RLWideAdjust;

    [SerializeField]
    private float RLHeightAdjust;

    #endregion

    # region BoxSizeSetting
    [SerializeField]
    private float BotBoxWide;

    [SerializeField]
    private float BotBoxHeight;

    [SerializeField]
    private float BotWideAdjust;

    [SerializeField]
    private float BotHeightAdjust;

    #endregion

    #region Enable

    public bool UseLeft;
    public bool UseRight;

    #endregion

    public LayerMask Ground;
    public LayerMask Wall;

    void Start()
    {
        RLDetectSize = new Vector2(transform.lossyScale.x * RLBoxWide,
                                   transform.lossyScale.y * RLBoxHeight);

        BotDetectSize = new Vector2(transform.lossyScale.x * BotBoxWide,
                                    transform.lossyScale.y * BotBoxHeight);
    }

    // Update is called once per frame
    void Update()
    {
        //BotDetectSize = new Vector2(transform.lossyScale.x * BotBoxWide,
        //                            transform.lossyScale.y * BotBoxHeight);

        //RLDetectSize = new Vector2(transform.lossyScale.x * RLBoxWide,
        //                           transform.lossyScale.y * RLBoxHeight);

        //BottomDetectPos = new Vector2(transform.position.x + BotWideAdjust,
        //                              transform.position.y + BotHeightAdjust);

        //RightDetectPos = new Vector2(transform.position.x + transform.lossyScale.x * 0.5f + RLWideAdjust,
        //                             transform.position.y + RLHeightAdjust);

        //LeftDetectPos = new Vector2(transform.position.x - transform.lossyScale.x * 0.5f - RLWideAdjust,
        //                             transform.position.y + RLHeightAdjust);

        SetButtomBox();
        SetSideBox();

        //GroundDetect = Physics2D.OverlapBox(BottomDetectPos, BotDetectSize, 0, Ground);

        //LeftWallDetect = Physics2D.OverlapBox(LeftDetectPos, RLDetectSize, 0, Wall);
        //RightWallDetect = Physics2D.OverlapBox(RightDetectPos, RLDetectSize, 0, Wall);

        GroundTouching = GroundDetect;

        if (LeftWallDetect || RightWallDetect)
            WallTouching = true;
        else
            WallTouching = false;

    }

    private void SetButtomBox()
    {
        BotDetectSize = new Vector2(transform.lossyScale.x * BotBoxWide,
                              transform.lossyScale.y * BotBoxHeight);

        BottomDetectPos = new Vector2(transform.position.x + BotWideAdjust,
                                     transform.position.y + BotHeightAdjust);

        GroundDetect = Physics2D.OverlapBox(BottomDetectPos, BotDetectSize, 0, Ground);

    }

    private void SetSideBox()
    {
        RLDetectSize = new Vector2(transform.lossyScale.x * RLBoxWide,
                           transform.lossyScale.y * RLBoxHeight);

        if (UseRight)
        {
            RightDetectPos = new Vector2(transform.position.x + transform.lossyScale.x * 0.5f + RLWideAdjust,
                                     transform.position.y + RLHeightAdjust);

            RightWallDetect = Physics2D.OverlapBox(RightDetectPos, RLDetectSize, 0, Wall);
        }

        if (UseLeft)
        {
            LeftDetectPos = new Vector2(transform.position.x - transform.lossyScale.x * 0.5f - RLWideAdjust,
                                               transform.position.y + RLHeightAdjust);

            LeftWallDetect = Physics2D.OverlapBox(LeftDetectPos, RLDetectSize, 0, Wall);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BottomDetectPos, BotDetectSize);
        Gizmos.DrawWireCube(RightDetectPos, RLDetectSize);
        Gizmos.DrawWireCube(LeftDetectPos, RLDetectSize);
    }
}
