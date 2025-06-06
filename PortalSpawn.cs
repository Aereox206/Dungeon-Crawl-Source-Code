using UnityEngine;

public class PortalSpawn : MonoBehaviour
{
    public SpriteRenderer portalSprite;
    public Lever lever;

    private void Update()
    {
        if (lever.on) spawnPortal();
    }

    public void spawnPortal()
    {
        Color color = portalSprite.color;
        color.a = 1f;
        portalSprite.color = color;
    }
}
