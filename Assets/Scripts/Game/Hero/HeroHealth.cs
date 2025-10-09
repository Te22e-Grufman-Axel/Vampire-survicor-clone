using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int armor = 0;
    public float invincibilityDuration = 0.5f; 
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
            FindFirstObjectByType<Deathmanager>().ShowDeathScreen();
        }

        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            int finalDamage = damage - armor;
            armor = armor - damage;
            if (armor < 0) armor = 0;
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
