using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class GunManager : MonoBehaviour
{
    public GameObject newWeaponsScreen;
    public TextMeshProUGUI gunNameText;
    public TextMeshProUGUI FireRateText;
    public TextMeshProUGUI ReloadTimeText;
    public TextMeshProUGUI MagazineSizeText;
    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI RangeText;
    public TextMeshProUGUI BulletSpeedText;
    public Button ContinueButton;
    public GunScript gunScript;
    public Dictionary<string, GunData> gunTypes = new Dictionary<string, GunData>();
    private int currentGunIndex = 0;

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


    public void GetNextGun()
    {
        List<GunData> gunList = new List<GunData>(gunTypes.Values);
        if (gunList.Count == 0) return;
        currentGunIndex = (currentGunIndex + 1) % gunList.Count;
        GunData previousGun = gunScript.GetCurrentGunData();
        GunData newGun = gunList[currentGunIndex];
        if (newGun.gunName == previousGun.gunName)
        {
            GetNextGun();
            return;
        }
        SetGunData(newGun.gunName, gunScript);
        NewWeaponsScreen(previousGun, newGun);
        if( currentGunIndex == gunList.Count - 1)
        {
            FindFirstObjectByType<LevelManagerr>().HasAllGuns = true;
        }
    }

    private void NewWeaponsScreen(GunData PreviusesGun, GunData NewGun)
    {
        Time.timeScale = 0f;
        newWeaponsScreen.SetActive(true);
        gunNameText.text = NewGun.gunName;
        FireRateText.text = "Fire Rate: " + PreviusesGun.fireRate.ToString() + " -> " + NewGun.fireRate.ToString();
        ReloadTimeText.text = "Reload Time: " + PreviusesGun.reloadTime.ToString() + " -> " + NewGun.reloadTime.ToString();
        MagazineSizeText.text = "Magazine Size: " + PreviusesGun.magazineSize.ToString() + " -> " + NewGun.magazineSize.ToString();
        DamageText.text = "Damage: " + PreviusesGun.bulletDamage.ToString() + " -> " + NewGun.bulletDamage.ToString();
        RangeText.text = "Range: " + PreviusesGun.range.ToString() + " -> " + NewGun.range.ToString();
        BulletSpeedText.text = "Bullet Speed: " + PreviusesGun.bulletSpeed.ToString() + " -> " + NewGun.bulletSpeed.ToString();
        ContinueButton.onClick.RemoveAllListeners();
        ContinueButton.onClick.AddListener(() => CloseNewWeaponsScreen());
    }
    public void CloseNewWeaponsScreen()
    {
        newWeaponsScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    
}
