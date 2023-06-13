using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private BaseTurretClass _gunTurret;

    private BaseTurretClass _currentTurret;

    void Start()
    {
        _currentTurret = _gunTurret;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (raycastHit.collider.gameObject.layer != 9)
                {
                    Instantiate(_currentTurret, new Vector3(raycastHit.point.x, 1, raycastHit.point.z), Quaternion.identity);
                }
                else
                {
                    print("Не могу здесь строить!");
                }
            }
        }
    }
}
