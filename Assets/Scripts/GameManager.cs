using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject _destructible;

    private int _destructibleCount = 15;

    private float maxXaxis = 27f;
    private float minXaxis = -27f;
    private float maxZaxis = 13f;
    private float minZaxis = 70f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnDestructible()
    {

        for (int i = 0; i < _destructibleCount; i++)
        {
            float xValue = Random.Range(minXaxis, maxXaxis);
            float zValue = Random.Range(minZaxis, maxZaxis);
            Instantiate(_destructible, new Vector3(xValue, 4f, zValue), Quaternion.identity);
        }
    }
}
