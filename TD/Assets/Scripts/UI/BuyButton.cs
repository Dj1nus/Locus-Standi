using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 bigSize = new Vector2(150, 150);
    private Vector2 smallSize = new Vector2(140, 140);

    [SerializeField] public Building _building;
    [SerializeField] private BuildingCostPanel _panel;
    //[SerializeField] private RectTransform _image;

    PlayersResources _playerResources;
    private Cost _buildingCost;
    private Button _button;
    private RectTransform _rectTransform;

    private AudioPlayer _audioPlayer;

    private void CompareCost()
    {
        _buildingCost = _building.GetCost();

        if (_playerResources.metal >= _buildingCost.metal && _playerResources.organic >= _buildingCost.organic)
        {
            _button.interactable = true;

            _panel.ChangeMetalColor(true);
            _panel.ChangeOrganicColor(true);
        }

        else
        {
            _button.interactable = false;

            if (_playerResources.metal < _buildingCost.metal)
            {
                _panel.ChangeMetalColor(false);
            }

            else
            {
                _panel.ChangeMetalColor(true);
            }

            if (_playerResources.organic < _buildingCost.organic)
            {
                _panel.ChangeOrganicColor(false);
            }

            else
            {
                _panel.ChangeOrganicColor(true);
            }
        }
    }

    public void OnClick()
    {
        //if (_audioPlayer != null)
        //{
        //    _audioPlayer.Play("Buy", Random.Range(0.9f, 1.1f));
        //}

        //GlobalEventManager.BuyButtonClicked(_building);
        
    }

    public virtual void Init()
    {
        GlobalEventManager.OnResourceValueChanged += CompareCost;
        _button = GetComponent<Button>();

        _playerResources = FindObjectOfType<PlayersResources>();

        CompareCost();

        _panel.SetCostText(_buildingCost);

        _audioPlayer = GetComponentInChildren<AudioPlayer>();
        _rectTransform = GetComponent<RectTransform>();
    }

    //void Start()
    //{
    //    Init();
    //}

    private void OnDisable()
    {
        GlobalEventManager.OnResourceValueChanged -= CompareCost;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        _rectTransform.sizeDelta = bigSize;
        //_image.sizeDelta = bigSize;

        if (_audioPlayer != null)
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.sizeDelta = smallSize;
        //_image.sizeDelta = smallSize;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.Play(SoundTypes.Buy, Random.Range(0.9f, 1.1f));
            }

            GlobalEventManager.BuyButtonClicked(_building);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GlobalEventManager.SendOnPointerUp();
    }
}
