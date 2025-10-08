using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;



public class UpgradePreviewManager : MonoBehaviour
{
    public UpgradeData upgradeData;
    public RawImage iconImage;
    public TMP_Text upgradeNameText;
    public TMP_Text descriptionText;
    public TMP_Text effectAmountText;
    public TMP_Text affectedStatText;
    public List<Texture2D> IconTextures;


    public void ShowPreview(UpgradeData data)
    {
        upgradeData = data;

        upgradeNameText.text = data.upgradeName;
        int IconID = data.icon;
        if (IconID >= 0 && IconID < IconTextures.Count)
        {
            iconImage.texture = IconTextures[IconID-1];
        }
        affectedStatText.text = "Affected Stat: " + data.affectedStat;
        effectAmountText.text = "Effect Amount: " + data.effectAmount.ToString();
        descriptionText.text = "Description: " + data.description;

    }



}




