using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject objectToSpawn;

    public void Spawn()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }

    public void Spawn(bool triggered)
    {
        if (triggered == true)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }

    IEnumerator Spawn(float spawnTime)
    {
        yield return new WaitForSeconds(spawnTime);
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
