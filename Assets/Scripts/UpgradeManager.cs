using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TMPro;


public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradeMenu;
    private GameObject player;
    public Upgrader Upgrader;

    public Dictionary<string, UpgradeData> upgradeTypes = new Dictionary<string, UpgradeData>();



    void Start()
    {
        CheckIfFolderExists();
        ReadInData();
    }


    public void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
        updateUpgradeMenu();
    }
    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ReadInData()
    {
        string filePath = Application.streamingAssetsPath + "/upgrades.json";
        if (System.IO.File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            List<UpgradeData> dataList = JsonUtility.FromJson<UpgradeDataList>(json).upgrades;
            foreach (var data in dataList)
            {
                upgradeTypes[data.upgradeName] = data;
            }
        }
    }


    private void updateUpgradeMenu()
    {
        List<UpgradeData> randomUpgrades = GetRandomUpgrades(4);

        for (int i = 0; i < randomUpgrades.Count; i++)
        {
            SetupUpgradeButton(randomUpgrades[i], i + 1);
        }
    }

    private List<UpgradeData> GetRandomUpgrades(int count)
    {
        List<UpgradeData> availableUpgrades = upgradeTypes.Values.ToList();
        List<UpgradeData> selectedUpgrades = new List<UpgradeData>();

        for (int i = 0; i < count && i < availableUpgrades.Count; i++)
        {
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            selectedUpgrades.Add(availableUpgrades[randomIndex]);
            availableUpgrades.RemoveAt(randomIndex);
        }

        return selectedUpgrades;
    }



    private void SetupUpgradeButton(UpgradeData data, int buttonIndex)
    {
        TextMeshProUGUI rarityText = null;
        RawImage rarityColour = null;
        RawImage icon = null;
        TextMeshProUGUI description = null;
        TextMeshProUGUI name = null;
        Button button = null;

        string buttonName = "UpgradeButton" + buttonIndex;
        Transform buttonTransform = upgradeMenu.transform.Find(buttonName);

        if (buttonTransform != null)
        {
            button = buttonTransform.GetComponent<Button>();

            if (button != null)
            {
                rarityText = button.transform.Find("RarityText").GetComponent<TextMeshProUGUI>();
                rarityColour = button.transform.Find("rarityColour").GetComponent<RawImage>();
                icon = button.transform.Find("icon").GetComponent<RawImage>();
                description = button.transform.Find("description").GetComponent<TextMeshProUGUI>();
                name = button.transform.Find("name").GetComponent<TextMeshProUGUI>();

                if (rarityText != null) rarityText.text = "Rarity: " + GetRarityText(data.purchasedLevel + 1);
                if (rarityColour != null) rarityColour.color = GetRarityColor(data.purchasedLevel + 1);
                if (icon != null) icon.texture = GetIconTexture(data.icon);
                if (description != null) description.text = data.description;
                if (name != null) name.text = data.upgradeName;


                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => ApplyUpgrade(data , data.purchasedLevel + 1) );
            }
        }
    }

    private void ApplyUpgrade(UpgradeData upgrade, int purchaseLevel)
    {
        upgrade.purchasedLevel++;

        Upgrader.ApplyUpgrade(upgrade, purchaseLevel);

        if(upgrade.purchasedLevel >= 5)
        {
            upgradeTypes.Remove(upgrade.upgradeName);
        }

        CloseUpgradeMenu();
    }

    private Color GetRarityColor(int purchasedLevel)
    {
        switch (purchasedLevel)
        {
            case 1:
                return Color.gray;
            case 2:
                return Color.green; 
            case 3:
                return Color.blue; 
            case 4:
                return new Color(0.58f, 0f, 0.83f);
            case 5:
                return Color.yellow;
            default:
                return Color.white; 
        }
    }
    private string GetRarityText(int purchasedLevel)
    {
        switch (purchasedLevel)
        {
            case 1:
                return "Common"; 
            case 2:
                return "Uncommon";
            case 3:
                return "Rare";
            case 4:
                return "Epic";
            case 5:
                return "Legendary";
            default:
                return "Unknown"; 
        }
    }
    private Texture GetIconTexture(string iconName)
    {
        string filePath = Application.persistentDataPath + "/Icons/" + iconName + ".png";
        if (System.IO.File.Exists(filePath))
        {
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            Texture2D tex = new Texture2D(2, 2);
            if (tex.LoadImage(fileData))
            {
                return tex;
            }
        }
        return null;
    }


    private void CheckIfFolderExists()
    {
        string folderPath = Application.persistentDataPath + "/Icons/";
        if (!Directory.Exists(folderPath))
        {
            Debug.Log("Icons folder not found, creating it at: " + folderPath);
            Directory.CreateDirectory(folderPath);
        }
        else
        {
            Debug.Log("Icons folder exists at: " + folderPath);
        }
    }
}
