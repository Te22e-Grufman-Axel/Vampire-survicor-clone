using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour
{
    [Header("Background Settings")]
    public GameObject backgroundTilePrefab; // Assign your background prefab here
    public Transform player; // Reference to the player transform
    public float tileSize = 20f; // Size of each background tile
    public int renderDistance = 3; // How many tiles around the player to render

    public int poolSize = 50; // Pool size if using pooling

    private Dictionary<Vector2Int, GameObject> activeTiles = new Dictionary<Vector2Int, GameObject>();
    private Queue<GameObject> tilePool = new Queue<GameObject>();
    private Vector2Int lastPlayerGridPos;

    private void Start()
    {
        InitializePool();

        if (player != null)
        {
            lastPlayerGridPos = WorldToGridPosition(player.position);
            GenerateTilesAroundPosition(lastPlayerGridPos);
        }
    }

    private void Update()
    {
        if (player == null) return;

        Vector2Int currentPlayerGridPos = WorldToGridPosition(player.position);

        if (currentPlayerGridPos != lastPlayerGridPos)
        {
            UpdateBackgroundTiles(currentPlayerGridPos);
            lastPlayerGridPos = currentPlayerGridPos;
        }
    }

    private void InitializePool()
    {
        if (backgroundTilePrefab == null)
        {
            Debug.LogError("Background tile prefab is not assigned!");
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject tile = Instantiate(backgroundTilePrefab, transform);
            tile.SetActive(false);
            tilePool.Enqueue(tile);
        }
    }

    private Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPosition.x / tileSize),
            Mathf.FloorToInt(worldPosition.y / tileSize)
        );
    }

    private Vector3 GridToWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(
            gridPosition.x * tileSize + tileSize * 0.5f,
            gridPosition.y * tileSize + tileSize * 0.5f,
            0f
        );
    }

    private void UpdateBackgroundTiles(Vector2Int playerGridPos)
    {
        HashSet<Vector2Int> requiredPositions = new HashSet<Vector2Int>();

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                Vector2Int pos = playerGridPos + new Vector2Int(x, y);
                requiredPositions.Add(pos);
            }
        }

        List<Vector2Int> tilesToRemove = new List<Vector2Int>();
        foreach (var kvp in activeTiles)
        {
            if (!requiredPositions.Contains(kvp.Key))
            {
                tilesToRemove.Add(kvp.Key);
            }
        }

        foreach (var pos in tilesToRemove)
        {
            RemoveTile(pos);
        }

        foreach (var pos in requiredPositions)
        {
            if (!activeTiles.ContainsKey(pos))
            {
                CreateTile(pos);
            }
        }
    }

    private void GenerateTilesAroundPosition(Vector2Int centerPos)
    {
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                Vector2Int pos = centerPos + new Vector2Int(x, y);
                if (!activeTiles.ContainsKey(pos))
                {
                    CreateTile(pos);
                }
            }
        }
    }

    private void CreateTile(Vector2Int gridPos)
    {
        GameObject tile = GetTileFromPool();
        if (tile == null) return;

        tile.transform.position = GridToWorldPosition(gridPos);
        tile.SetActive(true);
        activeTiles[gridPos] = tile;
    }

    private void RemoveTile(Vector2Int gridPos)
    {
        if (activeTiles.TryGetValue(gridPos, out GameObject tile))
        {
            activeTiles.Remove(gridPos);
            ReturnTileToPool(tile);
        }
    }

    private GameObject GetTileFromPool()
    {
        if (tilePool.Count > 0)
        {
            return tilePool.Dequeue();
        }
        else if (backgroundTilePrefab != null)
        {
            return Instantiate(backgroundTilePrefab, transform);
        }

        return null;
    }

    private void ReturnTileToPool(GameObject tile)
    {
        tile.SetActive(false);
        tilePool.Enqueue(tile);

    }
}
