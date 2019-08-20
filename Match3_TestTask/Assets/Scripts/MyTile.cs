using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTile : MonoBehaviour
{
    [SerializeField] Color selectedColor;

    SpriteRenderer renderer=null;
    static MyTile previousTile = null;// to be able to see selected tile from previousScripts
    bool isSelected=false;

    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (isSelected)
        {
            Deselect();
        }
        else
        {
            if (previousTile == null)
            {
                Select();
            }
            else
            {
                if (GetSurroundingTiles().Contains(previousTile.gameObject))
                {
                    Swap(previousTile.renderer);
                    previousTile.Deselect();
                }

                else
                {
                    previousTile.Deselect();
                    Select();
                }
            }
        }
    }

    private void Select()
    {
        renderer.color = selectedColor;
        isSelected = true;
        previousTile = gameObject.GetComponent<MyTile>();
    }

    private void Deselect()
    {
        renderer.color = Color.white;
        isSelected = false;
        previousTile = null;
    }

    private void Swap(SpriteRenderer tileRenderer)
    {
        if (tileRenderer.sprite == renderer.sprite)
            return;

        Sprite middleSprite = tileRenderer.sprite;
        tileRenderer.sprite = renderer.sprite;
        renderer.sprite = middleSprite;
    }

    private GameObject GetNextTile(Vector2 direction)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction);

        foreach (RaycastHit2D hit2D in hit)
        {
            if(hit2D.collider!=null)
            {
                if (hit2D.collider.gameObject != gameObject)
                {
                    return hit2D.collider.gameObject;
                }
            }
        }
        return null;
    }

    private List<GameObject> GetSurroundingTiles()
    {
        List<GameObject> surTiles = new List<GameObject>();

        surTiles.Add(GetNextTile(Vector2.up));
        surTiles.Add(GetNextTile(Vector2.down));
        surTiles.Add(GetNextTile(Vector2.left));
        surTiles.Add(GetNextTile(Vector2.right));

        return surTiles;
    }
}
