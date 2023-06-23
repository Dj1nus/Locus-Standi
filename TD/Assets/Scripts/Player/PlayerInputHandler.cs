using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Building _gunTurret;
    [SerializeField] private Builder _buildingManager;

    private Building _currentTurret;

    public Ray GetCursorRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        return ray;
    }

    public RaycastHit GetRaycastHit() 
    { 
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);

        return hit;
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
            _buildingManager.CreateGhostBuilding(_currentTurret);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _buildingManager.PlaceBuilding();
        }
    }
}
