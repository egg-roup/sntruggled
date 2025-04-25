using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int waveNumber = 0;
    public int enemySpawnAmount = 0;

    public GameObject[] spawners;
    public GameObject enemy;

    public float waveInterval = 30f; // Time between waves
    private float countdownTimer;

    public int CurrentWave => waveNumber;
    public float TimeUntilNextWave => countdownTimer;

    private void Start()
    {
        spawners = new GameObject[5];

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;
        }

        countdownTimer = 10f;
        StartCoroutine(WaveSpawner());
    }

    private void Update() {
        if (countdownTimer > 0) {
            countdownTimer -= Time.deltaTime;
        }
    }

    private IEnumerator WaveSpawner()
    {
        Debug.Log("First wave starting in 10 seconds...");
        yield return new WaitForSeconds(10f);
        
        while (true)
        {
            StartWave();

            // Gradually increase the interval, up to a max value
            waveInterval = Mathf.Min(waveInterval + 3f, 120f); // add 5 seconds per wave, cap at 60
            countdownTimer = waveInterval; 
            
            yield return new WaitForSeconds(waveInterval);
        }
    }

    private void SpawnEnemy()
    {
        int spawnerID = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[spawnerID].transform.position, spawners[spawnerID].transform.rotation);
        Debug.Log($"Spawned enemy at Spawner {spawnerID}");
    }

    private void StartWave()
    {
        waveNumber++;
        enemySpawnAmount = 2 + (waveNumber - 1); // grows each wave

        Debug.Log($"Wave {waveNumber} started! Spawning {enemySpawnAmount} enemies.");

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }


}
