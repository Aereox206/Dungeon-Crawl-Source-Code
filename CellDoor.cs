using UnityEngine;

public class CellDoor : MonoBehaviour
{
    public Lever[] levers;

    private bool isOpen = false;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
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
        spriteRenderer.enabled = !open;   
        boxCollider.enabled = !open;     
    }
}
