using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct EnemyType
{
    public GameObject enemyToSpawn;
    public int amountToSpawn;
}

[System.Serializable]
public struct Wave
{
    public EnemyType[] enemyTypes;

    public Vector3 spawnLocation;
}

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public float timeBetweenSpawns = 1.0f; // Time between spawns

    private int currentWaveIndex = 0;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];

            foreach (var enemyType in currentWave.enemyTypes)
            {
                for (int i = 0; i < enemyType.amountToSpawn; i++)
                {
                    GameObject enemy = Instantiate(enemyType.enemyToSpawn, currentWave.spawnLocation, Quaternion.identity);
                    spawnedEnemies.Add(enemy);
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }

            while (spawnedEnemies.Count > 0)
            {
                spawnedEnemies.RemoveAll(item => item == null);
                yield return null;
            }
            currentWaveIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

/*
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
    private List<GameObject> spawnedEnemies = new List<GameObject>();
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
                    GameObject enemy=Instantiate(currentWave.enemyToSpawn, currentWave.spawnLocation, Quaternion.identity);
                    spawnedEnemies.Add(enemy);
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }

                
                enemiesSpawned = 0;

                while(spawnedEnemies.Count > 0)
                {
                    spawnedEnemies.RemoveAll(item => item == null);
                    yield return null;
                }
                currentWaveIndex++;
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/