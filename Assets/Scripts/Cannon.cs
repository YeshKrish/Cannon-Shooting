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
    [SerializeField]
    private LineRenderer _lineRenderer;

    private int _ballsToSpawn = 10;
    private List<GameObject> _balls = new List<GameObject>();

    public float Power = 1.0f;

    private const int N_TRAJECTORY_POINTS = 10;

    private Vector3 _initialVelocity;

    void Start()
    {

        _lineRenderer.positionCount = N_TRAJECTORY_POINTS;

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

        _initialVelocity = mouseWorldPos - _firePoint.position;

        Vector3 FirePointToMousePointDist = (mouseWorldPos - _firePoint.position).normalized;
        float distancex = Mathf.Abs(mouseWorldPos.x - _firePoint.position.x);
        float distancey = Mathf.Abs(mouseWorldPos.y - _firePoint.position.y);
        float distancez = Mathf.Abs(mouseWorldPos.z - _firePoint.position.z);
        Debug.Log(distancey);
        if(distancey > 7.7f)
        {
            distancey = 6.5f;
        }
        UpdateLineRenderer();

        if (Input.GetMouseButtonDown(0))
        {
            GameObject cannonBall = GetBall();
            cannonBall.transform.position = _firePoint.position;
            cannonBall.transform.rotation = _firePoint.rotation;

            Rigidbody rb = cannonBall.GetComponent<Rigidbody>();

            rb.AddForce(new Vector3(FirePointToMousePointDist.x * (Power + distancex), FirePointToMousePointDist.y * (Power + distancey), FirePointToMousePointDist.z * (Power + distancez)), ForceMode.Impulse);
        
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

    private void UpdateLineRenderer()
    {
        float g = Physics.gravity.magnitude;
        float velocity = _initialVelocity.magnitude;
        float angle = Mathf.Atan2(_initialVelocity.y, _initialVelocity.x);

        Vector3 start = _firePoint.position;

        float timeStamp = 0.1f;
        float fTime = 0f;
        for (int i = 0; i < N_TRAJECTORY_POINTS; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle);
            float dy = velocity * fTime * Mathf.Sin(angle) - (g * fTime * fTime / 2f);
            Vector3 pos = new Vector3(start.x + dx, start.y + dy, start.z + dy);
            _lineRenderer.SetPosition(i, pos);
            fTime += timeStamp;
        }
    }
}
