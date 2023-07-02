using System;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _mapSize;
    [SerializeField] private Vector4 _mapBorders;
    
    private MapUnit[,] _map;
    private MapUnit[] _deposits;

    public Vector2Int GetMapSize()
    {
        return _mapSize;
    }

    public bool IsPointAvaible(MapUnit unit)
    {
        if (unit.GetUnitType() == MapUnit._types.turret)
        {
            foreach (Vector2Int point in unit.GetClamedPoints())
            {
                if (point.x < _mapBorders.z || point.x > _mapBorders.x || point.y < _mapBorders.w || point.y > _mapBorders.y)
                {
                    return false;
                }

                else if (_map[point.x, point.y] != null)
                {
                    return false;
                }
            }
        }

        else if (unit.GetUnitType() == MapUnit._types.deposit)
        {
            foreach (Vector2Int point in unit.GetClamedPoints())
            {
                if (_map[point.x, point.y] != new Deposit())
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void UpdateMap(MapUnit unit)
    {
        if (unit.GetUnitType() == MapUnit._types.turret)
        {
            foreach (Vector2Int point in unit.GetClamedPoints())
            {
                _map[point.x, point.y] = unit as Building;
            }
        }

        else if (unit.GetUnitType() == MapUnit._types.deposit)
        {
            foreach (Vector2Int point in unit.GetClamedPoints())
            {
                _map[point.x, point.y] = unit as Deposit;
            }
        }
        
    }

    public void DeleteBuildingFromMap(MapUnit unit)
    {
        if (unit.GetUnitType() == MapUnit._types.miner)
        {
            foreach (Vector2Int point in unit.GetClamedPoints())
            {
                _map[point.x, point.y] = new Deposit();
            }
            return;
        }

        foreach (Vector2Int point in unit.GetClamedPoints())
        {
            _map[point.x, point.y] = null;
        }
    }

    private void Awake()
    {
        GlobalEventManager.OnBuildingDestroy.AddListener(DeleteBuildingFromMap);
        _map = new MapUnit[_mapSize.x, _mapSize.y];

        _deposits = FindObjectsOfType<Deposit>();

        foreach (MapUnit deposit in _deposits)
        {
            UpdateMap(deposit);
        }

        Array.Clear(_deposits, 0, _deposits.Length);
    }
}
