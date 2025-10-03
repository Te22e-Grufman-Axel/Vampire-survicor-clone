using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManagerr : MonoBehaviour
{
    public int currentLevel = 0;
    public float currentXP = 0f;
    public float xpToNextLevel;
    public float xpIncreaseRate = 1.5f;
    public TMP_Text levelText;
    public Slider xpSlider;
    public UpgradeManager upgradeManager;

    void Start()
    {
        xpToNextLevel = CalculateXPForNextLevel(currentLevel);
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
    }   
    public void GainXP(float amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }
    void Update()
    {
        levelText.text = "Level: " + currentLevel;
        xpSlider.maxValue = xpToNextLevel;
        xpSlider.value = currentXP;
    }
    void LevelUp()
    {
        currentLevel++;
        currentXP -= xpToNextLevel;
        xpToNextLevel = CalculateXPForNextLevel(currentLevel);
        upgradeManager.OpenUpgradeMenu();
    }
    float CalculateXPForNextLevel(int level)
    {
        return 100f * Mathf.Pow(xpIncreaseRate, level); 
    }

}
