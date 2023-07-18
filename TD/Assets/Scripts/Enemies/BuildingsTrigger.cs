using UnityEngine;

public class BuildingsTrigger : MonoBehaviour 
{
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
        targetSelector = GetComponentInParent<EnemyTargetSelector>();
    }
}
