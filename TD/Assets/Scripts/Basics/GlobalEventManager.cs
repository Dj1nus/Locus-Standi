using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent<Building> OnBuildingDestroy = new UnityEvent<Building>();
    public static UnityEvent<Level._states> OnVisualModeChanged = new UnityEvent<Level._states>();
    public static UnityEvent OnResourceValueChanged = new UnityEvent();
    public static UnityEvent<Building> OnBuyButtonClick = new UnityEvent<Building>();

    public static void SendBuildingDestroy(Building building)
    {
        OnBuildingDestroy?.Invoke(building);
    }

    public static void ChangeVisualMode(Level._states state)
    {
        OnVisualModeChanged?.Invoke(state);
    }

    public static void ResourceValueChanged()
    {
        OnResourceValueChanged?.Invoke();
    }

    public static void BuyButtonClicked(Building building)
    {
        OnBuyButtonClick?.Invoke(building);
    }
}
