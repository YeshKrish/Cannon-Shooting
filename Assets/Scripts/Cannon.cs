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

    private const int N_TRAJECTORY_POINTS = 10;

    private Vector3 _initialVelocity;

    private int _activeBallCount;

    [SerializeField]
    private GameObject _circleImage;

    private GameObject _cicle;

    void Start()
    {
        _cicle = Instantiate(_circleImage, Vector3.up, Quaternion.Euler(new Vector3(90f, 0f, 0f))).gameObject;

        _activeBallCount = 0;

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
        _activeBallCount = 0;
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


        //Debug.Log(mouseWorldPos);
        UpdateLineRenderer();

        float velocity = _initialVelocity.magnitude;
        float dot = Vector3.Dot(_firePoint.forward, FirePointToMousePointDist);
        float angle = Mathf.Acos(dot);

        Debug.Log(FirePointToMousePointDist);
        Vector3 dir = AngToDir(angle);
        Vector3 newPos = new Vector3(dir.x, _cicle.transform.position.y, dir.z) + new Vector3(_initialVelocity.x, 0f, _initialVelocity.z );
        _cicle.transform.position = newPos;

        if (Input.GetMouseButtonDown(0) && mouseWorldPos.y > 1f)
        {
            GameObject cannonBall = GetBall();
            cannonBall.transform.position = _firePoint.position;
            cannonBall.transform.rotation = _firePoint.rotation;

            Rigidbody rb = cannonBall.GetComponent<Rigidbody>();

            rb.AddForce(FirePointToMousePointDist * 2f * velocity * angle, ForceMode.Impulse);
        
        }

        foreach (GameObject ballCount in _balls)
        {
            if(ballCount.activeSelf)
            {
                _activeBallCount++;
            }
        }
        UIManager.Instance.Counttext.text = (_balls.Count - _activeBallCount).ToString();
    }

    private Vector3 AngToDir(float ang)
    {
        float x = Mathf.Cos(ang);
        float y = Mathf.Sin(ang);
        return new Vector3(x, y, 0f);
    }

    private GameObject GetBall()
    {
        foreach (GameObject balls in _balls)
        {
            if(balls.activeSelf == false)
            {
                Rigidbody rb = balls.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
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
