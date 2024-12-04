using UnityEngine;

public class WeaponShield : WeaponBase
{
    float damage;

    [SerializeField]
    AudioClip clip;

    private void Awake()
    {
        damage = 2f;    
    }

    protected override void Attack()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius, enemyLayerMask);
        
        foreach (Collider2D coll in colls)
        {
            coll.GetComponent<EnemyBase>().TakeDamage(damage);
        }
        SoundManager.Instance.PlaySound(clip, 1f);
    }

    public void DamageUp(float value)
    {
        damage *= value;
    }
}
