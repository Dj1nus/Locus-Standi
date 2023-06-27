using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetSelector : MonoBehaviour
{
    private Entity _mainBase;
    private NavMeshAgent  _agent;
    private Entity _target;
    private List<Entity> _targets = new List<Entity>();

    public void Init()
    {
        _mainBase = GameObject.Find("MainBase").GetComponent<Entity>();
        _agent =  GetComponent<NavMeshAgent>() ;
        _target = _mainBase;
        _agent.destination = _target.transform.position;
    }

    public Entity GetTarget()
    {
        return _target;
    }

    private void SetTarget(Entity target)
    {
        _target = target;
        _agent.destination = _target.transform.position;
    }
    public void CheckTargetExist()
    {
        if (_target == null)
        {
            _targets.Remove(_target);
            SetTarget(_mainBase);
        }
    }

    public void ChooseTarget()
    {
        float distanceToBase = Vector3.Distance(transform.position, _mainBase.transform.position);
        float minDistance = distanceToBase;

        Entity possibleTarget = null;

        foreach (Entity target in _targets)
        {
            if (target != null)
            {
                float distancetotarget = Vector3.Distance(transform.position, target.transform.position);
                if (distancetotarget < distanceToBase & distancetotarget <= minDistance)
                {
                    possibleTarget = target;
                    minDistance = distancetotarget;
                }
            }
        }
        
        if (possibleTarget != null)
        {
            SetTarget(possibleTarget);
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

    private void Update()
    {
        CheckTargetExist();
    }
}
