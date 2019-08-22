using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTile : MonoBehaviour
{
    [SerializeField] Color selectedColor;

    SpriteRenderer renderer=null;
    static MyTile previousTile = null;// to be able to see selected tile from previousScripts
    bool isSelected=false;
    bool matchFound = false;

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
                    RemoveMatched();
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

    private List<GameObject> FindMatch(Vector2 direction)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

        while(hit.collider!=null && 
            hit.collider/*.gameObject*/.GetComponent<SpriteRenderer>().sprite==renderer.sprite)//hlllkjjhiohhhlhoih
        {
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, direction);//hit from match to the next tile
        }
        return matchingTiles;
    }

    private void RemoveMatched()
    {
        List<GameObject> tilesToRemove = new List<GameObject>();

        tilesToRemove.AddRange(FindMatch(Vector2.up));
        tilesToRemove.AddRange(FindMatch(Vector2.down));
        tilesToRemove.AddRange(FindMatch(Vector2.left));
        tilesToRemove.AddRange(FindMatch(Vector2.right));

        if(tilesToRemove.Count>=2)
        {
            for(int i=0; i<tilesToRemove.Count; i++)
            {
                Destroy(tilesToRemove[i]);
            }
        }
    }
}
