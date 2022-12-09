using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnin : MonoBehaviour
{
    public Wave[] waves;
    public GameObject ghost;

    Wave currentWave;

    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    float nextSpawnTime;

    [SerializeField] private Transform[] spawnpoints;

    private Transform selectedSpawnPoint;

    private GameObject ghostParent;

    void Start()
    {

        nextWave();
    }

    void Update()
    {
        if(enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
           enemiesRemainingToSpawn--;
           nextSpawnTime = Time.time + currentWave.timeBetweenSpawns; 

           GameObject ghostClone = Instantiate(ghost, selectedSpawnPoint.transform.position, Quaternion.identity);
           ghostClone.transform.parent = GameObject.Find("Shadows").transform;
        }

    }
    void nextWave()
    {
        SpawnGenerator();

        currentWaveNumber ++;

        currentWave = waves[currentWaveNumber - 1];

        enemiesRemainingToSpawn = currentWave.enemyCount;
    }
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }

    void SpawnGenerator()
    {
        selectedSpawnPoint = spawnpoints[UnityEngine.Random.Range(0, spawnpoints.Length)];
    }
}
