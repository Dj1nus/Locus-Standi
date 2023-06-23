using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private BuildingGrid _grid;

    private Building _ghostBuilding;
    private bool _isCanPlace = false;
    
    public void CreateGhostBuilding(Building building) //Сделать кучу для "призрачных" зданий
    {
        if (_ghostBuilding != null)
        {
            Destroy(_ghostBuilding.gameObject);
        }

        _ghostBuilding = Instantiate(building);
    }

    public void PlaceBuilding()
    {
        if (_ghostBuilding != null && _isCanPlace) 
        {
            _grid.UpdateMap(_ghostBuilding);
            _ghostBuilding.SetState(true);
            _ghostBuilding = null;
        }
    }

    private void MoveGhostBuilding()
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray cameraToGroundRay = _inputHandler.GetCursorRay();

        if (groundPlane.Raycast(cameraToGroundRay, out float position))
        {
            Vector3 mousePositionInWorld = cameraToGroundRay.GetPoint(position);
            Vector3Int _possiblePosition = new Vector3Int(Mathf.RoundToInt(mousePositionInWorld.x), _ghostBuilding.GetYOffset(), Mathf.RoundToInt(mousePositionInWorld.z));

            _ghostBuilding.transform.position = _possiblePosition;

            _isCanPlace = _grid.IsPointAvaible(new Vector2Int(_possiblePosition.x, _possiblePosition.z), _ghostBuilding);

            _ghostBuilding.SetVisualMode(_isCanPlace);
        }
    }

    void Update()
    {
        if (_ghostBuilding != null)
        {
            MoveGhostBuilding();
        }
    }
}
