using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        

        if (!_isBaseDestroyed)
        {
            print("Destroyed");
            _isBaseDestroyed = true;
        }
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
            _isEnemiesLeft = true;
        }
    }
    ///////////////////////////////

    IEnumerator DelayBeforEnd(bool isWin)
    {


        if (isWin)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            Progress.Instance.OnLevelComplition(index);
        }
       
        yield return new WaitForSeconds(DELAY_BEFORE_END);

        GlobalEventManager.SendGameEnded(isWin);

        print("GEM sended");
    }

    private void LevelEnded(bool isWin)
    {
        if (isWin)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            Progress.Instance.OnLevelComplition(index);
        }

        GlobalEventManager.SendGameEnded(isWin);

        print("GEM sended");
    }

    private void OnEnable()
    {
        _spawner = FindObjectOfType<Spawner>();

        _spawner.OnLastWave += SetIsLastWave;
        GlobalEventManager.OnEnemyDied += SetIsEnemiesLeft;
        GlobalEventManager.OnMainBaseDestroy += SetIsBaseDestroyed;
    }

    private void OnDestroy()
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
            //StartCoroutine(DelayBeforEnd(true));
            LevelEnded(true);
        }

        else if (_isBaseDestroyed && !_isSignalSended)
        {
            print("Send");

            _isSignalSended = true;
            //StartCoroutine(DelayBeforEnd(false));
            LevelEnded(false);
        }


        if (Input.GetKey(KeyCode.L))
        {
            print(_isSignalSended);
            print(_isBaseDestroyed);
        }
    }
}
