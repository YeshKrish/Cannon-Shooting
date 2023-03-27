using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _destructible;

    private int _destructibleCount = 15;

    private float maxXaxis = 27f;
    private float minXaxis = -27f;
    private float maxZaxis = 13f;
    private float minZaxis = 70f;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        for (int i = 0; i < _destructibleCount; i++)
        {
            float xValue = Random.Range(minXaxis, maxXaxis);
            float zValue = Random.Range(minZaxis, maxZaxis);
            Instantiate(_destructible, new Vector3(xValue, 4f, zValue), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
