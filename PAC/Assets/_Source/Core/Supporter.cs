using UnityEngine;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Supporter : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private bool IsSupportOn = true;
    [field: SerializeField] public bool CanChange { get; private set; } = false;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsSupportOn) return;
        CanChange = true;
        //Vector3 worldPos = collision.transform.position;
        //Vector3Int tilePos = tilemap.WorldToCell(worldPos);
        //Vector3 tileCenter = tilemap.GetCellCenterWorld(tilePos);

        //float threshold = 0.05f;

        //bool inCenterX = Mathf.Abs(worldPos.x - tileCenter.x) < threshold;
        //bool inCenterY = Mathf.Abs(worldPos.y - tileCenter.y) < threshold;

        //CanChange = inCenterX && inCenterY;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsSupportOn)
        {
            CanChange = false;
        }
    }
}

