using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAttack : MonoBehaviour
{
    #region Position

    protected Vector2 BoxCenter;

    [SerializeField]
    [Range(0, 2f)]
    private float BoxWideAdjust;

    [SerializeField]
    [Range(0, 2f)]
    private float BoxHighAdjust;

    #endregion

    #region Size

    protected Vector2 BoxSize;

    [SerializeField]
    [Range(0, 2f)]
    private float BoxWide;

    [SerializeField]
    [Range(0, 2f)]
    private float BoxHeight;

    #endregion 

    public LayerMask Attackable;

    public float BeforeAttack;
    public float Attacking;
    public float AfterAttack;

    public float AttackCD;
    protected float LastAttackTime;

    protected Animator Animator;
    public ChatacterData ChatacterData;

    private void Start()
    {
        Animator = this.GetComponent<Animator>();

        UpdataCollision();
    }

    protected void Attack()
    {
        StartCoroutine(HitCheck());
    }

    private IEnumerator HitCheck()
    {

        Debug.Log("Prepare Attack");

        yield return new WaitForSecondsRealtime(BeforeAttack);     
        // �e�n

        Debug.Log("Attack Start");

        // �ˬd���S���i��������
        var AttackDectect = Physics2D.OverlapBoxAll(BoxCenter, BoxSize, 0, Attackable);
       
     

        yield return new WaitForSecondsRealtime(Attacking);
        // �ʧ@��

        Debug.Log("Attack End");

        // �y���ˮ`
        foreach(var Attacked in AttackDectect)
        {
            Attacked.GetComponent<CommonHP>().Hurt(ChatacterData.Atk);
            Debug.Log(Attacked.gameObject.name);
        }

        yield return new WaitForSecondsRealtime(AfterAttack);
        // ��n       

        Debug.Log("Prepare Next Attack");
    }

    protected void UpdataCollision()
    {
        BoxCenter = new Vector2(transform.position.x + BoxWideAdjust, transform.position.y + BoxHighAdjust);

        BoxSize = new Vector2(transform.lossyScale.x * BoxWide, transform.lossyScale.y * BoxHeight);
    }

}
