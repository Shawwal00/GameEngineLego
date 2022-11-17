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

    private WheelFrictionCurve forwardFriction;
    private WheelFrictionCurve sidewaysFriction;

    [SerializeField] public bool movement = false;

    private void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
        boxCollider = GetComponent<BoxCollider>();

        wheelCollider.radius = 0.5f;
        wheelCollider.mass = 1;
        
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(1.5f, 1.5f, 1.5f);

        wheelSpring.damper = 10;
        wheelSpring.spring = 10;
        wheelSpring.targetPosition = 0.5f;
        wheelCollider.suspensionSpring = wheelSpring;

        forwardFriction.extremumSlip = 0.4f;
        forwardFriction.extremumValue = 1;
        forwardFriction.asymptoteSlip = 0.8f;
        forwardFriction.asymptoteValue = 0.75f;
        forwardFriction.stiffness = 1;
        wheelCollider.forwardFriction = forwardFriction;

        sidewaysFriction.extremumSlip = 0.2f;
        sidewaysFriction.extremumValue = 1;
        sidewaysFriction.asymptoteSlip = 0.5f;
        sidewaysFriction.asymptoteValue = 0.75f;
        sidewaysFriction.stiffness = 1;
        wheelCollider.sidewaysFriction = sidewaysFriction;
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
