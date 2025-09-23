using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PreviewManager : MonoBehaviour
{
    [SerializeField] private RawImage previewImage;
    [SerializeField] private Savemanager saveManager;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text damageResistanceText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text AttackText;
    [SerializeField] private TMP_Text attackSpeedText;
    [SerializeField] private TMP_Text attackRangeText;
    [SerializeField] private TMP_Text sizeText;
    [SerializeField] private TMP_Text aggressionText;
    [SerializeField] private TMP_Text weaponTypeText;
    [SerializeField] private TMP_Text rarityText;

    public List<Texture2D> shapeTextures;

    public void ShowPreview(EnemyData data)
    {
        if (data == null) return;

        nameText.text = data.name;
        healthText.text = "Max Health: " + data.MaxHealth;
        damageResistanceText.text = "Damage Resistance: " + data.Damageresistance;
        speedText.text = "Speed: " + data.speed;
        AttackText.text = "Damage: " + data.damage;
        attackSpeedText.text = "Attack Speed: " + data.attackspeed;
        attackRangeText.text = "Attack Range: " + data.attackRange;
        sizeText.text = "Size: " + data.size;
        aggressionText.text = "Aggression: " + data.aggression;
        weaponTypeText.text = "Weapon Type: " + data.weaponstype;
        rarityText.text = "Rarity: " + data.rarity;

        if (!data.PngOrColour)
        {
            // PNG mode
            string appDataPath = Path.Combine(
                Application.persistentDataPath, "UserImages"
            );
            string pngPath = Path.Combine(appDataPath, data.pngName);
            StartCoroutine(LoadPngImage(pngPath, previewImage));
        }
        else
        {
            // Shape + Color mode
            int shapeId = data.shape;
            if (shapeId >= 0 && shapeId < shapeTextures.Count)
            {
                previewImage.texture = shapeTextures[shapeId];
                previewImage.color = new Color(data.color.x, data.color.y, data.color.z, 1f);
            }
        }
    }


    IEnumerator LoadPngImage(string filePath, RawImage img)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("PNG file not found: " + filePath);
            yield break;
        }
        string urlPath = "file:///" + filePath.Replace("\\", "/");
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(urlPath))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.Success)
                img.texture = DownloadHandlerTexture.GetContent(uwr);
            else
                Debug.LogError("Failed to load PNG: " + uwr.error);
        }
    }



}
