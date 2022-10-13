using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    //Theses are all public so that the user can change these to their own assets.

    // [SerializeField] public GameObject wheel;
    // [SerializeField] public GameObject baseBlock;
    // [SerializeField] public GameObject engine;
    // [SerializeField] public GameObject fuel;
    // [SerializeField] public GameObject seat;
    // [SerializeField] public GameObject wings;
    // [SerializeField] public GameObject jet;

    [SerializeField] public GameObject gridBlock;
    private GameObject cursor;

    private bool firstBlock = false;

    Vector3 cursorVector = new Vector3(-1,0,0);
    
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

        cursor = GameObject.Find("Cursor");

    }

    private void Start()
    {
       
    }

    private void Update()
    {
        moveCursor();
    }

    private void moveCursor()
    {
        //The below controls the cursor
        if (Input.GetKeyDown(KeyCode.S))
        {
            cursor.transform.position = cursor.transform.position + Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            cursor.transform.position = cursor.transform.position + Vector3.up;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cursor.transform.position = cursor.transform.position + Vector3.forward;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cursor.transform.position = cursor.transform.position + Vector3.back;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cursor.transform.position = cursor.transform.position + Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cursor.transform.position = cursor.transform.position + Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (firstBlock == false)
            {
                Instantiate(gridBlock, cursor.transform.position, cursor.transform.rotation);
                firstBlock = true;
            }

            if (firstBlock == true)
            {
                float sd = 5000;
                //Do a raycast and see if a block is nearby
                Ray cursorRay = new Ray(transform.position, cursorVector);
                RaycastHit hit;
                Debug.DrawRay(transform.position, cursorVector, Color.green, sd);

                for (int i = 0; i < 6; i++)
                {
                    if (Physics.Raycast(cursorRay, out hit))
                    {
                        if (hit.distance < 0.5)
                        {
                            Instantiate(gridBlock, cursor.transform.position, cursor.transform.rotation);
                        }
                    }
                }
            }
        }
    }
}
