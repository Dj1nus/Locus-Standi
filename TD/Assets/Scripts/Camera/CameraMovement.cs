using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _smoothing = 5f;
    [SerializeField] private Vector2 _upBorders = new Vector2(100, 100);
    [SerializeField] private Vector2 _downBorders = new Vector2(0, 0);

    private Vector3 _targetPosition;
    private Vector3 _input;

    private void Awake()
    {
        _targetPosition = transform.position;
    }

    public void CalculateInput(float x, float z)
    {
        Vector3 horizontal = transform.right * x;
        Vector3 vertical = transform.forward * z;

        _input = (horizontal + vertical).normalized;
    }

    private void CameraMove()
    {
        Vector3 nextTargetPosition = _targetPosition + _input * _speed;
        
        if (IsInBounds(nextTargetPosition))
        {
            _targetPosition = nextTargetPosition;
        }

        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothing);
    }

    private bool IsInBounds(Vector3 position)
    {
        return position.x < _upBorders.x &&
            position.z < _upBorders.y &&
            position.x > _downBorders.x &&
            position.z > _downBorders.y;
    }

    private void Update()
    {
        CameraMove();
    }
}
