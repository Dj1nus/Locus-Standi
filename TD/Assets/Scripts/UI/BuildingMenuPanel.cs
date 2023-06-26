using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class BuildingMenuPanel : MonoBehaviour
{
    private enum _states
    {
        up,
        down
    }

    private _states _state;

    [SerializeField] private int _upPositionY;
    [SerializeField] private int _downPositionY;
    private RectTransform _transform;

    public void ChangeBuildingMenuPosition(float time)
    {
        if (_state == _states.down)
        {
            
            _transform.DOLocalMoveY(_upPositionY, time);
            _state = _states.up;
        }

        else
        {
            _transform.DOLocalMoveY(_downPositionY, time);
            _state = _states.down;
        }
    }

    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _state = _states.down;
    }
}
