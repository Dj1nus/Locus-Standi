using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _mapSize;
    [SerializeField] private List<Vector3> _busyPoints = new List<Vector3>();

    public bool IsPointAvaible(Vector3Int position)
    {
        if ((position.x < -_mapSize.x || position.x > _mapSize.x) || (position.z < -_mapSize.y || position.z > _mapSize.y))
        {
            return false;
        }
        else if (_busyPoints.Contains(position))
        {
            return false;
        }

        return true;
    }

    public void UpdateList(Building building)
    {
        foreach (Vector3 position in building.GetClamedPoints()) 
        {
            _busyPoints.Add(new Vector3(building.transform.position.x + position.x, building.transform.position.y, building.transform.position.z + position.y));
        }
    }

    private void RemoveBuildingFromList(Building building)
    {
        print("Destroyed");

        foreach (Vector3 position in building.GetClamedPoints())
        {
            _busyPoints.Remove(new Vector3(building.transform.position.x + position.x, building.transform.position.y, building.transform.position.z + position.y));
        }
    }

    private void Awake()
    {
        GlobalEventManager.OnBuildingDestroy.AddListener(RemoveBuildingFromList);
    }
}
