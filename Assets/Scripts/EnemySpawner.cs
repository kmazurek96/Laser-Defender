using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingwave = 0;
    [SerializeField] bool looping = false;
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingwave; waveIndex < waveConfigs.Count;waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig WaveConfig)
    {
        for (int i = 1; i <= WaveConfig.GetnumberOfEnemies(); i++)
        {
            var newEnemy =Instantiate(WaveConfig.GetenemyPrefab(), WaveConfig.GetWavepoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(WaveConfig);
            yield return new WaitForSeconds(WaveConfig.GettimeBetweenSpawns());
            
            
        }
    }


}
