using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] powerUpPrefabs;
    public GameObject[] pickPointsPrefabs;
    public GameObject[] obstaclePrefabs;
    public float obstacleCheckRadius = 6.0f;
    public int maxSpawnAttemptsPerObstacle = 10;
    private GameManager gameManager;
    private float spawnRangeX = 45.0f;
    private float spawnRangeZ = 45.0f;
    // Start is called before the first frame update
    void Start()
    {

        ObstacleSpawnRandom();
        Instantiate(enemyPrefab, GenerateEnemySpawnPosition(), enemyPrefab.transform.rotation);
        InvokeRepeating("SpawnRandomPowerUps", 2, 15);
        InvokeRepeating("SpawnRandomPickUpPoints", 2, 1.5f);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnRandomPowerUps()
    {
        if (gameManager.isGameActive)
        {
            int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0.80f, Random.Range(-spawnRangeZ, spawnRangeZ));
            Instantiate(powerUpPrefabs[powerUpIndex], spawnPosition, powerUpPrefabs[powerUpIndex].transform.rotation);
        }
    }
    void SpawnRandomPickUpPoints()
    {
        if (gameManager.isGameActive)
        {
            int pickUpPointsIndex = Random.Range(0, pickPointsPrefabs.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0.80f, Random.Range(-spawnRangeZ, spawnRangeZ));
            Instantiate(pickPointsPrefabs[pickUpPointsIndex], spawnPosition, pickPointsPrefabs[pickUpPointsIndex].transform.rotation);
        }
    }
    private Vector3 GenerateEnemySpawnPosition()
    {
        float enemySpawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float enemySpawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        Vector3 randomPosition = new Vector3(enemySpawnPosX, 7, enemySpawnPosZ);

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
