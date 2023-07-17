using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndChecker : MonoBehaviour
{
    private Spawner _spawner;
    private bool _isLastWave;
    private bool _isEnemiesLeft;
    private bool _isBaseDestroyed;

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
        StartCoroutine(Kostyl());
    }
    IEnumerator Kostyl()
    {
        yield return new WaitForEndOfFrame();

        if (FindObjectOfType<EnemyEntity>() == null)
        {
            _isEnemiesLeft = true;
        }
    }
    ///////////////////////////////

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
        if (_isLastWave && _isEnemiesLeft)
        {
            print("you won");
        }

        else if (_isBaseDestroyed)
        {
            print("you Loose");
        }
        
    }
}
