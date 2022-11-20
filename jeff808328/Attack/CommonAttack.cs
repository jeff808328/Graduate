using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAttack : MonoBehaviour
{
    #region Position

    protected Vector2 BoxCenter;

    [SerializeField]
    private float BoxWideAdjust;

    [SerializeField]
    private float BoxHighAdjust;

    #endregion

    #region Size

    protected Vector2 BoxSize;

    [SerializeField]
    private float BoxWide;

    [SerializeField]
    private float BoxHeight;

    #endregion 

    public LayerMask Attackable;

    public float BeforeAttack;
    public float Attacking;
    public float AfterAttack;

    protected CommonAnimator Animator;
    protected CommonMove CommonMove;
    public ChatacterData ChatacterData;

    protected PlayerState PlayerState;

    private void Start()
    {
        Animator = this.GetComponent<CommonAnimator>();

        PlayerState = this.GetComponent<PlayerState>();

        CommonMove = this.GetComponent<CommonMove>();

        UpdataCollision();

        gameObject.GetComponent<PlayerState>().ResetAttack(0);
    }

    public void Attack()
    {
        StartCoroutine(HitCheck());     
    }

    private IEnumerator HitCheck()
    {
      //  Debug.Log("Prepare Attack");

        yield return new WaitForSecondsRealtime(BeforeAttack);     
        // 前搖

     //   Debug.Log("Attack Start");

        // 檢查有沒有可攻擊物件
        var AttackDectect = Physics2D.OverlapBoxAll(BoxCenter, BoxSize, 0, Attackable);
         
     
        yield return new WaitForSecondsRealtime(Attacking);
        // 動作中

       // Debug.Log("Attack End");

        // 造成傷害
        foreach(var Attacked in AttackDectect)
        {
            Attacked.GetComponent<CommonHP>().Hurt(ChatacterData.Atk);
            Debug.Log(Attacked.gameObject.name);
        }

        yield return new WaitForSecondsRealtime(AfterAttack);
        // 後搖       

        Debug.Log("Prepare Next Attack");
    }

    protected void UpdataCollision()
    {
        BoxCenter = new Vector2(transform.position.x + BoxWideAdjust * CommonMove.LastMoveDirection, 
                                   transform.position.y + BoxHighAdjust * transform.localScale.y);

        BoxSize = new Vector2(transform.lossyScale.x * BoxWide, transform.lossyScale.y * BoxHeight);
    }

}
