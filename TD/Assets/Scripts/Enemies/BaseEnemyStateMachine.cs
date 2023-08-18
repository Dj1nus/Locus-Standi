using UnityEngine;

public class BaseEnemyStateMachine : MonoBehaviour
{
    protected enum _states
    {
        Move,
        MoveToTurret,
        Spawn,
        Attack
    }
    protected _states _state;

    protected EnemyTargetSelector _targetSelector;
    protected Entity _entity;

    protected Entity _target;

    public virtual void Init()
    {
        _state = _states.Move;
        _targetSelector = GetComponent<EnemyTargetSelector>();
        _entity = GetComponent<Entity>();

        _targetSelector.OnBuildingDetected += OnTurretDetected;
    }

    private void OnDestroy()
    {
        _targetSelector.OnBuildingDetected -= OnTurretDetected;
    }

    public void OnTurretDetected(Entity target)
    {
        if (target is TurretEntity)
        {
            TurretEntity t = target as TurretEntity;
            t.OnDestroy += OnTurretDestroy;
        }

        _target = target;

        _state = _states.MoveToTurret;
    }

    private void OnTurretDestroy(Entity target)
    {
        if (target is TurretEntity)
        {
            TurretEntity t = _target as TurretEntity;
            t.OnDestroy -= OnTurretDestroy;
        }

        _target = _targetSelector.GetMainBase();

        _targetSelector.SetTargetToNull(target);

        _state = _states.Move;
    }

    protected virtual void IsCanAttack()
    {
        if (_target != null && Vector3.Distance(transform.position, _target.transform.position) <= _target.DistanceToDamage)
        {
            _state = _states.Attack;
        }
    }

    public void OnEnemyDied()
    {
        _state = _states.Move;
    }

    public virtual void StateMachine()
    {
        IsCanAttack();

        switch (_state)
        {
            case _states.Move:
                _targetSelector.ChooseTarget();
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
}
