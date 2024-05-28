using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public GameObject enemyToSpawn;
    public int amountToSpawn;
    public Vector3 spawnLocation;
}
public class EnemySpawner : MonoBehaviour
{

    public Wave[] waves;
    public float timeBetweenSpawns = 1.0f; // Tijd tussen spawns

    private int currentWaveIndex = 0;
    private int enemiesSpawned = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());

        IEnumerator SpawnEnemies()
        {
            while (currentWaveIndex < waves.Length)
            {
                Wave currentWave = waves[currentWaveIndex];
                for (int i = 0; i < currentWave.amountToSpawn; i++)
                {
                    Instantiate(currentWave.enemyToSpawn, currentWave.spawnLocation, Quaternion.identity);
                    enemiesSpawned++;
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }

                currentWaveIndex++;
                enemiesSpawned = 0;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
