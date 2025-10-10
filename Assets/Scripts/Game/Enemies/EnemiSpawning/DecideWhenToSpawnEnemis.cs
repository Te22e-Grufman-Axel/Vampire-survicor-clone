using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DecideWhenToSpawnEnemis : MonoBehaviour
{
    public List<string> StagingPool = new List<string>();
    public List<string> ActivePool = new List<string>();

    public EnemySpawner enemySpawner;
    public EnemyManager enemyManager;

    public float spawnInterval = 1.5f;
    private float spawnTimer = 0f;
    private float timeSinceStart = 0f;

    private float addEnemyInterval = 10f;
    private float addEnemyTimer = 0f;

    void Start()
    {

        foreach (KeyValuePair<string, EnemyData> kvp in enemyManager.enemyTypes)
        {
            EnemyData data = kvp.Value;
            int weight = Mathf.Clamp(11 - data.rarity, 1, 10);
            for (int i = 0; i < weight; i++)
            {
                StagingPool.Add(kvp.Key);
            }
        }
        
        // Sort by rarity (grouping same rarities together), then randomize within each rarity group
        StagingPool = StagingPool
            .OrderBy(enemyKey => enemyManager.enemyTypes[enemyKey].rarity)
            .ThenBy(x => Random.value)
            .ToList();
        
        string enemyID = StagingPool[0];
        StagingPool.RemoveAt(0);
        ActivePool.Add(enemyID);
    }    void Update()
    {
        float deltaTime = Time.deltaTime;
        timeSinceStart += deltaTime;
        spawnTimer += deltaTime;
        addEnemyTimer += deltaTime;


        spawnInterval = Mathf.Lerp(1.5f, 0.3f, Mathf.Log10(1f + timeSinceStart * 0.05f));


        if (spawnTimer >= spawnInterval && ActivePool.Count > 0)
        {
            SpawnEnemy();
        }


        if (addEnemyTimer >= addEnemyInterval && StagingPool.Count > 0)
        {
            addEnemyTimer = 0f;
            string enemyID = StagingPool[0];
            StagingPool.RemoveAt(0);
            ActivePool.Add(enemyID);
        }

    }

    private string GetRandomEnemyType()
    {
        int index = Random.Range(0, ActivePool.Count);
        return ActivePool[index];
    }

    public void SpawnEnemy()
    {
        spawnTimer = 0f;
        string enemyID = GetRandomEnemyType();
        enemySpawner.SpawnEnemy(enemyID);
    }

    public void SpawnSpecificEnemy(string enemyType)
    {
        enemySpawner.SpawnEnemy(enemyType);
    }
}
