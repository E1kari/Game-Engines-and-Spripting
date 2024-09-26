using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemies_;

    public SpawnPoint[] spawnPoints_;

    // Start is called before the first frame update
    void Start()
    {
        enemies_ = new List<GameObject> { };
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies_.Count; i++)
        {
            if (enemies_[i].GetComponent<EnemyController>().health <= 0)
            {
                enemies_[i].GetComponent<EnemyController>().die();
                enemies_.RemoveAt(i);
            }
        }
    }

    public void SpawnWave(WaveManager.Wave wave)
    {
        foreach (WaveManager.WaveElement waveElement in wave.wave)
        {
            GameObject enemy = waveElement.enemyType;
            for (int i = 0; i < waveElement.amount; i++)
            {
                int rnd = Random.Range(0, spawnPoints_.Length);
                //print(rnd);
                enemies_.Add(Instantiate(enemy, spawnPoints_[rnd].hit.point, transform.rotation));
            }
        }
    }

    public bool allEnemiesDead()
    {
        //print(enemies_.Count);
        if (enemies_.Count == 0)
        {
            return true;
        }
        return false;
    }
}
