using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnersObject
    {
        public GameObject prefab;
    }
    public SpawnersObject[] objects;
    public float spawnRate = 2f;
    public List<GameObject> spawnedPipes = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }
    private void Spawn()
    {
        int randomIndex = Random.Range(0, objects.Length);
        GameObject newPipe = Instantiate(objects[randomIndex].prefab, transform.position, Quaternion.identity);
        spawnedPipes.Add(newPipe.gameObject);
    }
    
}
