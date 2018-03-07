using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;

    public Transform[] spawnPoint;

    public float shootRate = 0.5f; // in seconds
    public float projectileSpeed = 5f;
    public int damage = 20;
    private int count = 0;
    private float shootTimer = 0f;
    // Update is called once per frame
    void Update()
    {
        // Count up timer
        shootTimer += Time.deltaTime; // in seconds (ms)

        if (shootTimer >= shootRate && Input.GetKey(KeyCode.X))
        {
            // Loop through all spawn points
            //shootv1
            Shoot(spawnPoint[count]);
            count++;
            if (count == spawnPoint.Length)
            {
                count = 0;
            }
            //Shootv2
            //for (int i = 0; i < spawnPoint.Length; i++)
            //{
            //    // Shoot a projectile
            //    Shoot(spawnPoint[i]);
            //}
            // Reset timer
            shootTimer = 0f;
        }
    }

    // Logic for shooting a projectile
    void Shoot(Transform point)
    {
        // Instantiate a projectile prefab
        GameObject clone = Instantiate(projectilePrefab);
        // Set position of clone to player
        clone.transform.rotation = transform.rotation;
        clone.transform.Rotate(90, 0, 0);
        clone.transform.position = point.position;
        // Add force to projectile physics
            // Get Rigid Component
            Rigidbody2D rigid = clone.GetComponent<Rigidbody2D>();
            // Apply force
            rigid.AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);
    }
}
