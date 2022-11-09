using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    //Theses are all public so that the user can change these to their own assets.

     [SerializeField] public GameObject wheel;
     [SerializeField] public GameObject engine;
     [SerializeField] public GameObject fuel;
     [SerializeField] public GameObject seat;
    // [SerializeField] public GameObject wings;
    // [SerializeField] public GameObject jet;

    [SerializeField] public GameObject gridBlock;
    private GameObject cursor;

    private bool firstBlock = false;
    
    private bool seatCursorMove = false;
    private bool fuelCursorMove = false;
    private bool otherCursorMove = false;

    private bool seatPlaced = false;
    
    private int currentBuildingObject = 0;
    private int cursorLayout = 0;
    private int buildingLimitMax;
    private List<Vector3> cursorVector {get; set;}
    private List<GameObject> buildingObjects { get; set; }
    
    private void Awake()
    {
        seat.tag = "Seat";
        
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
        buildingObjects.Add(wheel);
        buildingObjects.Add(fuel);
        buildingObjects.Add(seat);

        buildingLimitMax = buildingObjects.Count - 1;
        
    }
    
    private void Update()
    {
        moveCursor();
        placeObject();
        switchCursor();
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
    }

    private void placeObject()
    {

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            Collider[] deleteCollider = Physics.OverlapSphere(transform.position, 0.1f);
            if (deleteCollider.Length > 1)
            {
                for (int i = 0; i < deleteCollider.Length; i++)
                {
                    if (deleteCollider[i].CompareTag("Seat"))
                    {
                        seatPlaced = false;
                    }

                    if (!deleteCollider[i].CompareTag("Cursor"))
                    {
                        Destroy(deleteCollider[i].gameObject);
                    }
                }
            }
        }

        //Placing the building block
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(seatPlaced);

            if (firstBlock == false)
            {
                Instantiate(buildingObjects[currentBuildingObject], cursor.transform.position,
                    cursor.transform.rotation);
                firstBlock = true;
            }

            else if (firstBlock == true)
            {
                Collider[] checkDoubleCollider = Physics.OverlapSphere(transform.position, 0.1f);
                if (checkDoubleCollider.Length > 1)
                {
                    for (int i = 0; i < checkDoubleCollider.Length; i++)
                    {
                        if (checkDoubleCollider[i].CompareTag("Seat"))
                        {
                            seatPlaced = false;
                        }

                        if (!checkDoubleCollider[i].CompareTag("Cursor"))
                        {
                            Destroy(checkDoubleCollider[i].gameObject);
                        }
                    }

                    if (seatPlaced == true && buildingObjects[currentBuildingObject].CompareTag("Seat"))
                    {
                        
                    }
                    
                    else 
                    {
                        if (buildingObjects[currentBuildingObject].CompareTag("Seat") && seatPlaced == false)
                        {
                            seatPlaced = true;
                        }
                        Instantiate(buildingObjects[currentBuildingObject], cursor.transform.position,
                            cursor.transform.rotation);
                    }
                    
                }

                else
                {
                    float raycastLine = 10;
                    //Do a raycast and see if a block is nearby
                    for (int i = 0; i < 6; i++)
                    {
                        Debug.DrawRay(transform.position, cursorVector[i], Color.green, raycastLine);
                        Ray cursorRay = new Ray(transform.position, cursorVector[i]);
                        RaycastHit hit;
                        if (Physics.Raycast(cursorRay, out hit))
                        {
                            if (hit.distance < 1.5)
                            {
                                if (seatPlaced == true && buildingObjects[currentBuildingObject].CompareTag("Seat"))
                                {
                        
                                }
                                
                                else 
                                {
                                    if (buildingObjects[currentBuildingObject].CompareTag("Seat") && seatPlaced == false)
                                    {
                                        seatPlaced = true;
                                    }
                                    Instantiate(buildingObjects[currentBuildingObject], cursor.transform.position,
                                        cursor.transform.rotation);
                                    i = 6; 
                                }
                            }
                        }
                    }
                }
            }
        }

    }

    private void switchCursor()
    {
        
        if (currentBuildingObject == 3)
        {
            otherCursorMove = false;
            seatCursorMove = false;
            transform.localScale = new Vector3(2, 1, 1);
            if (fuelCursorMove == false)
            {
                if (cursorLayout == 0)
                {
                    cursor.transform.position = cursor.transform.position + new Vector3(0.5f,0,0);
                }
                else if (cursorLayout == 2)
                {
                    cursor.transform.position = cursor.transform.position + new Vector3(0.5f,-0.5f,0);
                }
                fuelCursorMove = true;
                cursorLayout = 1;
            }
        }

        else if (currentBuildingObject == 4)
        {
            otherCursorMove = false;
            fuelCursorMove = false;
            transform.localScale = new Vector3(1, 2, 1);
            if (seatCursorMove == false)
            {
                if (cursorLayout == 0)
                {
                    cursor.transform.position = cursor.transform.position + new Vector3(0,0.5f,0);
                }
                else if (cursorLayout == 1)
                {
                    cursor.transform.position = cursor.transform.position + new Vector3(-0.5f,0.5f,0);
                }
                seatCursorMove = true;
                cursorLayout = 2;
            }
        }
        
        else
        {
            seatCursorMove = false;
            fuelCursorMove = false;
            transform.localScale = new Vector3(1,1,1);
            if (otherCursorMove == false)
            {
                if (cursorLayout == 1)
                {
                    cursor.transform.position = cursor.transform.position + new Vector3(-0.5f, 0f, 0);
                }
                else
                {
                    cursor.transform.position = cursor.transform.position + new Vector3(0, 0.5f, 0);
                }
                otherCursorMove = true;
                cursorLayout = 0;
            }
        }
    }
}
