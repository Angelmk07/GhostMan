using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class MasterPath : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на Tilemap
    public TileBase walkableTile; // Тайл, по которому можно ходить
    private static int[][] moves = {
        new int[] {1, 0}, new int[] {-1, 0}, // Вправо, влево
        new int[] {0, 1}, new int[] {0, -1}, // Вверх, вниз
        new int[] {1, 1}, new int[] {1, -1}, // Диагонали
        new int[] {-1, 1}, new int[] {-1, -1}
    };

    // Метод для поиска путей
    public List<List<Vector3Int>> FindPaths(Vector3Int start, Vector3Int end)
    {
        Queue<List<Vector3Int>> queue = new Queue<List<Vector3Int>>();
        List<List<Vector3Int>> paths = new List<List<Vector3Int>>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        queue.Enqueue(new List<Vector3Int> { start });
        visited.Add(start);
        int minSteps = int.MaxValue;

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var lastStep = path[path.Count - 1];

            if (path.Count > minSteps)
                continue;

            if (lastStep == end)
            {
                if (path.Count < minSteps)
                {
                    minSteps = path.Count;
                    paths.Clear();
                }
                paths.Add(new List<Vector3Int>(path));
                continue;
            }

            foreach (var move in moves)
            {
                Vector3Int newPos = lastStep + new Vector3Int(move[0], move[1], 0);

                // Проверяем, что клетка доступна и не занята
                if (IsCellWalkable(newPos) && !visited.Contains(newPos))
                {
                    var newPath = new List<Vector3Int>(path) { newPos };
                    queue.Enqueue(newPath);
                    visited.Add(newPos);
                }
            }
        }

        return paths;
    }

    // Проверка, доступна ли клетка для перемещения
    private bool IsCellWalkable(Vector3Int cellPosition)
    {
        // Проверяем, есть ли тайл на клетке и является ли он walkableTile
        return tilemap.GetTile(cellPosition) == walkableTile;
    }

    // Сохранение результата в JSON
    private void SavePathsToJson(List<List<Vector3Int>> paths, string filePath)
    {
        PathData pathData = new PathData { paths = paths };
        string json = JsonUtility.ToJson(pathData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Пути сохранены в " + filePath);
    }

    // Класс для сериализации данных
    [System.Serializable]
    private class PathData
    {
        public List<List<Vector3Int>> paths;
    }

    // Пример использования
    private void Start()
    {
        Vector3Int start = new Vector3Int(0, 0, 0); // Начальная позиция
        Vector3Int end = new Vector3Int(3, 3, 0);   // Конечная позиция

        var paths = FindPaths(start, end);

        if (paths.Count > 0)
        {
            Debug.Log($"Найдено {paths.Count} кратчайших путей.");
            foreach (var path in paths)
            {
                Debug.Log("Путь:");
                foreach (var step in path)
                {
                    Debug.Log(step);
                }
            }

            // Сохраняем пути в JSON
            SavePathsToJson(paths, Application.dataPath + "/paths.json");
        }
        else
        {
            Debug.Log("Путь не найден.");
        }
    }
}