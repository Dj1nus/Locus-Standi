using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private enum _states
    {
        Start,
        Spawn,
        Wait
    }

    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();
    [SerializeField] private int _numberOfWaves;
    [SerializeField] private int[] _numberOfEnemies;
    [SerializeField] private float _delayBetweenWaves;
    [SerializeField] private float _startDelay;
    [SerializeField] private BaseEnemyLogic _ram;

    private _states _state = _states.Start;
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
                        Instantiate(_ram, point.transform.position, Quaternion.identity).Init();   
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

        if (_wave == _numberOfWaves - 1)
        {
            Application.Quit();
        }
    }

}
