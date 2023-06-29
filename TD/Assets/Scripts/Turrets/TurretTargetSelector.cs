using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretTargetSelector : MonoBehaviour
{
    public UnityEvent<Entity> OnEnemyDetected = new UnityEvent<Entity>(); 

    private const float MIN_DISTANCE = 10f;

    private List<Entity> _targets = new List<Entity>();
    private Entity _target = null;

    public List<Entity> Targets { get { return _targets; } }

    public void AddTarget(Entity target)
    {
        _targets.Add(target);
    }

    public void RemoveTarget(Entity target) 
    {
        _targets.Remove(target);
    }

    private void SetTarget(Entity target)
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
        print(1);
        if (_targets.Count <= 0)
        {
            return;
        }

        float minDistance = MIN_DISTANCE;
        float distancetoTarget;

        Entity possibleTarget = null;

        foreach (Entity target in _targets)
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
            print("Sended");
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
