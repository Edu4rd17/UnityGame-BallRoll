using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] objectPrefabs;
    //public GameObject[] obstaclePrefabs;
    private float spawnRange = 45.0f;
    private float spawnDelay = 1.0f;
    private float spawnInterval = 1.5f;
    private SphereController sphereControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab, GenerateEnemySpawnPosition(), enemyPrefab.transform.rotation);

        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
        sphereControllerScript = GameObject.Find("Player").GetComponent<SphereController>();

    }

    // Update is called once per frame
    void Update()
    {
    }
    void SpawnObjects()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-spawnRange, spawnRange), 0.7f, Random.Range(-spawnRange, spawnRange));
        int objectIndex = Random.Range(0, objectPrefabs.Length);

        if (!sphereControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[objectIndex], spawnLocation, objectPrefabs[objectIndex].transform.rotation);
        }
    }
    private Vector3 GenerateEnemySpawnPosition()
    {
        float enemySpawnPosX = Random.Range(-spawnRange, spawnRange);
        float enemySpawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPosition = new Vector3(enemySpawnPosX, 0, enemySpawnPosZ);

        return randomPosition;
    }
    /* void ObstacleSpawn()
     {

         int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
         Vector3 spawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));

         Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, obstaclePrefabs[obstacleIndex].transform.rotation);

     }*/
}
