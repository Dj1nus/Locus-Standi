using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersResources : MonoBehaviour
{
    [SerializeField] private int _startMetalAmount;
    [SerializeField] private int _startOrganicAmount;

    private int _metal = 1;
    public int metal { get { return _metal; } }
    private int _organic = 1;
    public int organic { get { return _organic;} }

    public void IncreaseMetalValue(int metal)
    {
        _metal += metal;
        GlobalEventManager.ResourceValueChanged();
    }

    public void DecreaseMetalValue(int metal)
    {
        _metal -= metal;
        GlobalEventManager.ResourceValueChanged();
    }

    public void IncreaseOrganicValue(int organic) 
    { 
        _organic += organic;
        GlobalEventManager.ResourceValueChanged();
    }

    public void DecreaseOrganicValue(int organic)
    {
        _organic -= organic;
        GlobalEventManager.ResourceValueChanged();
    }

    private void TakeMoneyForEnemyKilling(Cost cost)
    {
        IncreaseMetalValue(cost.metal);
        IncreaseOrganicValue(cost.organic);
    }

    private void Start()
    {
        GlobalEventManager.OnEnemyDied += TakeMoneyForEnemyKilling;

        IncreaseMetalValue(_startMetalAmount);
        IncreaseOrganicValue(_startOrganicAmount);
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnEnemyDied -= TakeMoneyForEnemyKilling;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            IncreaseMetalValue(100);
            IncreaseOrganicValue(100);
        }
    }
}
