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
        if (unit is TurretBuilding)
        {
            foreach (Vector2Int point in unit.GetClamedPoints())
            {
                if (point.x > _mapBorders.x || point.x < _mapBorders.w || point.y > _mapBorders.y || point.y < _mapBorders.z)
                {
                    return false;
                }

                else if (_map[point.x, point.y] != null)
                {
                    return false;
                }
            }
        }

        else if (unit is MetalMiner)
        {
            foreach (Vector2Int point in unit.GetClamedPoints())
            {
                if (!(_map[point.x, point.y] is Deposit))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void UpdateMap<TMapUnit>(TMapUnit mapUnit)
        where TMapUnit : MapUnit
    {
        foreach (Vector2Int point in mapUnit.GetClamedPoints())
        {
            _map[point.x, point.y] = mapUnit;
        }
    }

    public void DeleteBuildingFromMap<TMapUnit>(TMapUnit unit)
        where TMapUnit : MapUnit
    {
        MapUnit placeholder = unit is MetalMiner ? FindObjectOfType<Deposit>() : null;

        foreach (Vector2Int point in unit.GetClamedPoints())
        {
            _map[point.x, point.y] = placeholder;
        }
    }

    private void Start()
    {
        GlobalEventManager.OnBuildingDestroy += DeleteBuildingFromMap;

        InitMap();

        GetComponentInChildren<GridVisualizer>().Init(new Vector2Int((int)_mapBorders.w,
            (int)_mapBorders.z), new Vector2Int((int)_mapBorders.x, (int)_mapBorders.y));
    }

    private void InitMap()
    {
        _map = new MapUnit[_mapSize.x, _mapSize.y];

        _deposits = FindObjectsOfType<Deposit>();

        foreach (MapUnit deposit in _deposits)
        {
            UpdateMap(deposit);
        }

        Array.Clear(_deposits, 0, _deposits.Length);

        UpdateMap(FindObjectOfType<MainBaseBuilding>());

        _deposits = FindObjectsOfType<Wall>();

        foreach (MapUnit wall in _deposits)
        {
            UpdateMap(wall);
        }
    }

    private void OnDisable()
    {
        GlobalEventManager.OnBuildingDestroy -= DeleteBuildingFromMap;
    }
}
