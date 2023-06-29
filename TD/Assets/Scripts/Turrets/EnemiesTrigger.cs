using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemiesTrigger : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private TurretTargetSelector targetSelector;

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
        targetSelector = parent.GetComponent<TurretTargetSelector>();
    }
}
