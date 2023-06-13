using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _mapSize;

    private Building[,] _grid;

    public Vector3Int GlobalToArrayCoordinates(Vector3Int position)
    {
        Vector3Int result = new Vector3Int(0,0,0);

        if (position.x < 0)
            result.x = Mathf.Abs(position.x);
        else
            result.x = position.x * 2;
        if (position.z < 0)
            result.z = Mathf.Abs(position.z);
        else
            result.z = position.z * 2;

        return result;
    }

    public bool IsPointAvaible(Vector3Int position)
    {
        if ((position.x < -_mapSize.x || position.x > _mapSize.x) || (position.z < -_mapSize.y || position.z > _mapSize.y))
        {
            return false;
        }
        else
        {
            position = GlobalToArrayCoordinates(position);

            for (int i = 0; i < position.x; i++)
            {
                for (int j = 0; j < position.z; j++)
                {
                    if (_grid[position.x + i, position.z + j] != null)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void UpdateGrid(Building building, Vector3Int position)
    {
        position = GlobalToArrayCoordinates(position);

        for (int i = 0; i < position.x; i++)
        {
            for (int j = 0; j < position.z; j++)
            {
                _grid[position.x + i, position.z + j] = building;
            }
        }
    }

    private void Awake()
    {
        _grid = new Building[_mapSize.x * 2, _mapSize.y * 2];
    }
}
