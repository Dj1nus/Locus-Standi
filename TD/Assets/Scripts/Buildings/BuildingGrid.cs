using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _mapSize;
    [SerializeField] private Building _mainBase;

    private Building[,] _grid;

    public bool IsPointAvaible(Vector3Int position, Building building)
    {
        if ((position.x < -_mapSize.x || position.x > _mapSize.x) || (position.z < -_mapSize.y || position.z > _mapSize.y))
        {
            return false;
        }

        //print(building);

        Vector2Int size = building.GetSize();
        Vector3 buildingPosition = building.transform.position;

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                //print(_grid[(int)buildingPosition.x + i, (int)buildingPosition.z + j]);
                if (_grid[(int)buildingPosition.x + i, (int)buildingPosition.z + j] != null)
                {
                    return false;
                }
            }
        }

        print(_grid);

        return true;
    }

    public void UpdateGrid(Building building)
    {
        Vector2Int size = building.GetSize();
        Vector3 position = building.transform.position;

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                _grid[(int)position.x + i, (int)position.z + j] = building;
            }
        }
    }

    private void Awake()
    {
        _grid = new Building[_mapSize.x, _mapSize.y];

    }
}
