using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerController controller;

    SpriteRenderer spriteRenderer;
    Animator animator;

    [SerializeField] 
    private Animator healEffectAni;
    [SerializeField] 
    private Animator speedEffectAni;
    [SerializeField] 
    private Animator skillEffectAni;

    bool isDead = false;

    void Start()
    {
        controller = GetComponent<PlayerController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        SetDeathAni();
        if (GameManager.Instance.isStop) return;
        Flip();
        SetMoveAni();
    }

    void Flip()
    {
        if(controller.CheckFlip() != 0)
        {
            spriteRenderer.flipX = controller.CheckFlip() < 0;         
        }
    }

    void SetMoveAni()
    {
        animator.SetBool("isRun", controller.CheckMove());
    }

    void SetDeathAni()
    {
        if (!isDead && Player.Instance.GetHp() <= 0)
        {
            isDead = true;
            animator.SetTrigger("isDead");
        }
    }
    public void PlayHealEffect()
    {
        if (!healEffectAni.gameObject.activeSelf)
        {
            healEffectAni.gameObject.SetActive(true);
        }
        healEffectAni.Play("Heal", 0, 0f);
    }

    public void PlaySpeedEffect()
    {
        if (!speedEffectAni.gameObject.activeSelf)
        {
            speedEffectAni.gameObject.SetActive(true);
        }
        speedEffectAni.Play("Speed", 0, 0f);
    }

    public void PlaySkillEffect()
    {
        StartCoroutine(ShowSkillEffectRoutine());
    }

    public void PlayDamageEffect()
    {
        StartCoroutine(ChangeColorRoutine());
    }

    private IEnumerator ShowSkillEffectRoutine()
    {
        skillEffectAni.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        skillEffectAni.gameObject.SetActive(false);
    }

    public IEnumerator ChangeColorRoutine()
    {
        ChangeColor();
        yield return new WaitForSeconds(0.06f);
        ResetColor();
    }

    void ChangeColor()
    {
        spriteRenderer.color = Color.red;
    }

    void ResetColor()
    {
        spriteRenderer.color = Color.white;
    }
}
