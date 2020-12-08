using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] objectPrefabs;
    public GameObject[] obstaclePrefabs;
    public float obstacleCheckRadius = 6.0f;
    public int maxSpawnAttemptsPerObstacle = 10;
    private float spawnRangeX = 45.0f;
    private float spawnRangeZ = 45.0f;
    private float spawnDelay = 1.0f;
    private float spawnInterval = 1.5f;
    private SphereController sphereControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab, GenerateEnemySpawnPosition(), enemyPrefab.transform.rotation);

        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
        sphereControllerScript = GameObject.Find("Player").GetComponent<SphereController>();
        ObstacleSpawnRandom();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnObjects()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0.7f, Random.Range(-spawnRangeZ, spawnRangeZ));
        int objectIndex = Random.Range(0, objectPrefabs.Length);

        if (!sphereControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[objectIndex], spawnLocation, objectPrefabs[objectIndex].transform.rotation);
        }
    }
    private Vector3 GenerateEnemySpawnPosition()
    {
        float enemySpawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float enemySpawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        Vector3 randomPosition = new Vector3(enemySpawnPosX, 0, enemySpawnPosZ);

        return randomPosition;
    }
    void ObstacleSpawnRandom()
    {
        for (int i = 0; i <= 30; i++)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            //Create a spawn position variable
            Vector3 spawnPosition = Vector3.zero;
            //check if we can spawn or not in this position
            bool validPosition = false;
            //checks how many times we tried to spawn in this position
            int spawnAttempts = 0;
            while (!validPosition && spawnAttempts < maxSpawnAttemptsPerObstacle)
            {
                spawnAttempts++;
                spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
                validPosition = true;
                //Collect all colliders withing the obstacles check radius
                Collider[] colliders = Physics.OverlapSphere(spawnPosition, obstacleCheckRadius);
                //go through each of the collider collected
                foreach (Collider col in colliders)
                {
                    if (col.tag == "Obstacle")
                    {
                        validPosition = false;
                    }
                }
            }
            if (validPosition)
            {
                Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, obstaclePrefabs[obstacleIndex].transform.rotation);
            }
        }
    }
}
