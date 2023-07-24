using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetSelector : MonoBehaviour
{
    public Action<Entity> OnBuildingDetected;

    private Entity _mainBase;
    private NavMeshAgent _agent;
    private Entity _target;
    private List<Entity> _targets = new List<Entity>();

    public void Init()
    {
        _mainBase = GameObject.Find("MainBase").GetComponent<Entity>();
        _agent = GetComponent<NavMeshAgent>() ;
        _target = _mainBase;
        _agent.destination = _target.transform.position;
    }

    public Entity GetMainBase()
    {
        return _mainBase;
    }

    public void AddTarget(Entity target)
    {
        _targets.Add(target);

        if (_target == _mainBase)
        {
            ChooseTarget();
        }
    }

    public void RemoveTarget(Entity target)
    {
        _targets.Remove(target);
    }

    public Entity GetTarget()
    {
        return _target;
    }

    private void SetTarget(Entity target)
    {
        _target = target;

        _agent.destination = _target.transform.position;

        OnBuildingDetected?.Invoke(_target);
    }

    public void StopMoving()
    {
        _agent.destination = transform.position;
    }

    public void ChooseTarget()
    {
        if (_targets.Count <= 0)
        {
            SetTarget(_mainBase);
        }

        float distanceToBase = Vector3.Distance(transform.position, _mainBase.transform.position);
        float minDistance = distanceToBase;

        Entity possibleTarget = null;

        foreach (Entity target in _targets)
        {
            if (target == null)
            {
                continue;
            }

            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget < distanceToBase & distanceToTarget <= minDistance)
            {
                possibleTarget = target;
                minDistance = distanceToTarget;
            }
        }
        
        if (possibleTarget != null)
        {
            SetTarget(possibleTarget);
            RemoveTarget(possibleTarget);
            return;
        }
    }

    public void SetTargetToNull(Entity target)
    {
        RemoveTarget(target);
        _target = null;
    }

    private void Update()
    {
        if (_target == null )//_targets.Count > 0)
        {
            ChooseTarget();
        }
    }
}
