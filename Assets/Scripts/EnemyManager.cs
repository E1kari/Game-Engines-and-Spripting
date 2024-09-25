using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnWave(WaveManager.Wave wave)
    {
        foreach (WaveManager.WaveElement waveElement in wave.wave)
        {
            GameObject enemy = waveElement.enemyType;
            for (int i = 0; i < waveElement.amount; i++)
            {
                enemies.Add(Instantiate(enemy));
            }
        }
    }
}
