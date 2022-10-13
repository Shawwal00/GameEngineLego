using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    //Theses are all public so that the user can change these to their own assets.
    
    // public GameObject wheel;
    // public GameObject baseBlock;
    // public GameObject engine;
    // public GameObject fuel;
    // public GameObject seat;
    // public GameObject wings;
    // public GameObject jet;

    public GameObject gridBlock;

    public static int gridx = 5;
    public static int gridy = 5;
    public static int gridz = 5;
    private int [, ,] grid = new int[gridx,gridy,gridz];  
    
    private void Awake()
    {
        //In the game I can check how many specific objects are in it by using the tags.
        
        // wheel.gameObject.tag = "wheel";
        // baseBlock.gameObject.tag = "baseBlock";
        // engine.gameObject.tag = "engine";
        // fuel.gameObject.tag = "fuel";
        // seat.gameObject.tag = "seat";
        // wings.gameObject.tag = "wings";
        // jet.gameObject.tag = "jet";

    }

    private void Start()
    {
        makeGrid();
    }

    private void Update()
    {
        moveCursor();
    }

    private void moveCursor()
    {
        //The below controls the cursor
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
    }

    private void makeGrid()
    {
        Vector3 firstpos = transform.position;
        Vector3 startpos = transform.position;
        for (int e = 0; e < gridy; e++)
        {
            if (e > 0)
            {
                transform.position = firstpos + new Vector3(0, e, 0);
                startpos = transform.position;
            }
            
            for (int d = 0; d < gridx; d++)
            {
                transform.position = startpos;
                if (d > 0)
                {
                    transform.position = transform.position + new Vector3(-d, 0, 0);
                }

                for (int i = 0; i < gridz; i++)
                {
                    Instantiate(gridBlock, transform.position, transform.rotation);
                    transform.position = transform.position + Vector3.forward;

                }
            }
        }
    }
}
