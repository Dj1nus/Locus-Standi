using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] private float _startDelay;
    [SerializeField] Waves[] _waves;

    private int _waveCount;
    private int _currentWaveIndex;

    private IEnumerator StartSpawn()
    {
        if (_startDelay != 0)
        {
            yield return new WaitForSeconds(_startDelay);
        }

        for (; _currentWaveIndex < _waveCount; _currentWaveIndex++) 
        {
            yield return StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        float delayBetweenSpawns = _waves[_currentWaveIndex].DelayBetweenSpawns;

        Entity[] enemiesArray = _waves[_currentWaveIndex].Wave.GetEnemy();
        int[] numbersOfEnemies = _waves[_currentWaveIndex].Wave.GetCount();

        for (int i = 0; i < _waves[_currentWaveIndex].Wave.GetLenght(); i++)
        {
            for (int j = 0; j < numbersOfEnemies[i]; j++)
            {
                var newEnemy = Instantiate(enemiesArray[i], 
                    _spawnPoints[Random.Range(0, _spawnPoints.Length)]);

                newEnemy.gameObject.GetComponent<TargetSelector>().Init();
                yield return new WaitForSeconds(delayBetweenSpawns);
            }
        }

        yield return new WaitForSeconds(_waves[_currentWaveIndex].WaveDuration);
    }

    void Start()
    {
        _waveCount = _waves.Length;
        _currentWaveIndex = 0;

        StartCoroutine(StartSpawn());
    }
}

[System.Serializable]

public class Waves
{
    [SerializeField] private float _waveDuration;
    [SerializeField] private float _delayBetweenSpawns;
    [SerializeField] private bool _isTimeFixed;
    [SerializeField] private EnemyDictionary _wavesDictionary;

    public EnemyDictionary Wave {get { return _wavesDictionary; } }
    public float WaveDuration { get { return _waveDuration; } }
    public float DelayBetweenSpawns { get { return _delayBetweenSpawns; } }
}

[System.Serializable]

public class EnemyDictionary
{
    [SerializeField] private Entity[] _enemy;
    [SerializeField] private int[] _count;

    public Entity[] GetEnemy()
    {
        return _enemy;
    }

    public int[] GetCount()
    {
        return _count;
    }

    public int GetLenght()
    {
        return _count.Length;
    }

    private void Init()
    {
        if(_enemy.Length != _count.Length)
        {
            Debug.LogError("Массивы имеют разное количество элементов!");
        }
    }
}
