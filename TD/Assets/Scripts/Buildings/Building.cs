using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Building : MapUnit
{
    public enum _states
    {
        Ghost,
        Placed
    }

    protected _states _state;

    [SerializeField] private Cost _cost;

    [SerializeField] private float _yOffset;
    [SerializeField] private Material _good;
    [SerializeField] private Material _bad;
    [SerializeField] private Material _standart;

    protected UnityEvent BuildingPlaced = new UnityEvent();

    private Renderer _renderer;
    private Collider _collider;
    private NavMeshObstacle _navMeshObstacle;

    public float GetYOffset()
    {
        return _yOffset;
    }

    public _states GetState()
    {
        return _state;
    }

    public Cost GetCost()
    {
        return _cost;
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

    public virtual void SetVisualMode(bool isAvaible)
    {
        if (_state == _states.Placed)
        {
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            _collider.enabled = true;
            _navMeshObstacle.enabled = true;
            _renderer.material = _standart;

            BuildingPlaced?.Invoke();


        }
        else
        {
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            _collider.enabled = false;
            _navMeshObstacle.enabled = false;

            if (isAvaible)
                _renderer.material = _good;

            else
                _renderer.material = _bad;
        }
    }

    public override void Init()
    {
        base.Init();

        _renderer = GetComponentInChildren<Renderer>();
        _collider = GetComponentInChildren<BoxCollider>();
        _navMeshObstacle = GetComponentInChildren<NavMeshObstacle>();

        _state = _states.Ghost;
    }

    private void Start()
    {
        Init();
    }
}

