using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTileController : MonoBehaviour
{
    public Lever[] levers;
    public Tilemap primaryTilemap;
    public Tilemap secondaryTilemap;

    private bool isOpen = false;
    private TilemapRenderer primaryRenderer;
    private TilemapRenderer secondaryRenderer;
    private TilemapCollider2D primaryCollider;
    private TilemapCollider2D secondaryCollider;

    private void Start()
    {
        primaryRenderer = primaryTilemap.GetComponent<TilemapRenderer>();
        secondaryRenderer = secondaryTilemap.GetComponent<TilemapRenderer>();
        primaryCollider = primaryTilemap.GetComponent<TilemapCollider2D>();
        secondaryCollider = secondaryTilemap.GetComponent<TilemapCollider2D>();

        Toggle(false);
    }

    private void Update()
    {
        CheckLevers();
    }

    private void CheckLevers()
    {
        foreach (Lever lever in levers)
        {
            if (!lever.on)
            {
                if (isOpen) Toggle(false);
                return;
            }
        }

        if (!isOpen) Toggle(true);
    }

    private void Toggle(bool open)
    {
        isOpen = open;
        primaryRenderer.enabled = !open;
        primaryCollider.enabled = !open;
        secondaryRenderer.enabled = open;
        secondaryCollider.enabled = open;
    }
}