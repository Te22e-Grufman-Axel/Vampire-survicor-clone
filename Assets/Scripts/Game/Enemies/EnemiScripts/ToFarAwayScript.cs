using UnityEngine;

public class ToFarAwayScript : MonoBehaviour
{
   public Transform player;
    public float safeDistance = 20;
    public DecideWhenToSpawnEnemis DecideWhenToSpawnEnemis;
    public string enemyType;


    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > safeDistance)
        {
            DecideWhenToSpawnEnemis.SpawnSpecificEnemy(enemyType);
            Destroy(gameObject);
        }
    }
}
