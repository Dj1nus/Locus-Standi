using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndChecker : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private StatisticsCollector _statistics;

    private int _totalEnemiesCount;

    private void OnEnable()
    {
        _spawner = FindObjectOfType<Spawner>();
        _statistics = FindObjectOfType<StatisticsCollector>();
        _spawner.OnLastWave += LastWave;
        GlobalEventManager.OnMainBaseDestroy += SetIsBaseDestroyed;
        GlobalEventManager.OnTotalEnemiesAmountCalculated += SetTotalEnemiesCount;
    }

    private void OnDestroy()
    {
        _spawner.OnLastWave -= LastWave;
        GlobalEventManager.OnEnemyDied -= CheckWinCondition;
        GlobalEventManager.OnMainBaseDestroy -= SetIsBaseDestroyed;
        GlobalEventManager.OnTotalEnemiesAmountCalculated -= SetTotalEnemiesCount;
    }

    private void SetTotalEnemiesCount(int count)
    {
        _totalEnemiesCount = count;
    }

    private void LastWave()
    {
        GlobalEventManager.OnEnemyDied += CheckWinCondition;
    }
    private void CheckWinCondition(Cost cost)
    {
        if (_statistics.EnemiesKilled >= _totalEnemiesCount)
        {
            EndLevel(isWin: true);
        }
    }

    private void SetIsBaseDestroyed()
    {
        EndLevel(isWin: false);
    }

    private void EndLevel(bool isWin)
    {
        if (isWin)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            Progress.Instance.OnLevelComplition(index);
        }

        GlobalEventManager.SendGameEnded(isWin);

        //print("GEM sended");
    }
}
