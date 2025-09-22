using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using TMPro;

public class Savemanager : MonoBehaviour
{
    public Transform contentParent;
    public GameObject galleryItemPrefab;
    public Button deleteSelectedButton;
    public Button saveAsNewButton;
    public Button saveButton;

    public string enemyDataPath;
    public List<Texture2D> shapeTextures; // Assign shape images in inspector

    private EnemyDataList enemyList;
    private int currentSelectedIndex = -1;

    void Start()
    {
        enemyDataPath = Path.Combine(Application.streamingAssetsPath, "enemyData.json");
        deleteSelectedButton.onClick.AddListener(DeleteSelected);
        saveAsNewButton.onClick.AddListener(SaveAsNew);
        saveButton.onClick.AddListener(SaveCurrent);

        LoadGallery();
    }

    void LoadGallery()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        enemyList = LoadEnemyList();

        for (int i = 0; i < enemyList.enemies.Count; i++)
        {
            AddGalleryItem(enemyList.enemies[i], i);
        }
    }

    EnemyDataList LoadEnemyList()
    {
        if (!File.Exists(enemyDataPath))
            return new EnemyDataList { enemies = new List<EnemyData>() };

        string json = File.ReadAllText(enemyDataPath);
        var list = JsonUtility.FromJson<EnemyDataList>(json);
        if (list == null || list.enemies == null)
            list = new EnemyDataList { enemies = new List<EnemyData>() };
        return list;
    }

    void AddGalleryItem(EnemyData data, int index)
    {
        GameObject entry = Instantiate(galleryItemPrefab, contentParent);
        Button button = entry.GetComponent<Button>();
        RawImage img = entry.GetComponentInChildren<RawImage>();
        TMP_Text label = entry.GetComponentInChildren<TMP_Text>();
        Image background = entry.GetComponent<Image>();

        label.text = data.name;

        if (!data.PngOrColour)
        {
            // PNG mode
            string appDataPath = Path.Combine(
                Application.persistentDataPath, "UserImages"
            );
            string pngPath = Path.Combine(appDataPath, data.pngName);
            StartCoroutine(LoadPngImage(pngPath, img));
        }
        else
        {
            // Shape + Color mode
            int shapeId = data.shape;
            if (shapeId >= 0 && shapeId < shapeTextures.Count)
            {
                img.texture = shapeTextures[shapeId];
                img.color = new Color(data.color.x, data.color.y, data.color.z, 1f);
            }
        }

        button.onClick.AddListener(() => SelectItem(index, background));
        background.color = Color.white;
    }

    IEnumerator LoadPngImage(string filePath, RawImage img)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("PNG file not found: " + filePath);
            yield break;
        }
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + filePath))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.Success)
                img.texture = DownloadHandlerTexture.GetContent(uwr);
            else
                Debug.LogError("Failed to load PNG: " + uwr.error);
        }
    }

    void SelectItem(int index, Image background)
    {
        // Deselect previous
        foreach (Transform child in contentParent)
        {
            var img = child.GetComponent<Image>();
            if (img) img.color = Color.white;
        }
        currentSelectedIndex = index;
        background.color = Color.green;
    }

    void DeleteSelected()
    {
        if (currentSelectedIndex < 0 || currentSelectedIndex >= enemyList.enemies.Count) return;
        enemyList.enemies.RemoveAt(currentSelectedIndex);
        SaveEnemyList();
        LoadGallery();
        currentSelectedIndex = -1;
    }

    void SaveAsNew()
    {
        // You need to get new enemy data from your input UI
        EnemyData newEnemy = GetEnemyDataFromInput();
        enemyList.enemies.Add(newEnemy);
        SaveEnemyList();
        LoadGallery();
    }

    void SaveCurrent()
    {
        if (currentSelectedIndex < 0 || currentSelectedIndex >= enemyList.enemies.Count) return;
        EnemyData updatedEnemy = GetEnemyDataFromInput();
        enemyList.enemies[currentSelectedIndex] = updatedEnemy;
        SaveEnemyList();
        LoadGallery();
    }

    void SaveEnemyList()
    {
        string json = JsonUtility.ToJson(enemyList, true);
        File.WriteAllText(enemyDataPath, json);
    }

    EnemyData GetEnemyDataFromInput()
    {
        return new EnemyData { name = "New Enemy", PngOrColour = false, pngName = "", shape = 0, color = Vector3.one };
    }
}

