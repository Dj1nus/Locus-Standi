using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    private enum _states
    {
        up,
        moving,
        down
    }

    private _states _state;

    [SerializeField] private float _time;
    [SerializeField] private BuildingMenuButton _button;
    [SerializeField] private BuildingMenuPanel _panel;

    public float GetTimeValue()
    {
        return _time;
    }

    IEnumerator BuildingMenuTimer(_states nextState)
    {
        _button.ChangeBuildingButtonPosition(_time);
        _panel.ChangeBuildingMenuPosition(_time);
        //_button.enabled = false;
        _button.SetButtonClickable(false);
        _state = _states.moving;
        yield return new WaitForSeconds(_time);
        _state = nextState;
        _button.SetButtonClickable(true);
        //_button.enabled = true;
    }

    public void ChangeMenuPosition()
    {
        if (_state == _states.down) 
            StartCoroutine(BuildingMenuTimer(_states.up));
        
        else
            StartCoroutine(BuildingMenuTimer(_states.down));
    }

    void Start()
    {
        _state = _states.down;
    }
}
