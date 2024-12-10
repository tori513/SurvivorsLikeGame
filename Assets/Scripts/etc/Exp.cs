using System.Collections;
using UnityEngine;

public class Exp : MonoBehaviour
{
    ObjectPool pool;

    [SerializeField]
    public int score;

    [SerializeField]
    float coolTime;

    [SerializeField]
    float height;
    [SerializeField]
    float speed;

    float timer;

    bool triggerFlag = false;

    public void Set(ObjectPool pool)
    {
        this.pool = pool;
    }

    private void Start()
    {
        StartCoroutine(CurveRoutine());
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > coolTime && triggerFlag)
        {
            pool.ReturnObject(gameObject);
            timer = 0;
        }
    }

    public void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position, 2.5f * Time.deltaTime);
    }

    IEnumerator CurveRoutine()
    {
        float randomValue = Random.Range(-1.5f, 1.5f);

        float t = 0f;
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(randomValue, 0);
        Vector2 center = (start + end) * 0.5f;
        center.y += height;

        while(t<1f)
        {
            t += speed * Time.deltaTime;
            Vector2 ac = Vector2.Lerp(start, center, t);
            Vector2 cb = Vector2.Lerp(center, end, t);
            Vector2 result = Vector2.Lerp(ac,cb, t);

            transform.position = result;

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ExpManager.Instance.AddScore();       
            triggerFlag = true;
        }
    }

    private void OnEnable()
    {
        timer = 0f;
        triggerFlag = false;
    }
}
