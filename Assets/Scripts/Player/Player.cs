using System.Collections;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField]
    float initHp;
    float currentHp;

    float timer = 0f;
    float coolTime = 1f;

    [SerializeField]
    HpBar hpBar;

    PlayerController playerController;
    PlayerAnimation playerAnimation;
    PlayerMove playerMove;

    [SerializeField]
    AudioClip damageClip;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMove = GetComponent<PlayerMove>();

        currentHp = initHp;
    }

    void Update()
    {
        timer += Time.deltaTime;
        hpBar.SetHpBar(currentHp, initHp);
    }

    public void TakeDamage(float damage)
    {
        if (GameManager.Instance.isStop) return;

        if (timer > coolTime)
        {
             timer = 0f;
             currentHp -= damage;
            SoundManager.Instance.PlaySound(damageClip, 0.8f);
            playerAnimation.PlayDamageEffect();
        }

        if(currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {       
        GameManager.Instance.GameOver();        
    }

    public void Heal(int hp)
    {
        currentHp += hp;
        playerAnimation.PlayHealEffect();
    }

    public void SpeedUp()
    {
        playerMove.SpeedUp();
        playerAnimation.PlaySpeedEffect();
    }

    public void SelectSkill()
    {
        playerAnimation.PlaySkillEffect();
    }

    public float GetHp()
    {
        return currentHp;
    }
}
