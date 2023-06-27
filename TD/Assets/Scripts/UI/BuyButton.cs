using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] Building _building;
    [SerializeField] PlayersResources _playerResources;

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

    void Start()
    {
        GlobalEventManager.OnResourceValueChanged.AddListener(CompareCost);   
        _button = GetComponent<Button>();

        CompareCost();
    }
}
