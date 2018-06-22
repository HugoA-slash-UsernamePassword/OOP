using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] prefabs = null;
    public float spawnRadius = 5f;
    public float spawnRate = 1f;
    private float spawnFactor = 0f;
    // Use this for initialization
    void HandleSpawn()
    {
        spawnFactor += Time.deltaTime;
        if (spawnFactor > spawnRate)
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            Spawn(prefabs[randomIndex]);
            spawnFactor = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleSpawn();
    }
    void Spawn(GameObject _object)
    {
        GameObject newObject = Instantiate(_object);
        Vector3 randomPoint = Random.insideUnitCircle * spawnRadius;
        newObject.transform.position = transform.position + randomPoint;

        int rendRandom = Random.Range(0, 8);
        Renderer rend = newObject.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material = Resources.Load("mat" + rendRandom) as Material;
            ParticleSystem.MainModule main = newObject.GetComponent<ParticleSystem>().main;
            main.startColor = rend.material.color;
        }
    }
}
