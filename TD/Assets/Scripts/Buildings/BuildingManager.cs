using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _playerInputHandler;
    [SerializeField] private Building _gunTurret;
    [SerializeField] private BuildingGrid _grid;
    
    private Building _pickedBuilding;
    private bool _isCanPlace = false;

    //private Vector3 _possiblePosition;
    
    public void CreateGhostBuilding(Building building) //Сделать кучу для "призрачных" зданий
    {
        if (_pickedBuilding != null)
        {
            Destroy(_pickedBuilding.gameObject);
        }

        _pickedBuilding = Instantiate(building);
    }

    public void PlaceBuilding()
    {
        if (_pickedBuilding != null && _isCanPlace) 
        {
            _pickedBuilding.SetStateToPlaced();
            _pickedBuilding = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        if (_pickedBuilding != null)
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray cameraToGroundRay = _playerInputHandler.GetCursorRay();

            if (groundPlane.Raycast(cameraToGroundRay, out float position))
            {
                Vector3 mousePositionInWorld = cameraToGroundRay.GetPoint(position);
                _pickedBuilding.SetStateToGhost();

                Vector3Int _possiblePosition = new Vector3Int(Mathf.RoundToInt(mousePositionInWorld.x), (int)_pickedBuilding.GetYOffset(), Mathf.RoundToInt(mousePositionInWorld.z));
                _pickedBuilding.transform.position = _possiblePosition;

                _isCanPlace = _grid.IsPointAvaible(_possiblePosition);

                _pickedBuilding.SetTransparent(_isCanPlace);
                

            }

        }
    }
}
