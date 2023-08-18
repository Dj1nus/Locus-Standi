using UnityEngine;
using System;

public class ChooseTurretsMenu : MonoBehaviour
{
    [SerializeField] private int _countOfBuildings;

    [NonSerialized] public Building[] _pickedBuildings;

    public Action<bool> OnArrayFullOrEmpty;

    void Start()
    {
        _pickedBuildings = new Building[_countOfBuildings];
    }

    public void AddBuilding(Building building)
    {
        for (int i = 0; i < _countOfBuildings; i++)
        {
            if (_pickedBuildings[i] == null)
            {
                _pickedBuildings[i] = building;
                break;
            }
        }

        if (GetSize() == _countOfBuildings)
        {
            OnArrayFullOrEmpty?.Invoke(true);
        }
    }

    public void DeleteBuilding(Building building)
    {
        for (int i = 0; i < _countOfBuildings; i++)
        {
            if (_pickedBuildings[i] == building)
            {
                _pickedBuildings[i] = null;
                break;
            }
        }

        if (GetSize() < _countOfBuildings)
        {
            OnArrayFullOrEmpty?.Invoke(false);
        }
    }

    public int GetSize()
    {
        int a = 0;

        foreach (var i in _pickedBuildings)
        {
            if (i != null) a++;
        }

        //print(a);

        return a;
    }

    public Building[] GetSortedArray()
    {
        Building tmp;

        for (int i = 0; i < GetSize(); i++)
        {
            for (int j = 0; j < GetSize() - 1; j++)
            {
                if (_pickedBuildings[j].GetCost().GetTotalCost() > 
                    _pickedBuildings[j + 1].GetCost().GetTotalCost())
                {
                    tmp = _pickedBuildings[j + 1];
                    _pickedBuildings[j + 1] = _pickedBuildings[j];
                    _pickedBuildings[j] = tmp;
                }
            }
        }

        return _pickedBuildings;
    }

}
