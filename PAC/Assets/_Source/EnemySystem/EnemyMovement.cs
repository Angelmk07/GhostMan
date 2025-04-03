using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [field:SerializeField] internal float speed { private set; get; } = 2f;
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private LayerMask obstacleLayer;
    [field: SerializeField] internal LayerMask playerLayer { private set; get; } 
    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private Vector2 currentDirection;

    void Start()
    {
        ChangeDirection();
    }

    internal void Update()
    {
        if (IsBlocked(currentDirection))
        {
            ChangeDirection();
        }
        else
        {
            Move();
        }

    }

    void Move()
    {
        transform.Translate(currentDirection * speed * Time.deltaTime);
    }

    internal void ChangeDirection()
    {
        Vector2 newDirection;
        do
        {
            newDirection = directions[Random.Range(0, directions.Length)];
        }
        while (IsBlocked(newDirection));

        currentDirection = newDirection;
    }

    bool IsBlocked(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, obstacleLayer);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            if (collision.gameObject.TryGetComponent(out PlayerStat playerStat))
            {
                playerStat.TakeHit();
            }
        }
        if(((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            ChangeDirection();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(gameObject.transform.position,currentDirection);
    }
}
