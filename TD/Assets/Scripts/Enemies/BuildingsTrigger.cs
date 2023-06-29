using System.Collections.Generic;
using UnityEngine;


public class BuildingsTrigger : MonoBehaviour 
{
    [SerializeField] private GameObject parent;
    private EnemyTargetSelector targetSelector;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Entity target))
        {
            targetSelector.AddTarget(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Entity target))
        {
            targetSelector.RemoveTarget(target);
        }
    }

    private void Start()
    {
        targetSelector = parent.GetComponent<EnemyTargetSelector>();
    }
}
