using UnityEngine;

public class TrackingEnemy : EnemyMovement
{
    [SerializeField] private float trackingRadius = 5f;

    private Transform player;
    private bool isTrackingPlayer = false;

    void Update()
    {
        if (PlayerInTrackingRange())
        {
            isTrackingPlayer = true;
            MoveTowardsPlayer();
        }
        else
        {
            if (isTrackingPlayer)
            {
                isTrackingPlayer = false;
                ChangeDirection();
            }
            base.Update();
        }
    }

    private bool PlayerInTrackingRange()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, trackingRadius, playerLayer);
        if (hit != null)
        {
            player = hit.transform;
            return true;
        }
        return false;
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            transform.Translate(directionToPlayer * speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trackingRadius);
    }
}