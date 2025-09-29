using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    public Transform hero;
    public HeroHealth heroHealth;
    public DecideWhenToSpawnEnemis decideWhenToSpawnEnemis;
    public int damage;
    public float attackSpeed;
    public float attackRange;
    public string weaponType;
    private float distance;
    private float TimeUntilNextAttack = 0f;

    private bool canAttack = true;

    void Start()
    {
        hero = FindObjectOfType<HeroMovement>().transform;
        heroHealth = hero.GetComponent<HeroHealth>();
        decideWhenToSpawnEnemis = FindObjectOfType<DecideWhenToSpawnEnemis>();

    }



    void Update()
    {
        Vector2 direction = (hero.position - transform.position).normalized;
        distance = Vector2.Distance(hero.position, transform.position);
        if (hero != null && canAttack)
        {
            canAttack = false;
            TimeUntilNextAttack = 0f;

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


        TimeUntilNextAttack += Time.deltaTime;
        if (TimeUntilNextAttack >= attackSpeed)
        {
            canAttack = true;
        }

    }

    void PerformMeleeAttack()
    {
        if (distance < attackRange)
        {
            if (heroHealth != null)
            {
                heroHealth.TakeDamage(damage);
            }
        }
    }
    void PerformExplodeAttack()
    {
        if (distance < attackRange)
        {
            if (heroHealth != null)
            {
                heroHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
    void PerformSpittingAttack()
    {
        if (distance < attackRange)
        {
            if (heroHealth != null)
            {
                heroHealth.TakeDamage(damage);
            }
        }
    }
    void PerformNecromacyAttack()
    {
        decideWhenToSpawnEnemis.SpawnEnemy();
    }
}
