using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;

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

    public TextMeshProUGUI uiWaveCount_;

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
        if (enemyManager.allEnemiesDead())
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        waveNumber++;
        print(waveNumber);
        if (waveNumber - 1 < allWaves.Length)
        {
            enemyManager.SpawnWave(allWaves[waveNumber - 1]);
        }
        //uiWaveCount_.text = ()
    }
}
