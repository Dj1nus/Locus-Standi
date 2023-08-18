using UnityEngine;
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

    [SerializeField] private BuyButton[] _buttons;

    private RectTransform _transform;

    private void InitButtons()
    {
        int numOfTurrets = Progress.Instance.ChoosedBuildings.Length;

        if (numOfTurrets < _buttons.Length)
        {
            for (int i = numOfTurrets; i < _buttons.Length; i++)
            {
                Destroy(_buttons[i].gameObject);
            }
        }

        for (int i = 0; i < numOfTurrets; i++)
        {
            _buttons[i]._building = Progress.Instance.ChoosedBuildings[i];
            _buttons[i].Init();
        }
    }

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
        InitButtons();

        FindObjectOfType<PlayersResources>().Init();

        _transform = GetComponent<RectTransform>();
        _state = _states.down;
    }
}
