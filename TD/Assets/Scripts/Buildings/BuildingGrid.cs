using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _mapSize;
    private Building[,] _map;

    public Vector2Int GetMapSize()
    {
        return _mapSize;
    }

    public bool IsPointAvaible(Vector2Int position, Building building)
    {
        foreach (Vector2Int point in building.GetClamedPoints())
        {
            if (_map[point.x, point.y] != null)
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

    public void DeleteBuildingFrommap(Building building)
    {
        foreach (Vector2Int point in building.GetClamedPoints())
        {
            _map[point.x, point.y] = null;
        }
    }

    private void Awake()
    {
        GlobalEventManager.OnBuildingDestroy.AddListener(DeleteBuildingFrommap);
        _map = new Building[_mapSize.x, _mapSize.y];
    }
}
