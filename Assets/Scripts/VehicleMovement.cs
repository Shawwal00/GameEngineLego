using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VehicleMovement : MonoBehaviour
{
    private float speed = 0;
    private int maxSpeed = 30;
    private float acceleration = 0.1f;

    private int wheelTouchingCounter = 0;
    
    private float maxfuel = 0;
    
    private float sensitivityX = 1.0f;

    private bool touching = false;

    private bool forward = false;
    private bool back = false;
    
    private TextMeshProUGUI fuelText;
    private TextMeshProUGUI speedText;
    
    public List<GameObject> wheelBlocks {get; set;}

    private void Awake()
    {
        GetComponent<VehicleMovement>().enabled = true;
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
                    acceleration = acceleration + 0.1f;
                }
                
                else if (transform.GetChild(i).tag == "Fuel")
                {
                    maxfuel = maxfuel + 50;
                }
            }
        
        fuelText = GameObject.Find("Fuel").GetComponent<TextMeshProUGUI>();
        speedText = GameObject.Find("Speed").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        uiText();
    }

    private void FixedUpdate()
    {
        Debug.Log(wheelBlocks[0]);
        float turn = Input.GetAxis("Turn");
        
        
        wheelTouchingCounter = 0;
        for (int i = 0; i < wheelBlocks.Count; i++)
        {
            if (wheelBlocks[i].GetComponent<WheelScript>().movement == false)
            {
               
                Debug.Log(touching);
                wheelTouchingCounter = wheelTouchingCounter + 1;
                if (wheelTouchingCounter == wheelBlocks.Count - 1)
                {
               
                    touching = false;
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
                if (speed < maxSpeed)
                {
                    speed = speed + acceleration;
                }

                forward = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (speed < maxSpeed)
                {
                    speed = speed + acceleration;
                }
                back = true;
            }
        }

        if (maxfuel < 0)
        {
            forward = false;
            back = false;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
        }
        
        if (Input.GetKeyUp(KeyCode.S))
        {
            back = false;
        }

        if (forward == true)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * speed));
            maxfuel = maxfuel - 0.2f;
        }

        if (back == true)
        {
            transform.Translate(Vector3.left * (Time.deltaTime * speed));
            maxfuel = maxfuel - 0.2f;
        }

        if (maxfuel > 0)
        {
            Rotating(turn);
        }

        if (forward == false && back == false)
        {
            if (speed > 0)
            {
                speed = speed - 0.5f;
            }
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
}
