using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Building _gunTurret;
    [SerializeField] private BuildingManager _buildingManager;

    private Building _currentTurret;

    public Ray GetCursorRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        return ray;
    }


    void Start()
    {
        _currentTurret = _gunTurret;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _buildingManager.SetGhostBuilding(_currentTurret);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _buildingManager.PlaceBuilding();
        }
    }
}
