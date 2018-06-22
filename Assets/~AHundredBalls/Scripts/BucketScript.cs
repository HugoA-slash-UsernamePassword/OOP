using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketScript : MonoBehaviour
{
    public float movementSpeed = 10f;
    private float trollSpeed;
    private float trollPoint;
    public bool fall;

    private Rigidbody2D rigid2D;
    private Renderer[] renderers;
    // Use this for initialization
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<Renderer>();

        int rendRandom = Random.Range(0, 8);

        foreach (var renderer in renderers)
        {
            renderer.material = Resources.Load("mat" + rendRandom) as Material;
        }

        int etcSelect = Random.Range(0,40);
        trollSpeed = movementSpeed;
        switch (etcSelect)
        {
            case 0:
                trollPoint = Random.Range(4, 6);
                trollSpeed = movementSpeed * Random.Range(0f, 9f);
                break;
            case 1:
                rigid2D.constraints = RigidbodyConstraints2D.FreezePositionY;
                break;
            case 2:
                rigid2D.constraints = RigidbodyConstraints2D.None;
                break;
            case 3:
                BoxCollider2D noWin = GetComponent<BoxCollider2D>();
                Destroy(noWin);
                break;
            case 4:
                fall = true;
                break;
            case 5:
                transform.localScale = new Vector3(1, -1, 1);
                break;
            case 6:
                rigid2D.constraints = RigidbodyConstraints2D.None;
                rigid2D.angularVelocity = 30;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandlePosition();
        HandleBoundary();
    }

    void HandlePosition()
    {
        rigid2D.velocity = Vector3.left * movementSpeed;
        if (transform.position.x < trollPoint) movementSpeed = trollSpeed;
    }
    void HandleBoundary()
    {
        Vector3 transformPos = transform.position;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transformPos);

        if(!IsVisible() && viewportPos.x < 0)
        {
            Destroy(gameObject);
        }
    }

    bool IsVisible()
    {
        foreach (var renderer in renderers)
        {
            if (renderer.isVisible)
            {
                return true;
            }
        }
        return false;
    }
}
