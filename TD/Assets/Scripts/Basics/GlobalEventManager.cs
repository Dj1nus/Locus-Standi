using System;

public class GlobalEventManager
{
    public static Action OnMainBaseDestroy;
    public static Action<Cost> OnBuildingPlaced;
    public static Action<MapUnit> OnBuildingDestroy;
    public static Action OnResourceValueChanged;
    public static Action<Cost> OnEnemyDied;
    public static Action OnLastWave;
    public static Action<bool> OnGameEnd;
    public static Action<int> OnTotalEnemiesAmountCalculated;
    public static Action OnPointerUp;
    public static Action<MapVisual.states> OnVisualModeChanged;
    public static Action<Building> OnBuyButtonClick;

    public static void SendOnPointerUp()
    {
        OnPointerUp?.Invoke();
    }

    public static void SendBuildingDestroy(MapUnit building)
    {
        OnBuildingDestroy?.Invoke(building);
    }

    public static void ChangeVisualMode(MapVisual.states state)
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
    }

    public static void SendGameEnded(bool isWin)
    {
        OnGameEnd?.Invoke(isWin);
    }

    public static void SendBuildingPlaced(Cost cost)
    {
        OnBuildingPlaced?.Invoke(cost);
    }

    public static void SendTotalEnemiesAmountCalculated(int amount)
    {
        OnTotalEnemiesAmountCalculated?.Invoke(amount);
    }
}
