using System.Collections;
using System.Collections.Generic;
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
    public TMP_InputField colorRInput;
    public TMP_InputField colorGInput;
    public TMP_InputField colorBInput;
    public TMP_InputField sizeInput;
    public Button createButton;

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

        float r = 0, g = 0, b = 0;
        float.TryParse(colorRInput.text, out r);
        float.TryParse(colorGInput.text, out g);
        float.TryParse(colorBInput.text, out b);
        newEnemy.color = new Vector3(r/255, g/255, b/255);
        int.TryParse(sizeInput.text, out newEnemy.size);

        string json = JsonUtility.ToJson(newEnemy, true);

        // Save to file in persistent data path
        string fileName = $"Enemy_{newEnemy.name}_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);
        System.IO.File.WriteAllText(filePath, json);

        Debug.Log($"Enemy data saved to: {filePath}");
    }
}
