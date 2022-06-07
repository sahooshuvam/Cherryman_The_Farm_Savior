using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CherryManager : MonoBehaviour
{
    public GameObject Collectedcherry;
    public float spawnRadius = 10;
    private int count = 10;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                Vector3 result = hit.position;
                //print("Result=" +result);
                Instantiate(Collectedcherry, result, Quaternion.identity);
            }
            else
                i--;
        }

    }
}
