using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float invincibilityDuration = 1.0f; 
    private float invincibilityTimer = 0.0f;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
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
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;

        }

    }
}
