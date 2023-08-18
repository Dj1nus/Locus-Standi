using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerStateMachine : BaseEnemyStateMachine
{
    [SerializeField] EnemyDictionary _enemiesMenu;
    [SerializeField] float _delayBetweenSpawn;
    [SerializeField] float _breakOfMovement;

    private EnemyEntity _enemyToSpawn;
    private int _count;
    private int _minCountToSpawn;
    private Vector3 _position;
    public override void Init()
    {
        base.Init();

        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        int index = Random.Range(0, _enemiesMenu.GetLenght() - 1);
        _enemyToSpawn = _enemiesMenu.GetEnemy()[index];
        _count = _enemiesMenu.GetCount()[index];

        if (_count > 2)
        {
            _minCountToSpawn = 3;
        }

        else
        {
            _minCountToSpawn = 1;
        }

        for (int i = 0; i < Random.Range(_minCountToSpawn, _count); i++)
        {
            _position = new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f),
                transform.position.y,
                transform.position.z + Random.Range(-1.5f, 1.5f));

            var newEnemy = Instantiate(_enemyToSpawn, _position, Quaternion.identity);

            newEnemy.gameObject.GetComponent<EnemyTargetSelector>().Init();
            newEnemy.GetComponent<BaseEnemyStateMachine>().Init();
        }

        StartCoroutine(DelayBetweenSpawns());

        _state = _states.Move;
    }

    IEnumerator DelayBetweenSpawns()
    {
        yield return new WaitForSeconds(Random.Range(_delayBetweenSpawn - 3f, _delayBetweenSpawn + 5f));
        _state = _states.Spawn;
    }

    public override void StateMachine()
    {
        IsCanAttack();

        switch (_state)
        {
            case _states.Move:
                _targetSelector.ChooseTarget();
                break;

            case _states.Spawn:
                _targetSelector.StopMoving();
                SpawnEnemies();
                break;

            case _states.Attack:
                if (_target != null)
                {
                    _entity.Attack(_target);
                }
                _targetSelector.StopMoving();
                break;
        }
    }

    private void FixedUpdate()
    {
        StateMachine();
    }
}

