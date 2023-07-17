using System;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static Action OnMainBaseDestroy;
    public static UnityEvent<MapUnit> OnBuildingDestroy = new UnityEvent<MapUnit>();
    public static UnityEvent<Level._states> OnVisualModeChanged = new UnityEvent<Level._states>();
    public static Action OnResourceValueChanged;
    public static UnityEvent<Building> OnBuyButtonClick = new UnityEvent<Building>();
    public static Action<Cost> OnEnemyDied;
    public static Action OnLastWave;

    public static void SendBuildingDestroy(MapUnit building)
    {
        OnBuildingDestroy?.Invoke(building);
    }

    public static void ChangeVisualMode(Level._states state)
    {
        OnVisualModeChanged?.Invoke(state);
    }

    public static void ResourceValueChanged()
    {
        OnResourceValueChanged?.Invoke();
    }

    public static void BuyButtonClicked(Building building)
    {
        OnBuyButtonClick?.Invoke(building);
    }

    public static void SendEnemyDied(Cost cost)
    {
        OnEnemyDied?.Invoke(cost);
    }

    public static void SendBaseDestroyed()
    {
        OnMainBaseDestroy?.Invoke();
    }

    public static void SendLastWave()
    {
        OnLastWave?.Invoke();
        print(1);
    }
}
