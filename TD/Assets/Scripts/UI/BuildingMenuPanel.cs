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

    private _states _state = _states.down;

    [SerializeField] private int _upPositionY;
    [SerializeField] private int _downPositionY;
    [SerializeField] private float _time;
    [SerializeField] private GameObject _button;
    private RectTransform _transform;

    public void ChangeBuildingMenuPosition()
    {
        if (_state == _states.down)
        {
            _transform.DOLocalMoveY(_upPositionY, _time);
            _state = _states.up;
        }

        else
        {
            _transform.DOLocalMoveY(_downPositionY, _time);
            _state = _states.down;
        }
    }

    void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
