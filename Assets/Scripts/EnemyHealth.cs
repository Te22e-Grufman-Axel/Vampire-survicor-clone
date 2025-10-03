// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health;
    public int maxHealth;
    public int Damageresistance;
    public string enemyType;
    public float xpValue; 
    
    private LevelManagerr levelManager;

    public void Start()
    {
        health = maxHealth;
        levelManager = FindFirstObjectByType<LevelManagerr>();
    }
    public void damage(int damage)
    {
        int actualDamage = damage - Damageresistance;
        if (actualDamage < 0)
        {
            actualDamage = 0;
        }

        health -= actualDamage;

        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (levelManager != null && xpValue > 0)
        {
            levelManager.GainXP(xpValue);
        }
        
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletScript bullet = other.GetComponent<BulletScript>();
            if (bullet != null)
            {
                int bulletDamage = (int)bullet.GetDamage();
                damage(bulletDamage);
                Destroy(other.gameObject);
            }
        }
    }
}
