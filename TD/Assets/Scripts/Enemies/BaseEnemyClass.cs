using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyClass : Entity
{
    private enum _states
    {
        Move,
        Attack
    }

    [SerializeField] private Entity _mainBase;
    private Entity _target;
    protected NavMeshAgent _agent;
    protected List<Entity> _targets = new List<Entity>();

    private _states _state;

    public void Init()
    {
        _state = _states.Move;
        _agent = GetComponent<NavMeshAgent>();
        _target = _mainBase;
        _agent.destination = _target.transform.position;
    }

    private void SetTarget(Entity target)
    {
        //print("Changing target");
        _target = target;
        _agent.destination = _target.transform.position;
    }

    private void ChooseTarget()
    {
        //print("Choosing target");

        if (_targets.Count <= 0)
        {
            return;
        }

        float distanceToBase = Vector3.Distance(transform.position, _mainBase.transform.position);
        float minDistance = distanceToBase;

        Entity possibleTarget = _mainBase;

        foreach (Entity target in _targets)
        {
            if (target != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (distanceToTarget < distanceToBase & distanceToTarget <= minDistance)
                {
                    possibleTarget = target;
                    minDistance = distanceToTarget;
                }
            }
        }
        SetTarget(possibleTarget);
    }

    private void CheckState()
    {
        if (_target == _mainBase & Vector3.Distance(transform.position, _target.transform.position) <= 3.5f)
        {
            print("Base");
            _state = _states.Attack;
            return;
        }
        if (Vector3.Distance(transform.position, _target.transform.position) <= 2f)
        {
            _state = _states.Attack;
            return;
        }
        _state = _states.Move;
    }

    private void CheckTargetExist()
    {
        if (_target == null)
        {
            _targets.Remove(_target);
            SetTarget(_mainBase);
        }
    }

    private void Attack()
    {
        //print(_target);
        _target.GetDamage(_damage);
    }

    public void StateMachine()
    {
        //print(_state);
        CheckHp();
        CheckTargetExist();
        CheckState();
        switch (_state)
        {
            case _states.Move:
                if (_targets.Count > 0)
                {
                    ChooseTarget();
                }
                break;

            case _states.Attack:
                Attack();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Entity target))
        {
            _targets.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Entity target))
        {
            _targets.Remove(target);
        }
    }
}
