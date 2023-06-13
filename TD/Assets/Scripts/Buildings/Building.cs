using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Vector2Int _gridPosiotion;
    [SerializeField] private int _costOrganic;
    [SerializeField] private int _costMetal;
    [SerializeField] private float _yOffset;

    private Renderer _renderer;
    private Collider _collider;
    private NavMeshObstacle _navMeshObstacle;
    private Material _material;
    private Color _color;

    public float GetYOffset()
    {
        return _yOffset;
    }
    public void SetStateToGhost()
    {
        _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        _collider.enabled = false;
        _navMeshObstacle.enabled = false;
    }

    public void SetStateToPlaced()
    {
        _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        _collider.enabled = true;
        _navMeshObstacle.enabled = true;
    }

    public void SetTransparent(bool isAvaible)
    {
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
    }

}
