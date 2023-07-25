using System.Collections;
using UnityEngine;

public class GameEndChecker : MonoBehaviour
{
    private const float DELAY_BEFORE_END = 1.5f;

    private Spawner _spawner;
    private bool _isLastWave;
    private bool _isEnemiesLeft;
    private bool _isBaseDestroyed;
    private bool _isSignalSended = false;

    private void SetIsLastWave()
    {
        _isLastWave = true;
    }

    private void SetIsBaseDestroyed()
    {
        _isBaseDestroyed = true;
    }

    ///////////////////////////////////
    private void SetIsEnemiesLeft(Cost cost)
    {
        if (_isLastWave)
        {
            StopAllCoroutines();
            StartCoroutine(Kostyl());
        }

    }
    IEnumerator Kostyl()
    {
        yield return new WaitForSeconds(3f);

        if (FindObjectOfType<EnemyEntity>() == null)
        {
            print("NoEnemies");
            _isEnemiesLeft = true;
        }
    }
    ///////////////////////////////

    IEnumerator DelayBeforEnd(bool isWin)
    {
        yield return new WaitForSeconds(DELAY_BEFORE_END);

        GlobalEventManager.SendGameEnded(isWin);
    }

    private void OnEnable()
    {
        _spawner = FindObjectOfType<Spawner>();

        _spawner.OnLastWave += SetIsLastWave;
        GlobalEventManager.OnEnemyDied += SetIsEnemiesLeft;
        GlobalEventManager.OnMainBaseDestroy += SetIsBaseDestroyed;
    }

    private void OnDisable()
    {
        _spawner.OnLastWave -= SetIsLastWave;
        GlobalEventManager.OnEnemyDied -= SetIsEnemiesLeft;
        GlobalEventManager.OnMainBaseDestroy -= SetIsBaseDestroyed;
    }

    void Update()
    {
        if (_isLastWave && _isEnemiesLeft && !_isSignalSended)
        {
            _isSignalSended = true;
            StartCoroutine(DelayBeforEnd(true));
        }

        else if (_isBaseDestroyed && !_isSignalSended)
        {
            _isSignalSended = true;
            StartCoroutine(DelayBeforEnd(false));
        }

    }
}
