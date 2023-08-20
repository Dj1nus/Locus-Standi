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

        State = States.Move;
    }

    IEnumerator DelayBetweenSpawns()
    {
        yield return new WaitForSeconds(Random.Range(_delayBetweenSpawn - 3f, _delayBetweenSpawn + 5f));
        State = States.Spawn;
    }

    public override void StateMachine()
    {
        IsCanAttack();

        switch (State)
        {
            case States.Move:
                TargetSelector.ChooseTarget();
                break;

            case States.Spawn:
                TargetSelector.StopMoving();
                SpawnEnemies();
                break;

            case States.Attack:
                if (Target != null)
                {
                    EntityComponent.Attack(Target);
                }
                TargetSelector.StopMoving();
                break;
        }
    }

    private void FixedUpdate()
    {
        StateMachine();
    }
}

