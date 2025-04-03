using UnityEngine;

public class FleeingEnemy : EnemyMovement
{
    [SerializeField] private float trackingRadius = 5f;
    [SerializeField] private float turnChance = 0.1f; // Шанс развернуться к игроку
    private Transform player;
    private bool isFleeing = false;

    void Update()
    {
        if (PlayerInTrackingRange())
        {
            if (Random.value < turnChance)
            {
                isFleeing = false;
                MoveTowardsPlayer();
            }
            else
            {
                isFleeing = true;
                MoveAwayFromPlayer();
            }
        }
        else
        {
            if (isFleeing)
            {
                isFleeing = false;
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

    private void MoveAwayFromPlayer()
    {
        if (player != null)
        {
            Vector2 directionAway = (transform.position - player.position).normalized;
            transform.Translate(directionAway * speed * Time.deltaTime);
        }
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, trackingRadius);
    }
}