using UnityEngine;

public class MapVisual : MonoBehaviour
{
    public enum _states
    {
        starting,
        building,
        defending
    }

    private _states _state;

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

    private void Start()
    {
        _state = _states.defending;
    }
}
