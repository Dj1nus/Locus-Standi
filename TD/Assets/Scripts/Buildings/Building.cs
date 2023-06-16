using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    public enum _states
    {
        Ghost,
        Placed
    }

    private _states _state; 
    
    [SerializeField] private Vector2[] _clamedPoints;
    [SerializeField] private Vector2Int _gridPosition;
    [SerializeField] private int _costOrganic;
    [SerializeField] private int _costMetal;
    [SerializeField] private float _yOffset;

    private Renderer _renderer;
    private Collider _collider;
    private NavMeshObstacle _navMeshObstacle;
    private Color _color;

    public Vector2[] GetClamedPoints()
    {
        return _clamedPoints;
    }

    public float GetYOffset()
    {
        return _yOffset;
    }

    public _states GetState()
    {
        return _state;
    }

    public void SetState(bool isPlaced)
    {
        if (isPlaced)
        {
            _state = _states.Placed;
        }
        else
        {
            _state = _states.Ghost;
        }
        SetVisualMode(isPlaced);
    }

    public void SetVisualMode(bool isAvaible)
    {
        if (_state == _states.Placed)
        {
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            _collider.enabled = true;
            _navMeshObstacle.enabled = true;
        }
        else
        {
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            _collider.enabled = false;
            _navMeshObstacle.enabled = false;
        }

        if (isAvaible)
        {
            _renderer.material.color = _color;
        }
        else
        {
            _renderer.material.color = Color.red;
        }
    }
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _color = _renderer.material.color;
        _state = _states.Ghost;
        //_size = Vector2Int.one;
    }

}
