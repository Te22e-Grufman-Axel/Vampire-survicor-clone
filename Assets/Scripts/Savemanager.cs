using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using TMPro;

public class Savemanager : MonoBehaviour
{
    private string userImagesPath;
    private GalleryItem currentSelectedItem;
    public Transform contentParent;
    public GameObject galleryItemPrefab;
    public Button deleteSelectedButton;

    public EnemyManager EnemyManager;

    void Start()
    {
        userImagesPath = Path.Combine(Application.persistentDataPath, "UserImages");
        if (!Directory.Exists(userImagesPath))
            Directory.CreateDirectory(userImagesPath);

        deleteSelectedButton.onClick.AddListener(DeleteSelected);
        LoadGallery();
    }

    void LoadGallery()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        string[] files = Directory.GetFiles(userImagesPath, "*.png");
        // foreach (var data in EnemyManager.dataList )
        // {
        //     StartCoroutine(LoadImageAsync(data.filePath));
        // }
    }

    IEnumerator LoadImageAsync(string filePath)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + filePath))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
                AddGalleryItem(filePath, DownloadHandlerTexture.GetContent(uwr));
        }
    }
    void AddGalleryItem(string filePath, Texture2D tex)
    {
        GameObject entry = Instantiate(galleryItemPrefab, contentParent);

        Button button = entry.GetComponent<Button>();
        RawImage img = entry.GetComponentInChildren<RawImage>();
        TMP_Text label = entry.GetComponentInChildren<TMP_Text>();
        Image background = entry.GetComponent<Image>();

        img.texture = tex;
        label.text = Path.GetFileName(filePath);

        GalleryItem item = new GalleryItem
        {
            filePath = filePath,
            button = button,
            background = background
        };

        button.onClick.AddListener(() => SelectItem(item));
    }

    void SelectItem(GalleryItem item)
    {
        if (currentSelectedItem != null && currentSelectedItem.background != null)
            currentSelectedItem.background.color = Color.white;

        currentSelectedItem = item;
        if (item.background != null)
            item.background.color = Color.green;
    }

    void DeleteSelected()
    {
        if (currentSelectedItem == null) return;

        if (File.Exists(currentSelectedItem.filePath))
            File.Delete(currentSelectedItem.filePath);

        Destroy(currentSelectedItem.button.gameObject);
        currentSelectedItem = null;
    }

    private class GalleryItem
    {
        public string filePath;
        public Button button;
        public Image background;
    }
}
