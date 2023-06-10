using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyClass : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private GameObject _mainTarget;

    public void Init()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    protected void Move()
    {
        _agent.destination = _mainTarget.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
