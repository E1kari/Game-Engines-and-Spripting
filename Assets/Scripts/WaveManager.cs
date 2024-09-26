using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    public Wave[] allWaves_;

    public TextMeshProUGUI uiWaveCount_;

    public int secondsToStartNewWave_;

    public EnemyManager enemyManager_;

    private bool waiting_;
    private int waveNumber;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 0;
        uiWaveCount_.text = waveNumber.ToString() + "/" + allWaves_.Length.ToString();
        StartCoroutine(NextWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting_)
        {
            if (enemyManager_.allEnemiesDead())
            {
                StartCoroutine(NextWave());
            }
        }
    }

    private IEnumerator NextWave()
    {
        waiting_ = true;
        waveNumber++;
        if (waveNumber - 1 < allWaves_.Length)
        {
            yield return new WaitForSeconds(secondsToStartNewWave_);
            enemyManager_.SpawnWave(allWaves_[waveNumber - 1]);
        }
        else
        {
            SceneManager.LoadScene("Win Screen");
        }
        uiWaveCount_.text = waveNumber.ToString() + "/" + allWaves_.Length.ToString();
        waiting_ = false;
    }
}
