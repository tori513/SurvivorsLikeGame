using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<EnemyLevel> enemyLevels;

    [SerializeField]
    ObjectPool[] pool;
    [SerializeField]
    ObjectPool expPool;
    [SerializeField]
    ObjectPool textPool;

    [SerializeField]
    Collider2D ground;

    [SerializeField]
    float radius;

    float timer;
    float gameTime;
    float coolTime;

    int currentIndex;

    Bounds bounds;

    private void Start()
    {
        bounds = ground.bounds;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        gameTime += Time.deltaTime;

        UpdateEnemyLevel();

        if (timer >coolTime)
        {
            SpawnEnemy();
        }
    }

    private void UpdateEnemyLevel()
    {
        foreach (EnemyLevel level in enemyLevels)
        {
            if(gameTime >= level.gameTime)
            {
                currentIndex = level.index;
                coolTime = level.coolTime;
                break;
            }
        }
    }

    void SpawnEnemy()
    {
        if (currentIndex >= pool.Length) return;

        GameObject enemyObj = pool[currentIndex].GetObject();
        EnemyBase enemy = enemyObj.GetComponent<EnemyBase>();

        Vector3 spawnPos = CalculatePosition();
        enemyObj.transform.position = spawnPos;

        enemy.Set(pool[currentIndex], expPool, textPool);

        timer = 0f;
    }

    Vector3 CalculatePosition()
    {
        float angle = Random.Range(0f, 360f);
        Vector3 pos = GetPos(angle, radius) + Player.Instance.transform.position;

        if (!bounds.Contains(pos))
        {
            pos = bounds.ClosestPoint(pos);
        }

        return pos;
    }

    Vector3 GetPos(float angle, float radius)
    {
        float rad = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius);
    }
}

[System.Serializable]
public struct EnemyLevel
{
    public float gameTime; 
    public int index;           
    public float coolTime;      
}


