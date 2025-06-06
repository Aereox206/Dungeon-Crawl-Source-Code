using UnityEngine;
using UnityEngine.Tilemaps;

public class DeleteTiles : MonoBehaviour
{
    public Lever lever;
    public Tilemap tilemap;
    public Vector3Int[] tilePositions;
    public SpawnTiles spawnTiles;
    public bool tilesDeleted = false;
    public bool activateWhenOn;

    // Update is called once per frame
    void Update()
    {
        if (activateWhenOn)
        {
            if (lever.on && !tilesDeleted)
            {
                deleteTiles();
            }

            if (!lever.on) tilesDeleted = false;
        }
        else
        {
            if (!lever.on && !tilesDeleted)
            {
                deleteTiles();
            }

            if (lever.on) tilesDeleted = false;
        }

        /*
        if (!lever.on && !tilesDeleted)
        {
            deleteTiles();
        }

        if (lever.on) tilesDeleted = false;
        */
    }

    public void deleteTiles()
    {
        foreach (Vector3Int pos in tilePositions)
        {
            tilemap.SetTile(pos, null);
        }
        tilesDeleted = true;
        spawnTiles.tilesPlaced = false;
    }
}
