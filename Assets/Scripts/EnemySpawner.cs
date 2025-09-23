using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemySpawner : MonoBehaviour
{
    public EnemyManager enemyManager;
    public int spawnamound = 0;
    public void SpawnEnemy(string enemyType, Vector3 position)
    {
        EnemyData data = enemyManager.GetEnemyData(enemyType);
        if (data == null) return;

        GameObject enemy = Instantiate(enemyManager.enemyPrefab, position, Quaternion.identity);

        var enemyHealth = enemy.GetComponent<EnemyHealth>();
        var enemyMovement = enemy.GetComponent<EnemyMovement>();
        var enemyAttack = enemy.GetComponent<EnemyAttack>();

        enemyHealth.maxHealth = data.MaxHealth;
        enemyHealth.Damageresistance = data.Damageresistance;
        enemyMovement.speed = data.speed;
        enemyMovement.aggression = data.aggression;
        enemyMovement.attackRange = data.attackRange;
        enemyMovement.weaponType = data.weaponstype;
        enemyAttack.damage = data.damage;
        enemyAttack.attackSpeed = data.attackspeed;
        enemyAttack.attackRange = data.attackRange;
        enemyAttack.weaponType = data.weaponstype;

        enemy.GetComponent<Renderer>().material.color = new Color(data.color.x, data.color.y, data.color.z);
        enemy.transform.localScale = Vector3.one * data.size;
    }
 
    void SpawnRandomEnemy()
    {
        if (enemyManager.enemyTypes.Count == 0) return;

        List<string> keys = new List<string>(enemyManager.enemyTypes.Keys);
        string randomType = keys[Random.Range(0, keys.Count)];

        Camera cam = Camera.main;
        float margin = 4f;

        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        int side = Random.Range(0, 4);
        Vector3 spawnPos = Vector3.zero;

        switch (side)
        {
            case 0: // Left
                spawnPos.x = min.x - margin;
                spawnPos.y = Random.Range(min.y, max.y);
                break;
            case 1: // Right
                spawnPos.x = max.x + margin;
                spawnPos.y = Random.Range(min.y, max.y);
                break;
            case 2: // Top
                spawnPos.x = Random.Range(min.x, max.x);
                spawnPos.y = max.y + margin;
                break;
            case 3: // Bottom
                spawnPos.x = Random.Range(min.x, max.x);
                spawnPos.y = min.y - margin;
                break;
        }
        spawnPos.z = 0;

        SpawnEnemy(randomType, spawnPos);
    }
}