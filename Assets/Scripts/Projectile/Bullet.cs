using UnityEngine;

public class Bullet : MonoBehaviour
{
    EnemyBase target;

    [SerializeField]
    float damage;

    [SerializeField]
    float speed;

    ObjectPool pool;

    public void Set(EnemyBase target, ObjectPool pool)
    {
        this.target = target;
        this.pool = pool;

        Vector2 direction = (target.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        if(target == null) return;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {           
            collision.GetComponent<EnemyBase>().TakeDamage(damage);
            pool.ReturnObject(gameObject);
        }
    }
}
