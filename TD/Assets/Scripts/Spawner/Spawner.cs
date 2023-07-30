using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Action OnLastWave;

    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] private float _startDelay;
    [SerializeField] Waves[] _waves;
    [SerializeField] private SkipPrepairing _skipPrepairing;

    private int _waveCount;
    private int _currentWaveIndex;
    public int totalEnemyCount;

    private void StartRoutine()
    {
        StartCoroutine(StartSpawn());
    }

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

        EnemyEntity[] enemiesArray = _waves[_currentWaveIndex].Wave.GetEnemy();
        int[] numbersOfEnemies = _waves[_currentWaveIndex].Wave.GetCount();

        for (int i = 0; i < _waves[_currentWaveIndex].Wave.GetLenght(); i++)
        {
            for (int j = 0; j < numbersOfEnemies[i]; j++)
            {
                var newEnemy = Instantiate(enemiesArray[i], 
                    _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)]);

                newEnemy.transform.position += new Vector3(UnityEngine.Random.Range(-20f, 20f), 0, UnityEngine.Random.Range(-20f, 20f));

                newEnemy.gameObject.GetComponent<EnemyTargetSelector>().Init();
                newEnemy.GetComponent<BaseEnemyStateMachine>().Init();
                yield return new WaitForSeconds(delayBetweenSpawns);
            }
        }

        if (_currentWaveIndex == _waveCount - 1)
        {
            OnLastWave?.Invoke();
        }

        yield return new WaitForSeconds(_waves[_currentWaveIndex].WaveDuration);
    }

    private int CalculateTotalEnemyCount()
    {
        int value = 0;
        int[] tmpArray;

        foreach (var wave in _waves)
        {
            tmpArray = wave.Wave.GetCount();

            for (int i = 0; i < tmpArray.Length;i++)
            {
                value += tmpArray[i];
            }
        }

        return value;
    }

    void Start()
    {
        _waveCount = _waves.Length;
        _currentWaveIndex = 0;

        _skipPrepairing.OnStartSpawning += StartRoutine;

        GlobalEventManager.SendTotalEnemiesAmountCalculated(CalculateTotalEnemyCount());
    }

    private void OnDisable()
    {
        _skipPrepairing.OnStartSpawning -= StartRoutine;
    }

    private void Update()
    {
        //print(_currentWaveIndex);
    }
}

[System.Serializable]

public class Waves
{
    [SerializeField] private float _waveDuration;
    [SerializeField] private float _delayBetweenSpawns;
    [SerializeField] public bool _isTimeFixed;
    [SerializeField] private EnemyDictionary _wavesDictionary;

    public EnemyDictionary Wave { get { return _wavesDictionary; } }
    public float WaveDuration { get { return _waveDuration; } }
    public float DelayBetweenSpawns { get { return _delayBetweenSpawns; } }
}

[System.Serializable]

public class EnemyDictionary
{
    [SerializeField] private EnemyEntity[] _enemy;
    [SerializeField] private int[] _count;

    public EnemyEntity[] GetEnemy()
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
