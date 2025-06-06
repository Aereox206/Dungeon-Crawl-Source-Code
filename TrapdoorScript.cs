using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapdoorScript : MonoBehaviour
{
    public Lever[] levers;

    private bool isOpen = false;
    private Tilemap tilemap;
    private TilemapRenderer tilemapRenderer;
    private TilemapCollider2D tilemapCollider;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapCollider = GetComponent<TilemapCollider2D>();
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
                if (isOpen) Toggle(false); // Close trapdoor
                return;
            }
        }

        if (!isOpen) Toggle(true); // Open trapdoor
    }

    private void Toggle(bool open)
    {
        isOpen = open;
        tilemapRenderer.enabled = !open;      
        tilemapCollider.enabled = !open;      
    }
}
