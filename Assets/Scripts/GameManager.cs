using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Button saveButton;

    private GameObject overallBody;
    private GameObject finalSeat;
    
    private GameObject spawn;
    [SerializeField] public PlaceObject objectList;
    
    private string savePath;
    
    private Scene currentScene;

    private bool creation = false;


    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "EditorScreen")
        {
            saveButton = GameObject.Find("Save").GetComponent<Button>();
            saveButton.onClick.AddListener(saveCreation);

            overallBody = GameObject.Find("OveralBody");
        }
    }

    private void Update()
    {
        if (currentScene.name == "VehicleTestScene" && creation == false)
        {
            spawn = GameObject.Find("Spawn");
            Debug.Log(overallBody);
            Instantiate(overallBody, spawn.transform.position, spawn.transform.rotation);
            creation = true;
        }

    }

    private void saveCreation()
    {
        finalSeat = GameObject.FindGameObjectWithTag("Seat");
        // Debug Final seat if empty throw message 
        if (finalSeat == null)
        {
            Debug.Log("A seat is required");
        }
        else
        {
            overallBody.transform.position = finalSeat.transform.position;
            overallBody.AddComponent<Rigidbody>();
            // Create Empty GameObject and set all the blocks as children under it
            for (int i = 0; i < objectList.vehicleBlocks.Count; i++)
            {
                Debug.Log(i);
                objectList.vehicleBlocks[i].transform.SetParent(overallBody.transform);
                if (objectList.vehicleBlocks[i].tag == "Wheel")
                {
                    objectList.vehicleBlocks[i].AddComponent<WheelScript>();
                    objectList.vehicleBlocks[i].AddComponent<WheelCollider>();
                    objectList.vehicleBlocks[i].GetComponent<BoxCollider>().enabled.Equals(false);
                }
            }

            overallBody.AddComponent<VehicleMovement>();
            overallBody.tag = "Vehicle";
            savePath = "Assets/MachineSaves/Vehicle.prefab";
            savePath = AssetDatabase.GenerateUniqueAssetPath(savePath);
            PrefabUtility.SaveAsPrefabAsset(overallBody, savePath);

            SceneManager.LoadScene("VehicleTestScene");
        
        }

    }
}
