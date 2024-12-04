using System.Collections;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField]
    ObjectPool pool;

    [SerializeField]
    float coolTime;

    [SerializeField]
    float radius;

    [SerializeField]
    Collider2D groundColl;

    Bounds ground;

    float timer;

    private void Start()
    {
        ground = groundColl.bounds;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer>coolTime)
        {
            timer = 0;

            SpawnBox();
        }     
    }

    void SpawnBox()
    {
        float angle = Random.Range(0f, 360f);
        Vector3 spawnPosition = GetPos(angle, radius) + Player.Instance.transform.position;

        if (!ground.Contains(spawnPosition))
        {
            spawnPosition = ground.ClosestPoint(spawnPosition);
        }

        GameObject boxObj = pool.GetObject();
        Box box = boxObj.GetComponent<Box>();
        box.Set(pool);
        box.transform.position = spawnPosition;
    }

    Vector3 GetPos(float angle, float radius)
    {
        float rad = Mathf.Deg2Rad * angle;
        return new Vector3(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius);
    }
}
