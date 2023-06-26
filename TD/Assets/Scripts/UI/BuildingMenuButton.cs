using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class BuildingMenuButton : MonoBehaviour
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

    private Button _button;

    public void SetButtonClickable(bool isClickable)
    {
        _button.interactable = isClickable;
    }

    public void ChangeBuildingButtonPosition(float time)
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

        _button = GetComponent<Button>();
    }
}
