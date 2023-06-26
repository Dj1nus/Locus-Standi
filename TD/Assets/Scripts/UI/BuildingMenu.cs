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
    [SerializeField] private Button _button;
    [SerializeField] private BuildingMenuPanel _panel;

    public float GetTimeValue()
    {
        return _time;
    }

    IEnumerator BuildingMenuTimer(_states nextState)
    {

        _panel.ChangeBuildingMenuPosition();
        _button.enabled = false;
        _state = _states.moving;
        yield return new WaitForSeconds(_time);
        _state = nextState;
        _button.enabled = true;
    }

    public void ChangeMenuPosition()
    {
        if (_state == _states.down) 
        {
            Debug.Log(2);
            StartCoroutine(BuildingMenuTimer(_states.up));
        }

        else
        {
            Debug.Log(3);
            StartCoroutine(BuildingMenuTimer(_states.down));
        }
    }

    void Start()
    {
        _state = _states.down;
    }
}
