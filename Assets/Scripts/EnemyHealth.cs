using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health;
    public int maxHealth;
    public int Damageresistance;

    public void Start()
    {
        health = maxHealth;
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
        Destroy(this.gameObject);
    }
}
