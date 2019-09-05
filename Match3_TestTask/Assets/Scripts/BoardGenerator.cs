using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] FieldSettings fieldSettings;

    List<GameObject> tiles;
    GameObject[,] allBoardTiles;//fsfsfsdf
    Vector2 tileSize;
    int boardHeight;
    int boardWidth;
    float startPositionX;
    float startPositionY;

    private void Awake()
    {
        tiles = new List<GameObject>();
        tiles = fieldSettings.GetTiles();
        allBoardTiles = new GameObject[fieldSettings.GetWidth(), fieldSettings.GetHeight()];//fdsfdsfsdf
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
                List<GameObject> possibleTiles = new List<GameObject>();
                possibleTiles.AddRange(tiles);
                possibleTiles.Remove(leftColumn[y]);
                possibleTiles.Remove(belowTile);

                int replaceTileIndex = Random.Range(0, possibleTiles.Count);
                GameObject psblTile = possibleTiles[replaceTileIndex];// to prevent the same tile and matches at the begining
                GameObject tile = Instantiate(psblTile, new Vector3(startPositionX + (tileSize.x * i), startPositionY + (tileSize.y * y)),
                   transform.rotation);
                allBoardTiles[i, y] = tile; //fsfsdfds
                tile.transform.parent = transform;

                leftColumn[y] = psblTile;
                belowTile = psblTile;
            }
        }
    }

    public void FindEmptySpace()
    {
        List<GameObject> emptyTiles = new List<GameObject>();

        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                if(allBoardTiles[x,y]==null)
                {
                    GameObject newTile= Instantiate(tiles[1], new Vector3(startPositionX + (tileSize.x * x), startPositionY + (tileSize.y * y)),
                        transform.rotation);
                    Debug.Log("null");
                }
            }
        }
    }

}
