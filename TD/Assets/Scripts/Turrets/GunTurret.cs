using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : Entity
{
    private bool _isSignalSended = false;

    public override void Die()
    {
        if (!_isSignalSended)
        {
            _isSignalSended = true;
            GlobalEventManager.SendBuildingDestroy(GetComponent<Building>());
        }
        Destroy(gameObject);
    }
}
