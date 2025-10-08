using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    [Header("Tile Settings")]
    public SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        // Auto-assign sprite renderer if not set
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        // Ensure the tile is on a background layer
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = "Background";
            spriteRenderer.sortingOrder = -100; // Far back
        }
    }
    
    public void SetSprite(Sprite sprite)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }
    
    public void SetColor(Color color)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }
    
    private void OnValidate()
    {
        // Auto-assign in editor
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}