using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GunManager : MonoBehaviour
{
    public Dictionary<string, GunData> gunTypes = new Dictionary<string, GunData>();

    void Awake()
    {
        LoadGunData();
    }

    void LoadGunData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "gunData.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            List<GunData> dataList = JsonUtility.FromJson<GunDataList>(json).guns;
            foreach (var data in dataList)
            {
                gunTypes[data.gunName] = data;
            }
        }
    }

    public bool SetGunData(string gunName, GunScript gunScript)
    {
        if (gunTypes.TryGetValue(gunName, out var data))
        {
            gunScript.SetStatsFromData(data);
            return true;
        }
        return false;
    }

    public GunData GetGunData(string gunName)
    {
        if (gunTypes.TryGetValue(gunName, out var data))
            return data;
        return null;
    }
}
