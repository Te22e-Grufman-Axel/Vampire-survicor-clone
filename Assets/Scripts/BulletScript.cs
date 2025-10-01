using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifeTime = 2f;
    public float damage = 1f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    
    public void SetDamage(float bulletDamage)
    {
        damage = bulletDamage;
    }
    
    public float GetDamage()
    {
        return damage;
    }
}
