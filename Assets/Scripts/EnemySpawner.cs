using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 1f;
    WaveConfigSO currentWave;
    [SerializeField] public bool isSpawning;

    void Start()
    {
        StartCoroutine(SpawnEnemies());    
    }
    IEnumerator SpawnEnemies()
    {
        do
        {
            for(int waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++)
            {
                WaveConfigSO wave = waveConfigs[waveIndex];
                int randomIndex = Random.Range(0, waveIndex);
                waveConfigs[waveIndex] = waveConfigs[randomIndex];
                waveConfigs[randomIndex] = wave; 
            }

            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                                currentWave.GetStartingWaypoint().position,
                                Quaternion.identity,
                                transform);
                    yield return new WaitForSecondsRealtime(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSecondsRealtime(timeBetweenWaves);
            }
        } while (isSpawning);
    }
    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}
