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
                if(allBoardTiles[x,y].GetComponent<SpriteRenderer>().sprite==null)
                {
                    MoveTilesDown(x, y);
                }
            }
        }
    }

    private void MoveTilesDown(int xPosition, int yPosition)
    {
        List<SpriteRenderer> sprites = new List<SpriteRenderer>();
        int nullCount = 0;

        for(int y=yPosition; y<boardHeight; y++)
        {
            SpriteRenderer sprite = allBoardTiles[xPosition, y].GetComponent<SpriteRenderer>();
            //if (sprite.sprite == null)
            //    nullCount++;
            //else
            //    sprites.Add(sprite);
            if (sprite.sprite == null)
            {
                nullCount++;
            }

            sprites.Add(sprite);
        }
        //Debug.Log(sprites.Count);
        //Debug.Log("null  "+nullCount);

        for (int i = 0; i < nullCount; i++)
        {
            for (int j = 0; j < sprites.Count - 1; j++)
            {
                sprites[j].sprite = sprites[j + 1].sprite;
                //sprites[j + 1].sprite = null;
                sprites[j + 1].sprite = GenerateNewSprite(xPosition, yPosition);
            }
        }
    }

    private Sprite GenerateNewSprite(int x, int y)
    {
        List<Sprite> sprites = new List<Sprite>();
        foreach (var tile in tiles)
        {
            sprites.Add(tile.GetComponent<SpriteRenderer>().sprite);
        }

        if (x > 0)
            sprites.Remove(allBoardTiles[x-1, y].GetComponent<SpriteRenderer>().sprite);
        if (x < boardWidth-1)
            sprites.Remove(allBoardTiles[x + 1, y].GetComponent<SpriteRenderer>().sprite);
        if (y > 0)
            sprites.Remove(allBoardTiles[x, y - 1].GetComponent<SpriteRenderer>().sprite);

        return sprites[Random.Range(0, sprites.Count)];
    }

}
