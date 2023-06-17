using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent<Building> OnBuildingDestroy = new UnityEvent<Building>();

    public static void SendBuildingDestroy(Building building)
    {
        OnBuildingDestroy.Invoke(building);
    }
}
