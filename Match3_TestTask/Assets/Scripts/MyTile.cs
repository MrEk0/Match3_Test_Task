using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTile : MonoBehaviour
{
    [SerializeField] Color selectedColor;

    SpriteRenderer renderer=null;
    static MyTile previousTile = null;// to be able to see selected tile from previousScripts
    bool isSelected=false;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (isSelected)
        {
            renderer.color = Color.white;
            isSelected = false;
            previousTile = null;
        }
        else
        {
            if (previousTile == null)
            {
                renderer.color = selectedColor;
                isSelected = true;
                previousTile = gameObject.GetComponent<MyTile>();
            }
            else
            {
                Swap(previousTile.renderer);
                previousTile.renderer.color = Color.white;
                previousTile = null;
                isSelected = false;
            }
        }
    }

    private void Swap(SpriteRenderer tileRenderer)
    {
        if (tileRenderer.sprite == renderer.sprite)
            return;

        Sprite middleSprite = tileRenderer.sprite;
        tileRenderer.sprite = renderer.sprite;
        renderer.sprite = middleSprite;
    }
}
