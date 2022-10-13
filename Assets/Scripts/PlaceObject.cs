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

    private float cursorSpeed;

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

    private void Update()
    {
        moveCursor();
    }

    private void moveCursor()
    {
        cursorSpeed = 5;
        //The below controls the cursor
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.right * (Time.deltaTime * cursorSpeed), Space.World);
            Debug.Log("Cursor");
        }
    }
}
