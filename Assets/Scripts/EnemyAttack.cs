using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    public Transform hero;
    public int damage;
    public float attackSpeed;
    public float attackRange;
    public string weaponType;
    private float distance;

    void Update()
    {
        Vector2 direction = (hero.position - transform.position).normalized;
        distance = Vector2.Distance(hero.position, transform.position);
        if (hero != null)
        {
            switch (weaponType)
            {
                case "Melee":
                    PerformMeleeAttack();
                    break;
                case "Explode":
                    PerformExplodeAttack();
                    break;
                case "Spitting":
                    PerformSpittingAttack();
                    break;
                case "Necromacy":
                    PerformNecromacyAttack();
                    break;
                default:
                    Debug.LogWarning("Unknown weapon type: " + weaponType);
                    break;
            }
        }
    }

    void PerformMeleeAttack()
    {
            Debug.Log("Performing Melee Attack with damage: " + damage);
    }
    void PerformExplodeAttack()
    {
        Debug.Log("Performing Ranged Attack with damage: " + damage);
    }
    void PerformSpittingAttack()
    {
        Debug.Log("Performing Spitting Attack with damage: " + damage);
    }
    void PerformNecromacyAttack()
    {
        Debug.Log("Performing Necromacy Attack with damage: " + damage);
    }
}
