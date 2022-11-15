using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    private Button saveButton;

    private GameObject overallBody;
    private GameObject finalSeat;

    [SerializeField] public PlaceObject objectList;
    
    private string savePath;


    private void Awake()
    {
        saveButton = GameObject.Find("Save").GetComponent<Button>();
        saveButton.onClick.AddListener(saveCreation);

        overallBody = GameObject.Find("OveralBody");
        
        
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
            // Create Empty GameObject and set all the blocks as children under it
            for (int i = 0; i < objectList.vehicleBlocks.Count; i++)
            {
                objectList.vehicleBlocks[i].transform.SetParent(overallBody.transform);
            }

            savePath = "Assets/MachineSaves/Vehicle.prefab";
            savePath = AssetDatabase.GenerateUniqueAssetPath(savePath);
            PrefabUtility.SaveAsPrefabAsset(overallBody, savePath);
        }

    }
}
