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
    public int iconID = 0;
    public ShapePicker ShapePicker;

    public UpgradeData GetUpgradeDataFromInputFields()
    {
        UpgradeData data = new UpgradeData
        {
            upgradeName = upgradeNameInput.text,
            description = upgradeDescriptionInput.text,
            affectedStat = StatToChangeDropdown.options[StatToChangeDropdown.value].text,
            icon = ShapePicker.GetSelectedButtonId(),
            effectAmount = float.Parse(effectAmountInput.text),
            purchasedLevel = 0
        };
        return data;
    }
}
