using UnityEngine;

public class EnemiesTrigger : MonoBehaviour
{
    private TurretTargetSelector targetSelector;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyEntity target))
        {
            targetSelector.AddTarget(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyEntity target))
        {
            targetSelector.RemoveTarget(target);
        }
    }

    private void Start()
    {
        targetSelector = GetComponentInParent<TurretTargetSelector>();
    }
}
