using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _smoothing = 5f;

    private float _targetAngle;
    private float _currentAngle;

    private void Awake()
    {
        _currentAngle = _targetAngle = transform.eulerAngles.y;
    }

    public void CalculateRotation(float inputRotation)
    {
        _targetAngle += inputRotation * _speed;
    }

    private void Rotate()
    {
        _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, Time.deltaTime * _smoothing);
        transform.rotation = Quaternion.AngleAxis(_currentAngle, Vector3.up);
    }

    private void Update()
    {
        Rotate();
    }

}
