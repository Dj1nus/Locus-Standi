using System;
using UnityEngine;

public class MainBase : Entity
{
    protected override void Die()
    {
        GlobalEventManager.SendBaseDestroyed();
    }
}
