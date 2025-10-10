using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform hero;
    public float speed;
    public int aggression;
    public float attackRange;

    public string weaponType;

        void Start()
        {
                hero = FindFirstObjectByType<HeroMovement>().transform;
        }
    void Update()
    {
        switch (aggression)
        {
            case 1://Run towards hero
                if (hero != null)
                {
                    Vector2 direction = (hero.position - transform.position).normalized;
                    transform.position += (Vector3)direction * speed * 2 * Time.deltaTime;
                }
                break;
            case 2://Walk towards hero
                if (hero != null)
                {
                    Vector2 direction = (hero.position - transform.position).normalized;
                    transform.position += (Vector3)direction * speed * Time.deltaTime;
                }
                break;
            case 3://range attack
                if (hero != null)
                {
                    Vector2 direction = (hero.position - transform.position).normalized;
                    float distance = Vector2.Distance(hero.position, transform.position);
                    if (distance > attackRange - 1)
                    {
                        transform.position += (Vector3)direction * speed * Time.deltaTime;
                    }
                    if (distance < attackRange - 1)
                    {
                        transform.position -= (Vector3)direction * speed * Time.deltaTime;
                    }
                }
                break;
            case 4://stay right inside camare view
                {
                    if (hero != null)
                    {
                        Vector2 direction = (hero.position - transform.position).normalized;
                        float distance = Vector2.Distance(hero.position, transform.position);
                        transform.position -= (Vector3)direction * speed * Time.deltaTime;

                        Camera cam = Camera.main;
                        float margin = 1f;
                        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
                        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
                        Vector3 newPos = transform.position;

                        if (transform.position.x < min.x + margin)
                        {
                            newPos.x = min.x + margin;
                        }
                        if (transform.position.x > max.x - margin)
                        {
                            newPos.x = max.x - margin;
                        }
                        if (transform.position.y < min.y + margin)
                        {
                            newPos.y = min.y + margin;
                        }
                        if (transform.position.y > max.y - margin)
                        {
                            newPos.y = max.y - margin;
                        }
                        transform.position = newPos;
                    }
                    break;
                }

        }
        if (hero != null)
        {
            Vector2 direction = (hero.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}
