using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetSelector : MonoBehaviour
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

    public void AddTarget(Entity target)
    {
        _targets.Add(target);
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
    }

    public void StopMoving()
    {
        _agent.destination = transform.position;
    }

    //Это тоже друзья
    IEnumerator Kostyl()
    {
        yield return new WaitForSeconds(0.01f);

        if (_target == null)
        {
            _targets.Remove(_target);
            SetTarget(_mainBase);
        }
    }
    private void CheckTargetExist(MapUnit building)
    {
        StartCoroutine(Kostyl());
    }
    //Их тоже нельзя разлучать
    //KOSTYLS LIVES MATTERS!!!

    public void ChooseTarget()
    {
        if (_targets.Count <= 0)
        {
            return;
        }

        float distanceToBase = Vector3.Distance(transform.position, _mainBase.transform.position);
        float minDistance = distanceToBase;

        Entity possibleTarget = null;

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
        
        if (possibleTarget != null)
        {
            _targets.Remove(possibleTarget);
            SetTarget(possibleTarget);
        }
    }

    private void Start()
    {
        GlobalEventManager.OnBuildingDestroy.AddListener(CheckTargetExist);
    }
}
