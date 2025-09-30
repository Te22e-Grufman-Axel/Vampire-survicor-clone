using UnityEngine;

public class GunScript : MonoBehaviour
{
    //find enemi colider, apply 100 damage to enemy
    public int damage = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy, destroying instantly.");
            Destroy(collision.gameObject);
        }
    }
}
