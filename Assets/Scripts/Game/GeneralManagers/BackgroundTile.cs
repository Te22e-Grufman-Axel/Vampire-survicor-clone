using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    [Header("Tile Settings")]
    public SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
            spriteRenderer.sortingLayerName = "Background";
            spriteRenderer.sortingOrder = -100; // Far back
    }
    
    public void SetSprite(Sprite sprite)
    {
            spriteRenderer.sprite = sprite;
    }
    
    public void SetColor(Color color)
    {
            spriteRenderer.color = color;
    }
    
    private void OnValidate()
    {
            spriteRenderer = GetComponent<SpriteRenderer>();
    }
}