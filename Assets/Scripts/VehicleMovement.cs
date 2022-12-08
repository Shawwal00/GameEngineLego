using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VehicleMovement : MonoBehaviour
{
    private Scene currentScene;
    
    private int maxSpeed = 10000;
    private int wheelTouchingCounter = 0;
    
    private float speed = 0;
    private float acceleration = 100f;
    private float accelerationIncrease = 100f;
    private float gravityIncrease = 50;
    private float maxfuel = 0;
    private float sensitivityX = 1.0f;

    private bool touching = false;
    private bool forward = false;
    private bool back = false;
    
    private TextMeshProUGUI fuelText;
    private TextMeshProUGUI speedText;

    private Rigidbody vehicleRigidbody;
    private Vector3 forcePosition;
    
    public List<GameObject> wheelBlocks {get; set;}

    
    private void Awake()
    {
        vehicleRigidbody = GetComponent<Rigidbody>();
        vehicleRigidbody.angularDrag = 100;
        vehicleRigidbody.mass = 10;
        currentScene = SceneManager.GetActiveScene();
        GetComponent<VehicleMovement>().enabled = true;
        
        // Setting up the various diffrent blocks
        wheelBlocks = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Wheel")
                {
                    wheelBlocks.Add(transform.GetChild(i).gameObject);
                }

                else if (transform.GetChild(i).tag == "Engine")
                {
                    maxSpeed = maxSpeed + 5;
                    acceleration = acceleration + accelerationIncrease;
                }
                
                else if (transform.GetChild(i).tag == "Fuel")
                {
                    maxfuel = maxfuel + 100;
                }

                else if (transform.GetChild(i).tag == "Seat")
                {
                    forcePosition = transform.GetChild(i).position;
                }
            }

        if (currentScene.name == "VehicleTestScene")
        {
            fuelText = GameObject.Find("Fuel").GetComponent<TextMeshProUGUI>();
            speedText = GameObject.Find("Speed").GetComponent<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        uiText();
        wheelCheck();
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
        }
        
        if (Input.GetKeyUp(KeyCode.S))
        {
            back = false;
        }
        
        if (maxfuel < 0)
        {
            forward = false;
            back = false;
        }
    }

    private void FixedUpdate()
    {
      
        float turn = Input.GetAxis("Turn");
        
        if (forward == true)
        {
            vehicleRigidbody.AddRelativeForce(new Vector3(speed * Time.deltaTime,0, 0));
            
            maxfuel = maxfuel - 0.2f;
            
            if (speed < maxSpeed)
            {
                speed = speed + acceleration;
            }

        }

        else if (back == true)
        {
            vehicleRigidbody.AddRelativeForce(new Vector3(-speed * Time.deltaTime,0, 0));

            maxfuel = maxfuel - 0.2f;

            if (speed < maxSpeed)
            {
                speed = speed + acceleration;
            }
        }
        
        else if (forward == false && back == false)
        {
            if (speed > 0)
            {
                speed = speed - acceleration;
            }
        }
        
        

        if (maxfuel > 0)
        {
            Rotating(turn);
        }
    }

    private void Rotating(float mouseXInput)
    {
        Rigidbody ourBody = this.GetComponent<Rigidbody>();
        
        if (mouseXInput != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);
            
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }

    private void uiText()
    {
        fuelText.text = "fuel = " + maxfuel.ToString();
        speedText.text = "speed = " + speed.ToString();
    }

    private void wheelCheck()
    {
        wheelTouchingCounter = 0;
        for (int i = 0; i < wheelBlocks.Count; i++)
        {
            if (wheelBlocks[i].GetComponent<WheelScript>().movement == false)
            {
                
                wheelTouchingCounter = wheelTouchingCounter + 1;
                if (wheelTouchingCounter == wheelBlocks.Count - 1)
                {
               
                    touching = false;
                    vehicleRigidbody.AddRelativeForce(new Vector3(0,-gravityIncrease * Time.deltaTime, 0));
                }
            }
            else
            {
                touching = true;
            }
        }
        if (touching == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                forward = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                back = true;
            }
        }
    }

}
