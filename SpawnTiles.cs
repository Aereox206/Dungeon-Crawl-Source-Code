using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnTiles : MonoBehaviour
{
    public FadeController fadeCon;
    public Lever lever;                   
    public Tilemap tilemap;              
    public Tile tileToPlace;
    public Vector3Int[] tilePositions;
    public bool tilesPlaced = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lever.on && !tilesPlaced)
        {
            spawnTiles();
        }
    }

    public void spawnTiles()
    {
        foreach (Vector3Int pos in tilePositions)
        {
            tilemap.SetTile(pos, tileToPlace);
        }
        tilesPlaced = true;
        fadeCon.ready = true;
    }
}
