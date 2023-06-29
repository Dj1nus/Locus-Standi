using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _mapSize;
    [SerializeField] private Vector4 _mapBorders;
    private Building[,] _map;

    public Vector2Int GetMapSize()
    {
        return _mapSize;
    }

    public bool IsPointAvaible(Building building)
    { 
        foreach (Vector2Int point in building.GetClamedPoints())
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

        return true;
    }

    public void UpdateMap(Building building)
    {
        foreach (Vector2Int point in building.GetClamedPoints())
        {
            _map[point.x, point.y] = building;
        }
    }

    public void DeleteBuildingFromMap(Building building)
    {
        foreach (Vector2Int point in building.GetClamedPoints())
        {
            _map[point.x, point.y] = null;
        }
    }

    private void Awake()
    {
        GlobalEventManager.OnBuildingDestroy.AddListener(DeleteBuildingFromMap);
        _map = new Building[_mapSize.x, _mapSize.y];
    }
}
