using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
#pragma warning disable 0649
    [SerializeField] private List<WaveConfig> waveConfigs;
#pragma warning restore 0649
    private readonly bool isLooping = true;
    private const int StartingWave = 0;

    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } 
        while (isLooping);

    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        for (var enemyCount = 0; enemyCount < wave.GetNumberOfEnemies(); enemyCount++)
        {
            GameObject newEnemy = Instantiate(wave.GetEnemyPrefab(), wave.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);
            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = StartingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            WaveConfig currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
}
