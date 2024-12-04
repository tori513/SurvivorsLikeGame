using UnityEngine;

public class Slash : MonoBehaviour
{
    float damage;

    [SerializeField]
    LayerMask enemyLayerMask;

    CircleCollider2D coll;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        damage = 7f;

        coll = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, coll.radius, enemyLayerMask);
        
        if(colls.Length != 0)
        {
            foreach (Collider2D coll in colls)
            {
                coll.GetComponent<EnemyBase>().TakeDamage(damage);
            }
        }
    }

    public void DamageUp(float value)
    {
        damage *= value;
    }
}
