using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform cannonTransform;
    [SerializeField]
    private GameObject _cannonBall;
    [SerializeField]
    private Transform _firePoint;
    private Camera _cam;
    private bool _isMousePressed = false;

    private int _ballsToSpawn = 10;
    private List<GameObject> _balls = new List<GameObject>();

    public float Power = 12.0f;

    void Start()
    {
        _cam = Camera.main;

        for(int i = 0; i < _ballsToSpawn; i++)
        {
            GameObject cannonBall = Instantiate(_cannonBall);
            cannonBall.SetActive(false);
            _balls.Add(cannonBall);
        }
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

        Vector3 FirePointToMousePointDist = (mousePos - _firePoint.position);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject cannonBall = GetBall();
            cannonBall.transform.position = _firePoint.position;
            cannonBall.transform.rotation = _firePoint.rotation;

            Rigidbody rb = cannonBall.GetComponent<Rigidbody>();

            rb.velocity = Power * _firePoint.forward;
        }
    }

    private GameObject GetBall()
    {
        foreach (GameObject balls in _balls)
        {
            if(balls.activeSelf == false)
            {
                balls.SetActive(true);
                return balls;
            }
        }
        return null;
    }

}
