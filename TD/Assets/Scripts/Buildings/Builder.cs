using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private PlayersResources _playersResources;
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private BuildingGrid _grid;

    private Building _ghostBuilding;
    private bool _isCanPlace = false;
    private Vector3 _previousPosition = new();

    public void DestroyCurrentGhostBuilding()
    {
        if (_ghostBuilding != null )
            Destroy(_ghostBuilding.gameObject);
    }

    public void CreateGhostBuilding(Building building)
    {
        DestroyCurrentGhostBuilding();

        _ghostBuilding = Instantiate(building);
        _ghostBuilding.Init();

        if (_ghostBuilding.GetUnitType() == MapUnit.TurretTypes.turret)
        {
            _ghostBuilding.GetComponent<BaseTurretStateMachine>().Init();
        }
    }

    public void PlaceBuilding()
    {
        if (_ghostBuilding != null && _isCanPlace) 
        {
            Cost cost = _ghostBuilding.GetCost();

            _playersResources.DecreaseMetalValue(cost.metal);
            _playersResources.DecreaseOrganicValue(cost.organic);

            _grid.UpdateMap(_ghostBuilding);
            _ghostBuilding.SetState(true);
            _ghostBuilding = null;
        }

        else
        {
            DestroyCurrentGhostBuilding();
        }
    }

    private void MoveGhostBuilding()
    {
        Plane groundPlane = new(Vector3.up, Vector3.zero);
        Ray cameraToGroundRay = _inputHandler.GetCursorRay();

        if (groundPlane.Raycast(cameraToGroundRay, out float position))
        {
            Vector3 mousePositionInWorld = cameraToGroundRay.GetPoint(position);
            
            if (mousePositionInWorld != _previousPosition)
            {
                Vector3 _possiblePosition = new Vector3(
                    Mathf.RoundToInt(mousePositionInWorld.x), 
                    _ghostBuilding.GetYOffset(),
                    Mathf.RoundToInt(mousePositionInWorld.z));

                _ghostBuilding.transform.position = _possiblePosition;

                _isCanPlace = _grid.IsPointAvaible(_ghostBuilding);

                _ghostBuilding.SetVisualMode(_isCanPlace);

                _previousPosition = mousePositionInWorld;
            }
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
