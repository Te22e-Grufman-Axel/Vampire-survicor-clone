using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateNewEnemys : MonoBehaviour
{
    public TMP_InputField maxHealthInput;
    public TMP_InputField speedInput;
    public TMP_InputField damageInput;
    public TMP_InputField nameInput;
    public TMP_InputField damageResistanceInput;
    public TMP_InputField attackSpeedInput;
    public TMP_InputField attackRangeInput;
    public TMP_Dropdown aggressionDropdown;
    public TMP_Dropdown weaponstypeDropdown;
    public TMP_Dropdown RarityDropdown;
    public TMP_InputField sizeInput;
    public ShapePicker shapePicker;
    public ImageUploadMenu imageUploadMenu;
    public ColourPickerControll colourPicker;
    public SelectMenu SelectMeny;

    public EnemyData GetEnemyDataFromInputFields()
    {
        EnemyData newEnemy = new EnemyData();
        int.TryParse(maxHealthInput.text, out newEnemy.MaxHealth);
        float.TryParse(speedInput.text, out newEnemy.speed);
        int.TryParse(damageInput.text, out newEnemy.damage);
        newEnemy.name = nameInput.text;
        int.TryParse(damageResistanceInput.text, out newEnemy.Damageresistance);
        float.TryParse(attackSpeedInput.text, out newEnemy.attackspeed);
        float.TryParse(attackRangeInput.text, out newEnemy.attackRange);

        newEnemy.aggression = aggressionDropdown.value;
        newEnemy.weaponstype = weaponstypeDropdown.options[weaponstypeDropdown.value].text;
        newEnemy.rarity = RarityDropdown.value;

        newEnemy.PngOrColour = SelectMeny.GetSelectedOption() == 1; // true if color, false if PNG
        newEnemy.shape = shapePicker.GetSelectedButtonId();

        float r = 1, g = 1, b = 1;
        if (newEnemy.PngOrColour)
        {
            Color color = colourPicker.GetCurrentColor();
            r = color.r;
            g = color.g;
            b = color.b;
            newEnemy.pngName = "";
        }
        else
        {
            newEnemy.pngName = imageUploadMenu.GetSelectedImageName();
        }
        newEnemy.color = new Vector3(r, g, b);
        int.TryParse(sizeInput.text, out newEnemy.size);

        return newEnemy;
    }
}