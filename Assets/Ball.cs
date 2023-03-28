using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionParticle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -28f || transform.position.x > 28f)
        {
            this.gameObject.SetActive(false);
        }
        if (transform.position.z > 75f)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destructible"))
        {
            GameObject _explosionInstance = Instantiate(_explosionParticle, collision.transform.position, collision.transform.rotation);
            this.gameObject.SetActive(false);
            Destroy(_explosionInstance, 3f);
        }
    }
}
