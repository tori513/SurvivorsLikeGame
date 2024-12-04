using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    float hp;
    [SerializeField]
    float speed;
    [SerializeField]
    float damage;

    [SerializeField]
    AudioClip clip;

    ObjectPool pool;
    ObjectPool expPool;
    ObjectPool textPool;

    SpriteRenderer spriteRenderer;
    Animator animator;
    Collider2D coll;
    Rigidbody2D rigid;

    float initHp;

    Vector2 vec;

    bool isColl;
    bool isDead;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();

        initHp = hp;
    }

    private void OnEnable()
    {
        isDead = false;
        hp = initHp;
        coll.enabled = true;
        ChangeColor(Color.white);
    }

    void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        vec = (Player.Instance.transform.position - transform.position).normalized;
        rigid.velocity = vec * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isColl && collision.gameObject.CompareTag("Player"))
        {
            if (isDead) return;

            isColl = true;

            StartCoroutine(DamagePlayerCoroutine());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColl = false;
        }
    }

    public void Set(ObjectPool pool, ObjectPool expPool, ObjectPool textPool)
    {
        this.pool = pool;
        this.expPool = expPool;
        this.textPool = textPool;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        MakeDamageText(damage);

        hp -= damage;

        StartCoroutine(ChangeColorCoroutine());

        if (hp <= 0)
        {
            Die();
        }
    }

    void Flip()
    {
        if (isDead) return;

        spriteRenderer.flipX = Player.Instance.transform.position.x < transform.position.x;
    }

    void MakeDamageText(float damage)
    {
        DamageText damageText = textPool.GetObject().GetComponent<DamageText>();
        damageText.MakeText(damage, transform.position);
        damageText.Set(textPool);
    }

    void Die()
    {
        GameManager.Instance.CountEnemy();
        SoundManager.Instance.PlaySound(clip, 0.6f);

        isDead = true;
        rigid.velocity = Vector2.zero;

        StartCoroutine(PlayDeathAniCoroutine());

        DropExp();

        coll.enabled = false;       
    }

    void DropExp()
    {
        Exp exp = expPool.GetObject().GetComponent<Exp>();
        exp.transform.position = transform.position;
        exp.Set(expPool);
    }

    void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
    }

    IEnumerator PlayDeathAniCoroutine()
    {
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(1f);
        pool.ReturnObject(gameObject);
    }

    IEnumerator ChangeColorCoroutine()
    {
        ChangeColor(Color.red);
        yield return new WaitForSeconds(0.06f);
        ChangeColor(Color.white);
    }

    IEnumerator DamagePlayerCoroutine()
    {
        while (isColl)
        {
            Player.Instance.TakeDamage(damage);
            yield return new WaitForSeconds(1f);
        }
    }
}
