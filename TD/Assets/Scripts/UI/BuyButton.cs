using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
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
        _playerResources.DecreaseMetalValue(_buildingCost.metal);
        _playerResources.DecreaseOrganicValue(_buildingCost.organic);

        GlobalEventManager.BuyButtonClicked(_building);
    }

    void Start()
    {
        GlobalEventManager.OnResourceValueChanged.AddListener(CompareCost);   
        _button = GetComponent<Button>();

        CompareCost();
    }
}
