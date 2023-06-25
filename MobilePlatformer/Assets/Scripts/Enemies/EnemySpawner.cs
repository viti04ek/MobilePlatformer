using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float SpawnDelay;
    private bool _canSpawn = true;


    private void Update()
    {
        if (_canSpawn)
            StartCoroutine("SpawnEnemy");
    }


    IEnumerator SpawnEnemy()
    {
        Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        _canSpawn = false;
        yield return new WaitForSeconds(SpawnDelay);
        _canSpawn = true;
    }
}
