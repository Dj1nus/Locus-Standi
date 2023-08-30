using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BuildingMenuButton : MonoBehaviour, IPointerEnterHandler
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
    private AudioPlayer _audioPlayer;

    public void SetButtonClickable(bool isClickable)
    {
        _button.interactable = isClickable;
    }

    void Start()
    {
        _audioPlayer = GetComponentInChildren<AudioPlayer>();

        _transform = GetComponent<RectTransform>();
        _state = _states.down;

        _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            _audioPlayer.Play(SoundTypes.Active, Random.Range(0.9f, 1.1f));
        }

        else
        {
            _audioPlayer.Play(SoundTypes.UnActive, Random.Range(0.9f, 1.1f));
        }

    }

    public void OnPointerClick()
    {
        if (_state == _states.down)
        {
            _audioPlayer.Play(SoundTypes.Buy, Random.Range(1f, 1.2f));

            _transform.DOLocalMoveY(_upPositionY, 0.5f);

            _state = _states.up;
        }
        else
        {
            _audioPlayer.Play(SoundTypes.Buy, Random.Range(0.85f, 0.95f));

            _transform.DOLocalMoveY(_downPositionY, 0.5f);

            _state = _states.down;
        }
    }
}
