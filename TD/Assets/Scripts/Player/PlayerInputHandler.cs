using System.Collections;
using UnityEngine;

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

    public bool _isDragging = false;

    //Они друзья,
    public void OnBuyBuildingButtonClick(Building building)
    {
        _isDragging = true;
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

    private void OnBuildingDrop()
    {
        _isDragging = false;

        _buildingManager.PlaceBuilding();
    }

    void Start()
    {
        GlobalEventManager.OnBuyButtonClick += OnBuyBuildingButtonClick;
        GlobalEventManager.OnPointerUp += OnBuildingDrop;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnPointerUp -= OnBuildingDrop;
        GlobalEventManager.OnBuyButtonClick -= OnBuyBuildingButtonClick;
    }

    void Update()
    {


        if (Input.GetMouseButton(0))
        {
            _x = Input.GetAxisRaw("Mouse X") * -1;
            _z = Input.GetAxisRaw("Mouse Y") * -1;
        }

        else if (Input.GetMouseButton(1))
        {
            _inputRotation = Input.GetAxisRaw("Mouse X");
        }

        if (!_isDragging)
        {
            _cameraMovement.CalculateInput(_x, _z);
            _cameraRotation.CalculateRotation(_inputRotation);

            float tmp = Input.GetAxisRaw("Mouse ScrollWheel");

            if (tmp != 0)
            {
                _cameraZoom.SetInput(tmp);
            }
            
        }

        _x = _z = 0;
        _inputRotation = 0;
        
    }
}
