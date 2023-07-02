using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurretStateMachine : MonoBehaviour
{
    private enum _states
    {
        idle,
        attack
    }
    private _states _state;

    private Entity _target;

    private TurretTargetSelector _targetSelector;
    private TurretShooter _shooter;
    private Entity _entity;
    private Quaternion _startRotation;

    public void Init()
    {
        _state = _states.idle;
        _startRotation = gameObject.transform.rotation;

        _targetSelector = GetComponent<TurretTargetSelector>();
        _shooter = GetComponent<TurretShooter>();
        _entity = GetComponent<Entity>();

        _targetSelector.OnEnemyDetected.AddListener(EnemyDetected);
    }

    private void EnemyDetected(Entity enemy)
    {
        //Debug.Log("я увидел цель");

        enemy.iAmDied.AddListener(TargetDied);

        _target = enemy;
        _state = _states.attack;
    }

    private void TargetDied(Entity target)
    {
        target.iAmDied.RemoveListener(TargetDied);
        _target = null;
        _targetSelector.SetTargetToNull();
        _state = _states.idle;
    }

    private void StateMachine()
    {
        switch (_state)
        {
            case _states.idle:
                //Debug.Log("я отдыхаю");
                //gameObject.transform.rotation = _startRotation;
                break;

            case _states.attack:
                //Debug.Log("я стрел€ю");
                _shooter.Shoot(_target);
                break;
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update() 
    {
        StateMachine();
    }
}
