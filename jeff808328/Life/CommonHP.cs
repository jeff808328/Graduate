using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonHP : MonoBehaviour
{
    public ChatacterData ChatacterData;

    private float HP;
    private float Def;
    private float HurtAdjust;

    protected Animator Animator;

    public float DisappearTime;

    public Slider Slider;
    public Gradient Gradient;
    public Image Fill;

    void Start()
    {
        HP = ChatacterData.HP;
        Def = ChatacterData.Def;
        HurtAdjust = 1;

        Fill.color = Gradient.Evaluate(1f);

        Animator = this.GetComponent<Animator>();
    }

    public void Hurt(float AttackerAtk)
    {
        HP -= (AttackerAtk - Def) * HurtAdjust;

        Slider.value = HP;
        Fill.color = Gradient.Evaluate(Slider.normalizedValue);

        if (HurtAdjust > 0)
            Animator.SetTrigger("Hurt");

        DieCheck();
    }

    public void HurtAdjustSet(int Value)
    {
        HurtAdjust = Value;
    }

    public void DieCheck()
    {
        if (HP <= 0)
        {
            Animator.SetBool("Die", true);
            this.gameObject.layer = 0;
            Invoke("Disappear", DisappearTime);
        }
    }

    private void Disappear()
    {
        this.gameObject.SetActive(false);
        Slider.gameObject.SetActive(false);
    }

    public void ResetEnemy(Vector3 GeneratePoint)
    {
        this.gameObject.layer = 7;
    }
}
