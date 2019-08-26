﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTile : MonoBehaviour
{
    [SerializeField] Color selectedColor;

    SpriteRenderer renderer=null;
    BoardGenerator board = null;
    static MyTile previousTile = null;// to be able to see selected tile from previousScripts
    bool isSelected=false;
    bool matchFound = false;

    private void Awake()
    {
        board = FindObjectOfType<BoardGenerator>();
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
                if (GetSurroundingTiles().Contains(previousTile.gameObject))//check if we hit the nearest tile
                {
                    Swap(previousTile.renderer);
                    //previousTile.RemoveMatched();
                    previousTile.Deselect();
                    RemoveMatched();
                    board.FindEmptySpace();
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
        float maxDistance = 1f;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, maxDistance);

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
        float maxDistance = 2f;
        List<GameObject> matchingTiles = new List<GameObject>();
        GameObject prevTile=gameObject;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, maxDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == renderer.sprite)
            {
                if (hit.collider.gameObject != gameObject)
                {
                    matchingTiles.Add(gameObject);
                    matchingTiles.Add(hit.collider.gameObject);
                    Debug.Log(matchingTiles.Count);

                    prevTile = hit.collider.gameObject;
                }
            }
            else
            {
                return matchingTiles;
            }
        }
        return matchingTiles;
    }

    private void RemoveMatched()
    {
        List<GameObject> tilesToRemoveHeight = new List<GameObject>();

        tilesToRemoveHeight.AddRange(FindMatch(Vector2.up));
        tilesToRemoveHeight.AddRange(FindMatch(Vector2.down));
        tilesToRemoveHeight.AddRange(FindMatch(Vector2.right));
        tilesToRemoveHeight.AddRange(FindMatch(Vector2.left));

        if (tilesToRemoveHeight.Count>=4)
        {
            for(int i=0; i<tilesToRemoveHeight.Count; i++)
            {
                tilesToRemoveHeight[i].GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }
}
