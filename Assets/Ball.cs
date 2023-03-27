using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionParticle;

    private Vector3 _originalPos;
    private Vector3 _originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        _originalPos = new Vector3(0f, 2.8f, -8.47f);
        _originalRotation = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -28f || transform.position.x > 28f)
        {
            transform.position = _originalPos;
            transform.rotation = Quaternion.Euler(_originalRotation);
            this.gameObject.SetActive(false);
        }
        if (transform.position.z > 75f)
        {
            transform.position = _originalPos;
            transform.rotation = Quaternion.Euler(_originalRotation);
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destructible"))
        {
            GameObject _explosionInstance = Instantiate(_explosionParticle, collision.transform.position, collision.transform.rotation);
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            Destroy(_explosionInstance, 3f);
        }
    }
}
