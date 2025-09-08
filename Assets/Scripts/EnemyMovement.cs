using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform hero; 
    public float speed = 2f;
    public int aggression;
    public float attackRange;

    public string weaponType;

    void Update()
    {
        if (hero != null)
        {
            Vector2 direction = (hero.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }
}
