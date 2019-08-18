using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] FieldSettings fieldSettings;

    List<GameObject> tiles;
    Vector2 tileSize;
    float boardHeight;
    float boardWidth;
    float startPositionX;
    float startPositionY;

    private void Awake()
    {
        tiles = new List<GameObject>();
        tiles = fieldSettings.GetTiles();
        boardHeight = fieldSettings.GetHeight();
        boardWidth = fieldSettings.GetWidth();
        startPositionX = transform.position.x;
        startPositionY = transform.position.y;
        tileSize = fieldSettings.GetTileSize();
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        for (float i = 0; i < boardWidth; i++)
        {
            for (float y = 0; y < boardHeight; y++)
            {
                int nextTileIndex = Random.Range(0, tiles.Count-1);
                GameObject newTile= Instantiate(tiles[nextTileIndex], new Vector3(startPositionX + (tileSize.x * i), startPositionY + (tileSize.y * y)),
                    transform.rotation);
                newTile.transform.parent = transform;
            }
        }
    }

}
