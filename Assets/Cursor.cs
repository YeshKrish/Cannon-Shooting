using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Sprite circleSprite;

    // Start is called before the first frame update
    void Start()
    {
        GameObject circleObj = new GameObject("Circle");
        circleObj.transform.parent = transform;
        SpriteRenderer sr = circleObj.AddComponent<SpriteRenderer>();
        sr.sprite = circleSprite;
        sr.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
