using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Builder _buildingManager;
    [SerializeField] private MapVisual _level;


    //��� ������,
    public void OnBuyBuildingButtonClick(Building building)
    {
        StartCoroutine(KostylTimer(building));
    }
    IEnumerator KostylTimer(Building building)
    {
        yield return new WaitForSeconds(0.01f);
        _buildingManager.CreateGhostBuilding(building);
    }
    //�� �� ���������!!!


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
    }
}
