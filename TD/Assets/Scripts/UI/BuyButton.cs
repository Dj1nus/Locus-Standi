using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] protected Building _building;

    PlayersResources _playerResources;
    private Cost _buildingCost;
    private Button _button;

    private void CompareCost()
    {
        _buildingCost = _building.GetCost();

        if (_playerResources.metal >= _buildingCost.metal && _playerResources.organic >= _buildingCost.organic)
        {
            _button.interactable = true;
        }

        else
        {
            _button.interactable = false;
        }
    }

    public void OnClick()
    {
        GlobalEventManager.BuyButtonClicked(_building);
    }

    public virtual void Init()
    {
        GlobalEventManager.OnResourceValueChanged += CompareCost;
        _button = GetComponent<Button>();

        _playerResources = FindObjectOfType<PlayersResources>();

        CompareCost();
    }

    void Start()
    {
        Init();
    }
}
