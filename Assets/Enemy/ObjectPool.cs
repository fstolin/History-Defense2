using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;
    [SerializeField] [Range(1,50)] int poolSize = 5;

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];
        // Instantiate + disable objects
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemy, this.transform);
            pool[i].SetActive(false);
        }
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    // Enables the obejct / enemy from the pool
    private void SpawnEnemy()
    {
        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeSelf)
            {
                enemy.SetActive(true);
                break;
            }
        }
    }
}
