using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManagerr : MonoBehaviour
{
    public int currentLevel = 0;
    public float currentXP = 0f;
    public float allXpEarned = 0f;
    public float xpToNextLevel;
    public float xpIncreaseRate = 1.5f;
    public TMP_Text levelText;
    public Slider xpSlider;
    public UpgradeManager upgradeManager;
    public bool HasAllGuns = false;

    void Start()
    {
        xpToNextLevel = CalculateXPForNextLevel(currentLevel);
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
    }
    public void GainXP(float amount)
    {
        currentXP += amount;
        allXpEarned += amount;
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
        if (currentLevel % 5 == 0 && !HasAllGuns)
        {
            FindFirstObjectByType<GunManager>().GetNextGun();
        }
        else
        {
            upgradeManager.OpenUpgradeMenu();
        }
    }
    float CalculateXPForNextLevel(int level)
    {
        return 100f * Mathf.Pow(xpIncreaseRate, level);
    }
    public int GetScore()
    {
        return (int)allXpEarned;
    }

}
