using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New field", menuName ="Field Properties")]
public class FieldSettings : ScriptableObject
{
    [SerializeField] int height;
    [SerializeField] int width;
    [SerializeField] List<GameObject> tiles;

    public int GetHeight()
    {
        return height;
    }

    public int GetWidth()
    {
        return width;
    }

    public List<GameObject> GetTiles()
    {
        return tiles;
    }

    public Vector2 GetTileSize()
    {
        if (tiles != null)
        {
            GameObject tile = tiles[0];
            Vector2 size = tile.GetComponent<SpriteRenderer>().bounds.size;
            return size;
        }
        return new Vector2();
    }
}
