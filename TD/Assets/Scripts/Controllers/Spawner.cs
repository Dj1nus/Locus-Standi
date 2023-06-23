using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private enum _states
    {
        Start,
        Spawn,
        Wait
    }
    private _states _state = _states.Start;

    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();
    [SerializeField] private int _numberOfWaves;
    [SerializeField] private float _delayBetweenWaves;
    [SerializeField] private float _startDelay;

    [SerializeField] private int[] _numberOfEnemies;
    [SerializeField] private TargetSelector _tierOneEnemy;
    [SerializeField] private TargetSelector _tierTwoEnemy;
    [SerializeField] private TargetSelector _tierThreeEnemy;

    

    [SerializeField] private int[,] a;

    private int _wave = 0;
    private bool _isBetweenWavesDelayTimerStopped = true;

    IEnumerator BetweenWavesDelayTimer(float time)
    {
        _isBetweenWavesDelayTimerStopped = false;
        yield return new WaitForSeconds(time);
        _state = _states.Spawn;
        _isBetweenWavesDelayTimerStopped = true;
    }

    IEnumerator StartDelay(float time)
    {
        yield return new WaitForSeconds(time);
        _state = _states.Spawn;
    }

    void Start()
    {
        StartCoroutine(StartDelay(_startDelay));
    }

    void Update()
    {
        switch (_state)
        {
            case _states.Start: break;

            case _states.Spawn:
                foreach (GameObject point in _spawnPoints)
                {
                    for (int i = 0; i < _numberOfEnemies[_wave] / _spawnPoints.Count; i++) 
                        Instantiate(_tierOneEnemy, point.transform.position, Quaternion.identity).Init();   
                }
                _state = _states.Wait;
                _wave++;
                break;

            case _states.Wait:
                if (_isBetweenWavesDelayTimerStopped)
                {
                    StartCoroutine(BetweenWavesDelayTimer(_delayBetweenWaves));
                }
                break;

            default: _state = _states.Wait; break;
        }
    }
}
