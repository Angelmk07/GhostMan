using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPathfinder : MonoBehaviour
{
    public Tilemap tilemap;
    public string saveFileName = "paths.json";
    private Vector3Int startPosition;
    private Dictionary<Vector3Int, List<List<Vector3Int>>> paths = new();

    void Start()
    {
        startPosition = tilemap.WorldToCell(transform.position);
        FindAllPaths();
        SavePathsToJson();
    }

    void FindAllPaths()
    {
        HashSet<Vector3Int> visited = new();
        ExplorePaths(startPosition, new List<Vector3Int>(), visited);
    }

    void ExplorePaths(Vector3Int position, List<Vector3Int> currentPath, HashSet<Vector3Int> visited)
    {
        if (visited.Contains(position)) return;
        if (tilemap.HasTile(position)) return;

        currentPath.Add(position);
        visited.Add(position);

        if (!paths.ContainsKey(position))
        {
            paths[position] = new List<List<Vector3Int>>();
        }
        paths[position].Add(new List<Vector3Int>(currentPath));

        List<Vector3Int> directions = new()
        {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };

        List<Task> tasks = new();
        foreach (var dir in directions)
        {
            Vector3Int nextPos = position + dir;
            if (!tilemap.HasTile(nextPos))
            {
                tasks.Add(Task.Run(() => ExplorePaths(nextPos, new List<Vector3Int>(currentPath), new HashSet<Vector3Int>(visited))));
            }
        }
    }

    void SavePathsToJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        string json = JsonUtility.ToJson(new PathData { Paths = paths }, true);
        File.WriteAllText(filePath, json);
        Debug.Log(Application.persistentDataPath);
    }

    public Dictionary<Vector3Int, List<List<Vector3Int>>> LoadPathsFromJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        if (!File.Exists(filePath)) return new Dictionary<Vector3Int, List<List<Vector3Int>>>();
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<PathData>(json).Paths;
    }

    [System.Serializable]
    public class PathData
    {
        public Dictionary<Vector3Int, List<List<Vector3Int>>> Paths;
    }
}
