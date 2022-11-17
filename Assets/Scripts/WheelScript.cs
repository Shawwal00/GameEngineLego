using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    private WheelCollider wheelCollider;
    private JointSpring wheelSpring;
    private BoxCollider boxCollider;

    [SerializeField] public bool movement = false;

    private void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
        boxCollider = GetComponent<BoxCollider>();
        
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(1.5f, 1.5f, 1.5f);

        wheelSpring.damper = 10;
        wheelSpring.spring = 10;
        wheelSpring.targetPosition = 0.5f;
        wheelCollider.suspensionSpring = wheelSpring;
        wheelCollider.forwardFriction = new WheelFrictionCurve();
        wheelCollider.sidewaysFriction = new WheelFrictionCurve();
    }

    private void OnTriggerEnter(Collider other)
    {
        movement = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        movement = false;
    }
}
