using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class BonusManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject bonusPrefab;
    [SerializeField] private int maxBonuses = 4;
    [SerializeField] private float minSpawnTime = 5f;
    [SerializeField] private float maxSpawnTime = 10f;
    private int collectedBonuses = 0;

    private void Start()
    {
        StartCoroutine(SpawnBonus());
    }

    private IEnumerator SpawnBonus()
    {
        while (collectedBonuses < maxBonuses)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnTime, maxSpawnTime));
            Vector3 spawnPosition = GetValidSpawnPosition();
            if (spawnPosition != Vector3.zero)
            {
                Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3Int randomCell = new Vector3Int(
                UnityEngine.Random.Range(tilemap.cellBounds.xMin, tilemap.cellBounds.xMax),
                UnityEngine.Random.Range(tilemap.cellBounds.yMin, tilemap.cellBounds.yMax),
                0);

            if (tilemap.HasTile(randomCell))
            {
                return tilemap.GetCellCenterWorld(randomCell);
            }
        }
        return Vector3.zero;
    }

    public void CollectBonus()
    {
        collectedBonuses++;
    }
}

public class Bonus : MonoBehaviour
{
    public int scoreValue = 100;
    public float bonusDuration = 5f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
