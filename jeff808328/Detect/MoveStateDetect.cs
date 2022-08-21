using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStateDetect : MonoBehaviour
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

    Collider2D GroundDetect;
    Collider2D LeftWallDetect;
    Collider2D RightWallDetect;

    #region RLbox data

    [SerializeField]
    [Range(0, 20f)]
    private float RLBoxWide;

    [SerializeField]
    [Range(0, 20f)]
    private float RLBoxHeight;

    [SerializeField]
    [Range(0, 20f)]
    private float RLWideAdjust;

    [SerializeField]
    [Range(-5f, 5f)]
    private float RLHeightAdjust;

    #endregion

    #region
    [SerializeField]
    [Range(0, 20f)]
    private float BotBoxWide;

    [SerializeField]
    [Range(0, 20f)]
    private float BotBoxHeight;

    [SerializeField]
    [Range(0, 20f)]
    private float BotWideAdjust;

    [SerializeField]
    [Range(0, 20f)]
    private float BotHeightAdjust;

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

        RLDetectSize = new Vector2(transform.lossyScale.x * RLBoxWide,
                                   transform.lossyScale.y * RLBoxHeight);

        BottomDetectPos = new Vector2(transform.position.x + BotWideAdjust,
                                      transform.position.y + BotHeightAdjust);

        RightDetectPos = new Vector2(transform.position.x + transform.lossyScale.x * 0.5f + RLWideAdjust,
                                     transform.position.y + RLHeightAdjust);

        LeftDetectPos = new Vector2(transform.position.x - transform.lossyScale.x * 0.5f - RLWideAdjust,
                                     transform.position.y + RLHeightAdjust);

        GroundDetect = Physics2D.OverlapBox(BottomDetectPos, BotDetectSize, 0, Ground);
        LeftWallDetect = Physics2D.OverlapBox(LeftDetectPos,RLDetectSize, 0, Wall);
        RightWallDetect = Physics2D.OverlapBox (RightDetectPos,RLDetectSize, 0, Wall);

        GroundTouching = GroundDetect;

        if (LeftWallDetect || RightWallDetect)
            WallTouching = true;
        else
            WallTouching = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BottomDetectPos, BotDetectSize);
        Gizmos.DrawWireCube(RightDetectPos, RLDetectSize);
        Gizmos.DrawWireCube(LeftDetectPos, RLDetectSize);
    }
}
