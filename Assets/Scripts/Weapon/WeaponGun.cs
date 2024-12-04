using UnityEngine;

public class WeaponGun : WeaponBase
{
    [SerializeField]
    ObjectPool pool;

    protected override void Attack()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius,enemyLayerMask);
        if (coll.Length == 0) return;

        Collider2D col = null;
        float minDistance = float.MaxValue;

        for (int i = 0;i < coll.Length; i++)
        {           
            float distance = Vector2.Distance(coll[i].transform.position, transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                col = coll[i];
            }
        }
        EnemyBase enemy = col.GetComponent<EnemyBase>();
        Shoot(enemy);
    }

    void Shoot(EnemyBase target)
    {
        if(target == null) return;

        GameObject bulletObj = pool.GetObject();
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.transform.position = transform.position;
        bullet.Set(target, pool);

        PlaySound();
    }
}
