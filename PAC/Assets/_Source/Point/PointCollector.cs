using System;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Tilemap))]
public class PointCollector : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private int scorePerPoint = 10;
    private int _score = 0;
    private int _bestScore = 0;
    [SerializeField] private LayerMask playerLayer;
    public event Action<int> ChangeScore;
    public event Action<int> ChangeBestScore;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Vector3 hitPosition = other.transform.position;
            Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);
            if (tilemap.HasTile(cellPosition)) 
            {
                tilemap.SetTile(cellPosition, null); 
                AddScore(scorePerPoint); 
            }
        }
    }

    private void AddScore(int amount)
    {
        _score += amount;
        ChangeScore?.Invoke(_score);
        if (_score > _bestScore)
        {
            _bestScore = _score;
            ChangeBestScore?.Invoke(_bestScore);
        }
        


    }
}