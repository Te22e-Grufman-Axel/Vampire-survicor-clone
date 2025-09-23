using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

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
    public Button createButton;

    public ShapePicker shapePicker;
    public ImageUploadMenu imageUploadMenu;
    void Start()
    {
        createButton.onClick.AddListener(CreateEnemyFromInput);
    }

    void CreateEnemyFromInput()
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

        newEnemy.PngOrColour = (shapePicker.GetSelectedButtonId() == 0) ? false : true;
        newEnemy.shape = shapePicker.GetSelectedButtonId();

        float r = 1, g = 1, b = 1;
        if (newEnemy.PngOrColour)
        {
            Color color = FindObjectOfType<ColourPickerControll>().GetCurrentColor();
            r = color.r;
            g = color.g;
            b = color.b;
            newEnemy.pngName = ""; // No PNG for shapes
        }
        else
        {
            newEnemy.pngName = imageUploadMenu.GetSelectedImageName();
        }
        newEnemy.color = new Vector3(r, g, b);
        int.TryParse(sizeInput.text, out newEnemy.size);





        string filePath = Path.Combine(Application.streamingAssetsPath, "enemyData.json");
        EnemyDataList enemyList = new EnemyDataList();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            enemyList = JsonUtility.FromJson<EnemyDataList>(json);
            if (enemyList == null || enemyList.enemies == null)
            {
                enemyList = new EnemyDataList { enemies = new List<EnemyData>() };
            }
        }
        else
        {
            enemyList.enemies = new List<EnemyData>();
        }

        enemyList.enemies.Add(newEnemy);
        string newJson = JsonUtility.ToJson(enemyList, true);
        File.WriteAllText(filePath, newJson);
    }
}
