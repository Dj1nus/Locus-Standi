using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Building _gunTurret;
    [SerializeField] private Builder _buildingManager;
    [SerializeField] private Level _level;

    private Building _currentBuilding;

    public void OnBuyBuildingButtonClick()
    {
        _buildingManager.CreateGhostBuilding(_currentBuilding);
    }

    public Ray GetCursorRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        return ray;
    }

    void Start()
    {
        _currentBuilding = _gunTurret;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _buildingManager.PlaceBuilding();
        }

        else if (_level.GetState() == Level._states.building && Input.GetMouseButtonDown(1))
        {
            _buildingManager.DestroyCurrentGhostBuilding();
        }
    }
}
