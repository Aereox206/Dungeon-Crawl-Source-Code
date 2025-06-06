using UnityEngine;

public class Gate : MonoBehaviour
{
    public Ghost [] ghosts;
    public Lever[] levers;
    public Sprite openSprite;
    public bool open = false;

    public void Update()
    {
        if (!open) checkLevers();
    }

    private void checkLevers()
    {
        foreach (Lever lever in levers)
        {
            if (!lever.on) return;
        }
        foreach (Ghost ghost in ghosts)
        {
            if (!ghost.GetComponent<TrapGhost>().isTrapped) return;
        }
        toggle();
    }

    public void toggle()
    {
        GetComponent<SpriteRenderer>().sprite = openSprite;
        open = !open;
    }
}
