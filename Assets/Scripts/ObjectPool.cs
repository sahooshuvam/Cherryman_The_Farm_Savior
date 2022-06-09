using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectPool : MonoBehaviour
{
    public GameObject spiderPrefabs;
    public int number;
    public float spawnRadius;
    public bool spawnOnStart = true;
    Vector3 result;

    // Start is called before the first frame update
    void Awake()
    {
        number = Random.Range(10,15); // taking the random value
        if (spawnOnStart)
        {
            SpawnInsect(); // Spawning of the spider
        }
    }

    private void SpawnInsect()
    {
        for (int i = 0; i < number; i++) 
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius; // within a radius we spawning the spider 
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                result = hit.position; // when its hit the ground 
                //print("Result=" +result);
                Instantiate(spiderPrefabs, result, Quaternion.identity);// when its hit the ground in that time Spider is instantiate
            }
            //else
                //i--;
        }
    }
}
