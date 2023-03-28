using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject _destructible;

    private int _destructibleCount = 10;

    private float maxXaxis = 27f;
    private float minXaxis = -27f;
    private float maxZaxis = 13f;
    private float minZaxis = 56f;

    private List<GameObject> _destructibleObjs = new List<GameObject>();

    private bool _isSpawnDestructiblePressed = false;

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
        for (int i = 0; i < _destructibleCount; i++)
        {
            float xValue = Random.Range(minXaxis, maxXaxis);
            float zValue = Random.Range(minZaxis, maxZaxis);
            GameObject destructible = Instantiate(_destructible, new Vector3(xValue, 4f, zValue), Quaternion.identity);
            destructible.SetActive(false);
            _destructibleObjs.Add(destructible);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject GetDestructible()
    {
        foreach (GameObject destructibles in _destructibleObjs)
        {
            if(destructibles.activeSelf == false)
            {
                destructibles.SetActive(true);
                return destructibles;
            }
        }
        return null;
    }

    public void SpawnDestructible()
    { 
        if(_isSpawnDestructiblePressed == false)
        {
            for (int i = 0; i < _destructibleCount; i++)
            {
                GameObject destructible = GetDestructible();   
            }
            _isSpawnDestructiblePressed = true;
        }
        else
        {
            foreach(GameObject destructible in _destructibleObjs)
            {
                destructible.SetActive(false);
            }
            for (int i = 0; i < _destructibleCount; i++)
            {
                float xValue = Random.Range(minXaxis, maxXaxis);
                float zValue = Random.Range(minZaxis, maxZaxis);
                GameObject destructible = GetDestructible();
                destructible.transform.position = new Vector3(xValue, 4f, zValue);
                destructible.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
