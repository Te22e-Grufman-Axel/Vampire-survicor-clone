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
        levelManager = FindObjectOfType<LevelManagerr>();
    }
    public void damage(int damage)
    {
        Debug.Log("Enemy damaged: " + damage);
        Debug.Log("Current health before damage: " + health);
        Debug.Log("Damage resistance: " + Damageresistance);

        int actualDamage = damage - Damageresistance;
        if (actualDamage < 0)
        {
            actualDamage = 0;
        }

        Debug.Log("Actual damage after resistance: " + actualDamage);
        health -= actualDamage;
        Debug.Log("Health after taking damage: " + health);

        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (levelManager != null && xpValue > 0)
        {
            Debug.Log($"Enemy {enemyType} killed! Gained {xpValue} XP");
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
                Debug.Log("Hit by bullet");
                int bulletDamage = (int)bullet.GetDamage();
                damage(bulletDamage);
                Destroy(other.gameObject);
            }
            Debug.Log("Hit by bullet2");
        }
    }
}
