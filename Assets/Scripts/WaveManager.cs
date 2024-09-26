using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct WaveElement
    {
        public GameObject enemyType;
        public int amount;
    }

    [System.Serializable]
    public struct Wave
    {
        public WaveElement[] wave;
    }

    public Wave[] allWaves;

    public SpawnPoint[] SpawnPoints;
    public EnemyManager enemyManager;
    private int waveNumber;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 0;
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NextWave()
    {
        waveNumber++;
        if (waveNumber - 1 < allWaves.Length)
        {
            enemyManager.SpawnWave(allWaves[waveNumber - 1]);
        }
    }
}
