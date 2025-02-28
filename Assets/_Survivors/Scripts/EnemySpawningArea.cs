using UnityEngine;

// it should have minimal and maximum radius around the player
public class EnemySpawningArea : MonoBehaviour
{
    [SerializeField] float minRadius = 5f;
    [SerializeField] float maxRadius = 10f;

    public Vector2 GetRandomPosition()
    {
        float randomRadius = Random.Range(minRadius, maxRadius);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 randomPosition = new Vector3(randomDirection.x, randomDirection.y) * randomRadius;
        return randomPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }
}
