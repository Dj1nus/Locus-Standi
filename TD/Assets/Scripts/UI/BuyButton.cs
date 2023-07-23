using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] protected Building _building;
    [SerializeField] private BuildingCostPanel _panel;

    PlayersResources _playerResources;
    private Cost _buildingCost;
    private Button _button;

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
        GlobalEventManager.BuyButtonClicked(_building);
    }

    public virtual void Init()
    {
        GlobalEventManager.OnResourceValueChanged += CompareCost;
        _button = GetComponent<Button>();

        _playerResources = FindObjectOfType<PlayersResources>();

        CompareCost();

        _panel.SetCostText(_buildingCost);
    }

    void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        GlobalEventManager.OnResourceValueChanged -= CompareCost;
    }
}
