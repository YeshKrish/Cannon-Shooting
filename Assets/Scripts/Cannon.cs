using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform cannonTransform;
    private Camera _cam;
    private bool _isMousePressed = false;


    void Start()
    {
        _cam = Camera.main;
    }


    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);


        Vector3 cannonToMouse = mouseWorldPos - cannonTransform.position;
        cannonToMouse.y = 0f;

        Quaternion lookRotation = Quaternion.FromToRotation(cannonTransform.forward, cannonToMouse);


        Quaternion targetRotation = cannonTransform.rotation * lookRotation;

        cannonTransform.rotation = targetRotation;

    }
}
