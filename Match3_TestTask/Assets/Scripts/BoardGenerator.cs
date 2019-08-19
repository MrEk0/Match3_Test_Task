using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] FieldSettings fieldSettings;

    List<GameObject> tiles;
    Vector2 tileSize;
    int boardHeight;
    int boardWidth;
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
        GameObject[] leftColumn = new GameObject[boardHeight];
        GameObject belowTile = null;

        for (int i = 0; i < boardWidth; i++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                //List<GameObject> possibleTiles = new List<GameObject>();
                //possibleTiles.AddRange(tiles);

                //int nextTileIndex = Random.Range(0, tiles.Count);
                //GameObject newTile = Instantiate(tiles[nextTileIndex], new Vector3(startPositionX + (tileSize.x * i), startPositionY + (tileSize.y * y)),
                //    transform.rotation);
                //newTile.transform.parent = transform;

                List<GameObject> possibleTiles = new List<GameObject>();
                possibleTiles.AddRange(tiles);
                possibleTiles.Remove(leftColumn[y]);
                possibleTiles.Remove(belowTile);

                int replaceTileIndex = Random.Range(0, possibleTiles.Count);
                GameObject repTile = possibleTiles[replaceTileIndex];
                GameObject replaceTile = Instantiate(repTile, new Vector3(startPositionX + (tileSize.x * i), startPositionY + (tileSize.y * y)),
                   transform.rotation);
                //newTile.transform.parent = transform;
                //Debug.Log(possibleTiles.Count);
                //Debug.Log("new    "+newTile);
                //newTile = repTile;
                //Debug.Log(newTile);
                //Debug.Log("replace    "+possibleTiles[replaceTileIndex]);
                //Destroy(newTile);

                leftColumn[y] = repTile;
                belowTile = repTile;
            }
        }
    }

}
