using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Renderer rend;
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleBoundary();
    }

    void HandleBoundary()
    {
        Vector3 transformPos = transform.position;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transformPos);

        if (viewportPos.y < 0)
        {
            GameManagerScript.score -= 2;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other2D)
    {
        if (other2D.CompareTag("Bucket"))
        {
            if (other2D.GetComponent<BucketScript>().fall)
            {
                other2D.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
            else
            {
                GameManagerScript.score += 3;
                Destroy(gameObject);
            }
        }
    }
}
