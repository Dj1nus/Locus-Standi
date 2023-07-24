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

    public void Init()
    {
        _state = _states.idle;

        _targetSelector = GetComponent<TurretTargetSelector>();
        _shooter = GetComponent<TurretShooter>();

        _targetSelector.OnEnemyDetected += EnemyDetected;
    }
  
    private void OnDestroy()
    {
        _targetSelector.OnEnemyDetected -= EnemyDetected;
    }

    private void EnemyDetected(EnemyEntity enemy)
    {
        enemy.EnemyDied += TargetDied;

        _target = enemy;
        _state = _states.attack;
    }

    private void TargetDied(EnemyEntity target)
    {
        target.EnemyDied -= TargetDied;
        _target = null;
        _targetSelector.SetTargetToNull();
        _state = _states.idle;
    }

    private void StateMachine()
    {
        switch (_state)
        {
            case _states.idle:
                break;

            case _states.attack:
                _shooter.ShooterStateMachine(_target);
                break;
        }
    }

    private void Update() 
    {
        StateMachine();
    }
}
