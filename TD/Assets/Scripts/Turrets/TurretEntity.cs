using System;
using UnityEngine.AI;
using UnityEngine;

public class TurretEntity : Entity
{
    private bool _isOnDestroySignalSended = false;
    public Action<TurretEntity> OnDestroy;

    protected override void Die()
    {
        if (!_isOnDestroySignalSended)
        {
            _isOnDestroySignalSended = true;

            OnDestroy?.Invoke(GetComponent<TurretEntity>());
            GlobalEventManager.SendBuildingDestroy(GetComponent<MapUnit>());
        }

        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshObstacle>().enabled = false;
        GetComponent<Building>().enabled = false;
        GetComponent<TurretShooter>().enabled = false;
        GetComponent<BaseTurretStateMachine>().enabled = false;

        //Destroy(gameObject);
        base.Die();
    }
}
