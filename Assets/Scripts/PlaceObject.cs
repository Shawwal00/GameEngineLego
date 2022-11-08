using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    //Theses are all public so that the user can change these to their own assets.

    // [SerializeField] public GameObject wheel;
     [SerializeField] public GameObject engine;
    // [SerializeField] public GameObject fuel;
    // [SerializeField] public GameObject seat;
    // [SerializeField] public GameObject wings;
    // [SerializeField] public GameObject jet;

    [SerializeField] public GameObject gridBlock;
    private GameObject cursor;

    private bool firstBlock = false;
    private int currentBuildingObject = 0;
    private int buildingLimitMax;
    private List<Vector3> cursorVector {get; set;}
    private List<GameObject> buildingObjects { get; set; }
    
    private void Awake()
    {
        //In the game I can check how many specific objects are in it by using the tags.

        // wheel.gameObject.tag = "wheel";
        // engine.gameObject.tag = "engine";
        // fuel.gameObject.tag = "fuel";
        // seat.gameObject.tag = "seat";
        // wings.gameObject.tag = "wings";
        // jet.gameObject.tag = "jet";

        cursor = GameObject.Find("Cursor");

        cursorVector = new List<Vector3>();
        cursorVector.Add( new Vector3(-1,0,0));
        cursorVector.Add( new Vector3(1,0,0));
        cursorVector.Add( new Vector3(0,1,0));
        cursorVector.Add( new Vector3(0,-1,0));
        cursorVector.Add( new Vector3(0,0,1));
        cursorVector.Add( new Vector3(0,0,-1));

        buildingObjects = new List<GameObject>();
        buildingObjects.Add(gridBlock);
        buildingObjects.Add(engine);

        buildingLimitMax = buildingObjects.Count - 1;
        
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
        
        // this changes the building block
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentBuildingObject = currentBuildingObject + 1;
            if (currentBuildingObject > buildingLimitMax)
            {
                currentBuildingObject = 0;
            }
            Debug.Log(currentBuildingObject);
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentBuildingObject = currentBuildingObject - 1;
            if (currentBuildingObject < 0)
            {
                currentBuildingObject = buildingLimitMax;
            }
            Debug.Log(currentBuildingObject);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (firstBlock == false)
            {
                Instantiate(buildingObjects[currentBuildingObject], cursor.transform.position, cursor.transform.rotation);
                firstBlock = true;
            }

            else if (firstBlock == true)
            {
                float raycastLine = 10; 
                Collider[] checkDoubleCollider = Physics.OverlapSphere(transform.position, 0.1f);
                if (checkDoubleCollider.Length > 1)
                { 
                    Destroy(checkDoubleCollider[1].gameObject);
                    Instantiate(buildingObjects[currentBuildingObject], cursor.transform.position, cursor.transform.rotation);
                }

                else
                {
                    //Do a raycast and see if a block is nearby
                    for (int i = 0; i < 6; i++)
                    {
                        Debug.DrawRay(transform.position, cursorVector[i], Color.green, raycastLine);
                        Ray cursorRay = new Ray(transform.position, cursorVector[i]);
                        RaycastHit hit;
                        if (Physics.Raycast(cursorRay, out hit))
                        {
                            if (hit.distance < 1)
                            {
                                Instantiate(buildingObjects[currentBuildingObject], cursor.transform.position, cursor.transform.rotation);
                                i = 6;
                            }
                        }
                    }   
                }
            }
        }
    }
}
