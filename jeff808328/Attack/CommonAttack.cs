using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAttack : MonoBehaviour
{
    #region Position

    protected Vector3 BoxCenter;

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

    public GameObject HitEffect;

    protected CommonAnimator Animator;
    protected CommonMove CommonMove;
    public ChatacterData ChatacterData;

    protected void SetComponent()
    {
        Animator = this.GetComponent<CommonAnimator>();

        CommonMove = this.GetComponent<CommonMove>();
    }

    public void Attack()
    {
        StartCoroutine(HitCheck());
    }

    private IEnumerator HitCheck()
    {
        //  Debug.Log("Prepare Attack");

        yield return new WaitForSecondsRealtime(BeforeAttack);
        // �e�n

        //   Debug.Log("Attack Start");

        // �ˬd���S���i��������
        var AttackDectect = Physics2D.OverlapBoxAll(BoxCenter, BoxSize, 0, Attackable);

        //if (HitEffect != null)
        //    Instantiate(HitEffect,this.gameObject.transform);


        yield return new WaitForSecondsRealtime(Attacking);
        // �ʧ@��

        // Debug.Log("Attack End");

        // �y���ˮ`
        foreach (var Attacked in AttackDectect)
        {
            Attacked.GetComponent<CommonHP>().Hurt(ChatacterData.Atk);
       //     Debug.Log(Attacked.gameObject.name);
        }

        yield return new WaitForSecondsRealtime(AfterAttack);
        // ��n       

    //    Debug.Log("Prepare Next Attack");
    }

    protected void UpdataCollision()
    {
        BoxCenter = new Vector2(transform.position.x + BoxWideAdjust * CommonMove.LastMoveDirection,
                                   transform.position.y + BoxHighAdjust * transform.localScale.y);

        BoxSize = new Vector2(transform.lossyScale.x * BoxWide, transform.lossyScale.y * BoxHeight);
    }

}
