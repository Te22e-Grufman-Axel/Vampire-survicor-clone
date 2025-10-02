using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 1f;
    public float maxRange = 10f;
    private Vector3 startPosition;
    private bool hasStartPosition = false;

    void Start()
    {
        startPosition = transform.position;
        hasStartPosition = true;
    }
    
    void Update()
    {
        if (hasStartPosition)
        {
            float distanceTraveled = Vector3.Distance(startPosition, transform.position);
            if (distanceTraveled >= maxRange)
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void SetDamage(float bulletDamage)
    {
        damage = bulletDamage;
    }
    
    public void SetRange(float range)
    {
        maxRange = range;
    }
    
    public float GetDamage()
    {
        return damage;
    }
}
