using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Dictionary<string, EnemyData> enemyTypes = new Dictionary<string, EnemyData>();

    void Awake()
    {
        LoadEnemyData();
    }

    void LoadEnemyData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "enemyData.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            List<EnemyData> dataList = JsonUtility.FromJson<EnemyDataList>(json).enemies;
            foreach (var data in dataList)
            {
                enemyTypes[data.name] = data;
            }
        }
    }

    public EnemyData GetEnemyData(string name)
    {
        if (enemyTypes.TryGetValue(name, out var data))
            return data;
        return null;
    }
}

[System.Serializable]
public class EnemyDataList
{
    public List<EnemyData> enemies;
}

