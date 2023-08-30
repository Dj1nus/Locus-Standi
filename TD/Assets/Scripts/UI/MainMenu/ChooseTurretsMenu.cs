using UnityEngine;
using System.Collections.Generic;
using System;

public class ChooseTurretsMenu : MenuWindows
{
    [SerializeField] private int _countOfBuildings;

    [NonSerialized] public List<Building> _pickedBuildings = new();

    public Action<bool> OnArrayFullOrEmpty;

    public void AddBuilding(Building building)
    {
        _pickedBuildings.Add(building);

        CheckIsFull();
    }

    public void DeleteBuilding(Building building)
    {
        _pickedBuildings.Remove(building);

        CheckIsFull();
    }

    private void CheckIsFull()
    {
        OnArrayFullOrEmpty?.Invoke(_pickedBuildings.Count >= _countOfBuildings);
    }

    public int GetSize()
    { 
        return _pickedBuildings.Count;
    }

    public List<Building> GetSortedArray()
    {
        for (int i = 0; i < GetSize(); i++)
        {
            for (int j = 0; j < GetSize() - 1; j++)
            {
                if (_pickedBuildings[j].GetCost().GetTotalCost() > 
                    _pickedBuildings[j + 1].GetCost().GetTotalCost())
                {
                    (_pickedBuildings[j + 1], _pickedBuildings[j]) =
                        (_pickedBuildings[j], _pickedBuildings[j + 1]);
                }
            }
        }

        return _pickedBuildings;
    }

}
