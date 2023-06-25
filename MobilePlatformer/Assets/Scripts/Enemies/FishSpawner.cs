using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject FishPrefab;
    public float SpawnDelay;
    private bool _canSpawn = true;


    private void Update()
    {
        if (_canSpawn)
            StartCoroutine("SpawnFish");
    }


    IEnumerator SpawnFish()
    {
        Instantiate(FishPrefab, transform.position, Quaternion.identity);
        _canSpawn = false;
        yield return new WaitForSeconds(SpawnDelay);
        _canSpawn = true;
    }
}
