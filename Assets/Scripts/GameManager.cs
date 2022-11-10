using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    private Button saveButton;

    private GameObject overallBody;
    private GameObject finalSeat;


    private void Awake()
    {
        saveButton = GameObject.Find("Save").GetComponent<Button>();
        saveButton.onClick.AddListener(saveCreation);

        overallBody = GameObject.Find("OveralBody");
    

    }

    private void saveCreation()
    {
        finalSeat = GameObject.FindGameObjectWithTag("Seat");
        overallBody.transform.position = finalSeat.transform.position;
        // Create Empty GameObject and set all the blocks as children under it
        

    }
}
