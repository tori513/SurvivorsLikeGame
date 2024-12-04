using UnityEngine;

public class WeaponRocket : WeaponBase
{
    [SerializeField]
    ObjectPool pool;

    protected override void Attack()
    {
        Shoot();
    }

    void Shoot()
    {
        Collider2D coll = FindEnemy();
        if ((coll == null))
        {
            return;
        }

        GameObject rocketObj = pool.GetObject();
        Rocket rocket = rocketObj.GetComponent<Rocket>();
        rocket.transform.position = transform.position;
        rocket.Set(pool, coll);
    }

    Collider2D FindEnemy()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, circleCollider.radius, enemyLayerMask);
        return coll;
    }
}
