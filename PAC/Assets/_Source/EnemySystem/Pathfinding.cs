using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    public Tilemap tilemap; 
    public List<Vector2Int> targetPositions; 
    public float speed = 2f; 

    private List<Vector2Int> path; 
    private int currentTargetIndex = 0; 
    private Vector2Int startPos; 

    void Start()
    {
        if (targetPositions.Count == 0)
        {
            Debug.LogError("Не заданы цели для перемещения!");
            return;
        }

        startPos = (Vector2Int)tilemap.WorldToCell(transform.position);
        FindNewPath(); 
    }

    void Update()
    {
        MoveAlongPath();
    }

    void FindNewPath()
    {
        Vector2Int targetPos = targetPositions[currentTargetIndex];
        path = FindPath(startPos, targetPos);

        if (path == null)
        {
            Debug.Log("Маршрут не найден к цели " + targetPos);
        }
        else
        {
            Debug.Log("Маршрут найден: " + string.Join(" -> ", path));
        }
    }

    List<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(start);

        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        cameFrom[start] = start;

        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == target)
            {
                return ReconstructPath(cameFrom, start, target);
            }

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighbor = current + dir;
                if (!cameFrom.ContainsKey(neighbor) && IsWalkable(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        return null;
    }

    List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int start, Vector2Int target)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int current = target;

        while (current != start)
        {
            path.Add(current);
            current = cameFrom[current];
        }

        path.Reverse();
        return path;
    }

    bool IsWalkable(Vector2Int position)
    {
        Vector3Int tilePosition = new Vector3Int(position.x, position.y, 0);
        return tilemap.GetTile(tilePosition) == null;
    }

    void MoveAlongPath()
    {
        if (path != null && path.Count > 0)
        {
            Vector2 target = (Vector2)tilemap.CellToWorld((Vector3Int)path[0]) + new Vector2(0.5f, 0.5f); 
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                path.RemoveAt(0);
            }
        }
        else
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Count;
            startPos = (Vector2Int)tilemap.WorldToCell(transform.position);
            FindNewPath();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (targetPositions != null)
        {
            foreach (var pos in targetPositions)
            {
                Vector3 worldPos = tilemap.CellToWorld((Vector3Int)pos) + new Vector3(0.5f, 0.5f, 0);
                Gizmos.DrawSphere(worldPos, 0.3f);
            }
        }
    }
}
