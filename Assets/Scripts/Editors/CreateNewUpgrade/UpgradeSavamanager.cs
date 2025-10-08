using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using TMPro;

public class UpgradeSavamanager : MonoBehaviour
{
    public Transform contentParent;
    public GameObject galleryItemPrefab;
    public Button deleteSelectedButton;
    public Button saveAsNewButton;
    public Button saveButton;

    private string UpgradeDataPath;
    public List<Texture2D> IconTextures;
    public UpgradePreviewManager UpgradePreviewManager  ;
    public CreateNewUpgrades CreateNewUpgrades;

    private UpgradeDataList upgradeList;
    private int currentSelectedIndex = -1;
    private int copynumber = 1;

    void Start()
    {
        UpgradeDataPath = Path.Combine(Application.streamingAssetsPath, "upgrades.json");
        deleteSelectedButton.onClick.AddListener(DeleteSelected);
        saveAsNewButton.onClick.AddListener(SaveAsNew);
        saveButton.onClick.AddListener(SaveCurrent);

        upgradeList = LoadUpgradeList();
        LoadGallery();
    }

    void LoadGallery()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        for (int i = 0; i < upgradeList.upgrades.Count; i++)
        {
            AddGalleryItem(upgradeList.upgrades[i], i);
        }
    }


    UpgradeDataList LoadUpgradeList()
    {
        if (!File.Exists(UpgradeDataPath))
            return new UpgradeDataList { upgrades = new List<UpgradeData>() };

        string json = File.ReadAllText(UpgradeDataPath);
        var list = JsonUtility.FromJson<UpgradeDataList>(json);
        if (list == null || list.upgrades == null)
            list = new UpgradeDataList { upgrades = new List<UpgradeData>() };
        return list;
    }

    void AddGalleryItem(UpgradeData data, int index)
    {
        GameObject entry = Instantiate(galleryItemPrefab, contentParent);
        Button button = entry.GetComponent<Button>();
        RawImage icon = entry.GetComponentInChildren<RawImage>();
        TMP_Text label = entry.GetComponentInChildren<TMP_Text>();
        Image background = entry.GetComponent<Image>();

        label.text = data.upgradeName;

        int IconID = data.icon;
        if (IconID >= 0 && IconID < IconTextures.Count)
        {
            icon.texture = IconTextures[IconID];
        }

        button.onClick.AddListener(() => SelectItem(index, background));
    }

    void SelectItem(int index, Image background)
    {
        foreach (Transform child in contentParent)
        {
            var img = child.GetComponent<Image>();
            if (img) img.color = Color.white;
        }
        currentSelectedIndex = index;
        background.color = Color.green;

        if (index >= 0 && index < upgradeList.upgrades.Count)
            UpgradePreviewManager.ShowPreview(upgradeList.upgrades[index]);
        else
            UpgradePreviewManager.ShowPreview(null);
    }



    void DeleteSelected()
    {
        if (currentSelectedIndex < 0 || currentSelectedIndex >= upgradeList.upgrades.Count) return;
        upgradeList.upgrades.RemoveAt(currentSelectedIndex);
        SaveUpgradeList();
        LoadGallery();
        currentSelectedIndex = -1;
    }

    void SaveAsNew()
    {
        UpgradeData newUpgrade = GetUpgradeDataFromInput();
        newUpgrade.upgradeName = StopSameName(newUpgrade.upgradeName);
        upgradeList.upgrades.Add(newUpgrade);
        SaveUpgradeList();
        LoadGallery();
    }

    void SaveCurrent()
    {
        UpgradeData updatedUpgrade = GetUpgradeDataFromInput();

        if (currentSelectedIndex < 0 || currentSelectedIndex >= upgradeList.upgrades.Count)
        {
            SaveAsNew();
        }
        else
        {
            updatedUpgrade.upgradeName = StopSameName(updatedUpgrade.upgradeName);
            upgradeList.upgrades[currentSelectedIndex] = updatedUpgrade;
        }

        SaveUpgradeList();
        LoadGallery();
    }

    void SaveUpgradeList()
    {
        string json = JsonUtility.ToJson(upgradeList, true);
        File.WriteAllText(UpgradeDataPath, json);
    }
    
    UpgradeData GetUpgradeDataFromInput()
    {
        if (CreateNewUpgrades != null)
            return CreateNewUpgrades.GetUpgradeDataFromInputFields();
        else
            return null;
    }
    string StopSameName(string name)
    {
        foreach (var upgrade in upgradeList.upgrades)
        {
            if (upgrade.upgradeName == name)
            {
                name = name + "_" + copynumber;
                copynumber++;
                return name;
            }
        }
        return name;
    }

}
