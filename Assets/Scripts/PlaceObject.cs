using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private TextMeshProUGUI blockText;

    private GameObject cursor;

    private bool firstBlock = false;
    private bool seatCursorMove = false;
    private bool fuelCursorMove = false;
    private bool otherCursorMove = false;
    private bool seatPlaced = false;

    private int currentBuildingObject = 0;
    private int cursorLayout = 0;
    private int buildingLimitMax;
    private List<Vector3> cursorVector { get; set; }
    private List<GameObject> buildingObjects { get; set; }

    public List<GameObject> vehicleBlocks {get; set;}
    private GameObject blockCopy;
        
    private void Awake()
    {
        // To find the blocks later on
        seat.tag = "Seat";
        gridBlock.tag = "BaseBlock";
        engine.tag = "Engine";
        wheel.tag = "Wheel";
        fuel.tag = "Fuel";
        
        cursor = GameObject.Find("Cursor");

        //for the Raycast
        cursorVector = new List<Vector3>();
        cursorVector.Add( new Vector3(-1,0,0));
        cursorVector.Add( new Vector3(1,0,0));
        cursorVector.Add( new Vector3(0,1,0));
        cursorVector.Add( new Vector3(0,-1,0));
        cursorVector.Add( new Vector3(0,0,1));
        cursorVector.Add( new Vector3(0,0,-1));

        //Adding in the diffrent blocks
        buildingObjects = new List<GameObject>();
        buildingObjects.Add(gridBlock);
        buildingObjects.Add(engine);
        buildingObjects.Add(wheel);
        buildingObjects.Add(fuel);
        buildingObjects.Add(seat);
        buildingLimitMax = buildingObjects.Count - 1;

        blockText = GameObject.Find("CurrentBlock").GetComponent<TextMeshProUGUI>();

        vehicleBlocks = new List<GameObject>();

    }
    
    private void Update()
    {
        moveCursor();
        placeObject();
        switchCursor();
        uiText();
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

        // This is the delete key
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
                        Destroy(blockCopy = deleteCollider[i].gameObject);
                        vehicleBlocks.Remove(blockCopy);
                    }
                }
            }
        }

        //Placing the building block
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(seatPlaced);

            // the First block
            if (firstBlock == false)
            {
                blockCopy = Instantiate(buildingObjects[currentBuildingObject], cursor.transform.position,
                    cursor.transform.rotation);
                firstBlock = true;
                vehicleBlocks.Add(blockCopy);
            }

            // Any blocks after
            else if (firstBlock == true)
            {
                // Checks to see if there is already a bloc there and if so it will delete it.
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
                            Destroy(blockCopy = checkDoubleCollider[i].gameObject);
                            vehicleBlocks.Remove(blockCopy);
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
                        blockCopy =  Instantiate(buildingObjects[currentBuildingObject], cursor.transform.position,
                            cursor.transform.rotation);
                        vehicleBlocks.Add(blockCopy);
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
                                    blockCopy = Instantiate(buildingObjects[currentBuildingObject], cursor.transform.position,
                                        cursor.transform.rotation);
                                    i = 6; 
                                    vehicleBlocks.Add(blockCopy);
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
        // This method will ensure that the blocks are always alligned 
        
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
    
    private void uiText()
    {
        if (currentBuildingObject == 0)
        {
            blockText.text = "This is a Base Block";
        }
        
        else if (currentBuildingObject == 1)
        {
            blockText.text = "This is a Engine Block";
        }
        
        else if (currentBuildingObject == 2)
        {
            blockText.text = "This is a Wheel Block";
        }
        
        else if (currentBuildingObject == 3)
        {
            blockText.text = "This is a Fuel Block";
        }
        
        else if (currentBuildingObject == 4)
        {
            blockText.text = "This is a Seat Block";
        }
    }

}


