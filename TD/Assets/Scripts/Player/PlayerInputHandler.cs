using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Builder _buildingManager;
    [SerializeField] private MapVisual _level;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private CameraRotation _cameraRotation;
    [SerializeField] private CameraZoom _cameraZoom;

    private float _x;
    private float _z;
    private float _inputRotation;

    //Они друзья,
    public void OnBuyBuildingButtonClick(Building building)
    {
        StartCoroutine(KostylTimer(building));
    }
    IEnumerator KostylTimer(Building building)
    {
        yield return new WaitForSeconds(0.01f);
        _buildingManager.CreateGhostBuilding(building);
    }
    //Их не разлучать!!!


    public Ray GetCursorRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        return ray;
    }

    void Start()
    {
        GlobalEventManager.OnBuyButtonClick.AddListener(OnBuyBuildingButtonClick);
    }

    void Update()
    {
        if (_level.GetState() == MapVisual._states.building && Input.GetMouseButtonDown(0))
        {
            _buildingManager.PlaceBuilding();
        }

        else if (_level.GetState() == MapVisual._states.building && Input.GetMouseButtonDown(1))
        {
            _buildingManager.DestroyCurrentGhostBuilding();
        }

        else if (Input.GetMouseButton(0))
        {
            _x = Input.GetAxisRaw("Mouse X") * -1;
            _z = Input.GetAxisRaw("Mouse Y") * -1;
        }

        else if (Input.GetMouseButton(1))
        {
            _inputRotation = Input.GetAxisRaw("Mouse X");
        }

        _cameraMovement.CalculateInput(_x, _z);
        _cameraRotation.CalculateRotation(_inputRotation);
        _cameraZoom.SetInput(Input.GetAxisRaw("Mouse ScrollWheel"));

        _x = _z = 0;
        _inputRotation = 0;


        
    }
}
