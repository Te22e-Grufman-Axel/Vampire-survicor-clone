using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using TMPro;
using SFB;


public class ImageUploadMenu : MonoBehaviour
{
    public Transform contentParent;
    public GameObject galleryItemPrefab;
    public Button deleteSelectedButton;

    private string userImagesPath;
    private GalleryItem currentSelectedItem;

    void Start()
    {
        userImagesPath = Path.Combine(Application.persistentDataPath, "UserImages");
        if (!Directory.Exists(userImagesPath))
            Directory.CreateDirectory(userImagesPath);

        deleteSelectedButton.onClick.AddListener(DeleteSelected);
        LoadGallery();
    }

    public void OpenExplorer()
    {
#if UNITY_EDITOR
        string path = UnityEditor.EditorUtility.OpenFilePanel("Choose a PNG", "", "png");
        if (!string.IsNullOrEmpty(path))
            StartCoroutine(LoadAndSaveImage(path));
#endif
#if !UNITY_EDITOR
        var pathArray = StandaloneFileBrowser.OpenFilePanel("Open File", "", "png", false);
        if (pathArray != null && pathArray.Length > 0 && !string.IsNullOrEmpty(pathArray[0]))
        {
            string path = pathArray[0];
            StartCoroutine(LoadAndSaveImage(path));
        }
#endif
    }

    IEnumerator LoadAndSaveImage(string path)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                Texture2D tex = DownloadHandlerTexture.GetContent(uwr);
                string fileName = Path.GetFileName(path);
                string savePath = Path.Combine(userImagesPath, fileName);


                byte[] pngData = tex.EncodeToPNG();


                Task.Run(() =>
                {
                    File.WriteAllBytes(savePath, pngData);
                });

                AddGalleryItem(savePath, tex);

                Debug.Log("Saved to: " + savePath);
            }
            else
            {
                Debug.LogError("Failed to load: " + uwr.error);
            }
        }
    }

    void LoadGallery()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        string[] files = Directory.GetFiles(userImagesPath, "*.png");
        foreach (string filePath in files)
            StartCoroutine(LoadImageAsync(filePath));
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
        public string GetSelectedImageName()
    {
        if (currentSelectedItem != null)
            return Path.GetFileName(currentSelectedItem.filePath);
        return "";
    }
}
