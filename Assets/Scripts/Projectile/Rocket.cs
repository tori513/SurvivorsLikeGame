using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    float damage;

    [SerializeField]
    LayerMask enemyMask;

    ObjectPool pool;

    [SerializeField]
    float speed;

    [SerializeField]
    ObjectPool effectPool;

    Collider2D coll;

    bool effectActive;

    Vector2 vec;

    [SerializeField]
    AudioClip clip;

    private void Awake()
    {
        damage = 5f;
    }

    public void Set(ObjectPool pool, Collider2D coll)
    {
        this.pool = pool;
        this.coll = coll;

        effectActive = false;

        vec = (coll.transform.position - transform.position).normalized;

        transform.SetParent(null);
    }

    private void Update()
    {
        transform.Translate(vec * speed * Time.deltaTime, Space.World);
    }

    void MakeEffect()
    {
        if (effectActive) return;
        effectActive = true;

        GameObject effect = effectPool.GetObject();
        effect.transform.position = transform.position;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        StartCoroutine(EffectRoutine(effect));
    }

    IEnumerator EffectRoutine(GameObject effect)
    {
        yield return new WaitForSeconds(0.7f);
        effectPool.ReturnObject(effect);
        pool.ReturnObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!effectActive && collision.CompareTag("Enemy"))
        {
            DealDamage();
            MakeEffect();
            SoundManager.Instance.PlaySound(clip, 1f);
        }
    }

    void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 2.5f, enemyMask);
        if (enemies.Length == 0) return;

        foreach (Collider2D enemy in enemies)
        {
            EnemyBase enemy_ = enemy.GetComponent<EnemyBase>();
            enemy_.TakeDamage(damage);
        }
    }

    public void DamageUp(float value)
    {
        damage *= value;
    }
}
