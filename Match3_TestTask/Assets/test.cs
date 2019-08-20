using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hit2D = Physics2D.RaycastAll(transform.position, Vector2.right);
        //Debug.DrawLine(transform.position, Vector2.up, Color.red);
        Debug.DrawRay(transform.position, Vector2.right, Color.red);
        //Debug.Log(hit2D.collider.gameObject);
        foreach (RaycastHit2D hit in hit2D)
        {
            if (hit.collider.gameObject != gameObject)
                Debug.Log(hit.collider.gameObject);

            //Debug.Log("me   "+gameObject);
        }
    //if (hit2D.collider.gameObject != gameObject)
    //    Debug.Log(hit2D.collider.gameObject);
}
}
