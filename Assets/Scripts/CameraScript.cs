using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] public Camera mainCamera;

    private float mouseX;
    private float mouseY;
    private float mouseZ;
    private Vector3 offset;


    private void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseZ = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(1))
        {
            offset = Quaternion.Euler(0, mouseX, 0) * offset;
        }

        float angleBetween = Vector3.Angle(Vector3.up, transform.forward);

        if (Input.GetMouseButton(0))
        {
            if ((angleBetween > 40.0f) && (mouseY < 0) || (angleBetween < 140.0f) && (mouseY > 0))
            {
                offset = Quaternion.Euler(mouseY, 0, 0) * offset;
            }
        }
 
            float dist = Vector3.Distance(target.transform.position, transform.position);

            if ((mouseZ > 0) && (dist < 15))
            {
                offset = Vector3.Scale(offset, new Vector3(1.05f, 1.05f, 1.05f));
            }

            if ((mouseZ < 0) && (dist > 1))
            {
                offset = Vector3.Scale(offset, new Vector3(0.95f, 0.95f, 0.95f));
            }

            float desiredAngle = target.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
            transform.position = target.transform.position + (rotation * offset);
            transform.LookAt(target.transform);
    }
}
