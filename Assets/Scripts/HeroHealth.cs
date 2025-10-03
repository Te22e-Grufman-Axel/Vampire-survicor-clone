using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int armor = 5;
    public float invincibilityDuration = 1.0f; 
    private float invincibilityTimer = 0.0f;
    private bool isInvincible = false;
    public TMP_Text healthText;
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }



        if (currentHealth <= 0)
        {
            Debug.Log("Hero is dead.");
        }

        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            int finalDamage = damage - armor;
            if (finalDamage < 0) finalDamage = 0;
            currentHealth -= finalDamage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;

        }

    }
}
