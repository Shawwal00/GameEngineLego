using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    private int speed = 25;
    private float sensitivityX = 1.0f;

    private bool touching = false;
    public List<GameObject> wheelBlocks {get; set;}

    private void Awake()
    {
        wheelBlocks = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Wheel")
                {
                    wheelBlocks.Add(transform.GetChild(i).gameObject);
                }
            }
    }
    private void FixedUpdate()
    {
        Debug.Log(touching);
        float turn = Input.GetAxis("Turn");
        if (touching == false)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.right * (Time.deltaTime * speed));
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.left * (Time.deltaTime * speed));
            }
        }

        Rotating(turn);
    }

    void Rotating(float mouseXInput)
    {
        Rigidbody ourBody = this.GetComponent<Rigidbody>();
        
        if (mouseXInput != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);
            
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
       
    }

  
}
