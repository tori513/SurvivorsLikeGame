using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField]
    protected CircleCollider2D circleCollider;

    protected float timer;
    [SerializeField]
    protected float coolTime;

    [SerializeField]
    protected LayerMask enemyLayerMask;

    [SerializeField]
    protected LayerMask boxLayerMask;

    protected SpriteRenderer spriteRenderer;

    protected AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        timer = coolTime;

        Attack();
    }

    public void WeaponUpdate()
    {
        bool enemyInRange = CheckEnemyInRange();

        if(!enemyInRange) return;

        coolTime -= Time.deltaTime;

        if (0 > coolTime)
        {
            Attack();
            coolTime = timer;
        }
    }

    protected bool CheckEnemyInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius, enemyLayerMask);
        return colliders.Length > 0;
    }

    protected abstract void Attack();

    protected void Flip()
    {
        spriteRenderer.flipX = true;
    }

    public float GetTimer()
    {
        return timer;
    }

    public float GetCoolTime()
    {
        return coolTime;
    }

    protected void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
