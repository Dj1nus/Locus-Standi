using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetSelector : MonoBehaviour
{
    public Action<EnemyEntity> OnEnemyDetected;

    private const float MIN_DISTANCE = 10f;

    private List<EnemyEntity> _targets = new List<EnemyEntity>();
    private EnemyEntity _target = null;

    public List<EnemyEntity> Targets { get { return _targets; } }

    public void AddTarget(EnemyEntity target)
    {
        _targets.Add(target);
    }

    public void RemoveTarget(EnemyEntity target) 
    {
        _targets.Remove(target);
    }

    private void SetTarget(EnemyEntity target)
    {
        _target = target;
        OnEnemyDetected?.Invoke(target);
    }

    public void SetTargetToNull()
    {
        _target = null;
    }

    public Entity GetTarget()
    {
        return _target;
    }

    public void ChooseTarget()
    {
        //print(1);
        if (_targets.Count <= 0)
        {
            return;
        }

        float minDistance = MIN_DISTANCE;
        float distancetoTarget;

        EnemyEntity possibleTarget = null;

        foreach (EnemyEntity target in _targets)
        {
            if (target == null)
            {
                continue;
            }

            distancetoTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distancetoTarget < minDistance)
            {
                possibleTarget = target;
                minDistance = distancetoTarget;
            }
        }

        if (possibleTarget != null)
        {
            SetTarget(possibleTarget);
            RemoveTarget(possibleTarget);
            return;
        }
    }

    private void Update()
    {
        if (_target == null && _targets.Count > 0)
        {
            ChooseTarget();
        }
    }
}
