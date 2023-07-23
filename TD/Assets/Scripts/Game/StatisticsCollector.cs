using UnityEngine;

public class StatisticsCollector : MonoBehaviour
{
    private int _totalEnemyCount = 0;
    private int _enemiesKilled = 0;
    private int _score = 0;
    private int _organicSpend = 0;
    private int _metalSpend = 0;
    private int _buildingsConstructed = 0;
    private int _buildingsDestroyed = 0;

    public int TotalEnemyCount {get { return _totalEnemyCount; }}
    public int EnemiesKilled {get { return _enemiesKilled; }}
    public int Score {get { return _score; }}
    public int OrganicSpend {get { return _organicSpend; }}
    public int MetalSpend {get { return _metalSpend; }}
    public int BuildingsConstructed {get { return _buildingsConstructed; }}
    public int BuildingsDestroyed {get { return _buildingsDestroyed; }}

    public void SetTotalEnemiesCount(int count)
    {
        _totalEnemyCount = count;
    }

    public void OnEnemyKilled(Cost cost)
    {
        _enemiesKilled++;
        _score += cost.score;
    }

    public void OnBuildingPlaced(Cost cost)
    {
        _buildingsConstructed++;
        _score += cost.score;
        _organicSpend += cost.organic;
        _metalSpend += cost.metal;
    }

    public void OnBuildingDestroyed(MapUnit building)
    {
        _buildingsDestroyed++;
        _score -= building.GetComponent<Building>().GetCost().score;
    }

    private void OnEnable()
    {
        GlobalEventManager.OnEnemyDied += OnEnemyKilled;
        GlobalEventManager.OnBuildingPlaced += OnBuildingPlaced;
        GlobalEventManager.OnBuildingDestroy += OnBuildingDestroyed;
        GlobalEventManager.OnTotalEnemiesAmountCalculated += SetTotalEnemiesCount;
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnEnemyDied -= OnEnemyKilled;
        GlobalEventManager.OnBuildingPlaced -= OnBuildingPlaced;
        GlobalEventManager.OnBuildingDestroy -= OnBuildingDestroyed;
        GlobalEventManager.OnTotalEnemiesAmountCalculated -= SetTotalEnemiesCount;
    }
}
