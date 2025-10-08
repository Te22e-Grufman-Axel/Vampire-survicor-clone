using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
public class CreateNewUpgrades : MonoBehaviour
{
    public TMP_InputField upgradeNameInput;
    public TMP_InputField upgradeDescriptionInput;
    public TMP_InputField effectAmountInput;
    public TMP_Dropdown StatToChangeDropdown;
    public int iconID;
    public ShapePicker ShapePicker;

    public UpgradeData GetUpgradeDataFromInputFields()
    {        
        UpgradeData data = new UpgradeData
        {
            upgradeName = upgradeNameInput.text,
            description = upgradeDescriptionInput.text,
            icon = ShapePicker.GetSelectedButtonId(),
            affectedStat = StatToChangeDropdown.options[StatToChangeDropdown.value].text,
            purchasedLevel = 0,
            effectAmount = float.TryParse(effectAmountInput.text, out float effectAmount) ? effectAmount : 0f
        };
        return data;
    }
}