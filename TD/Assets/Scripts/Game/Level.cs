using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public enum _states
    {
        building,
        defending
    }

    private _states _state;

    [SerializeField] private Vector2 _mapSize;
    

    public _states GetState()
    {
        return _state;
    }

    public void ChangeState()
    {
        if (_state == _states.building) 
            _state = _states.defending;
        else
            _state = _states.building;

        GlobalEventManager.ChangeVisualMode(_state);
    }
    private void StopLevel()
    {
        //Time.timeScale = 0;
    }

    private void OnEnable()
    {
        GlobalEventManager.OnMainBaseDestroy += StopLevel;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnMainBaseDestroy -= StopLevel;
    }

    private void Start()
    {
        _state = _states.defending;
    }
}
