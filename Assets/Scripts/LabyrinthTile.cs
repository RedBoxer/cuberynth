using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthTile : MonoBehaviour
{
    private bool bWall = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isWall()
    {
        return bWall;
    }

    public void ToWall()
    {
        bWall = true;
        
    }

    public void ToPath()
    {
        bWall = false;
        this.transform.position -= this.transform.up * this.transform.localScale.y;
        this.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
    }
}
