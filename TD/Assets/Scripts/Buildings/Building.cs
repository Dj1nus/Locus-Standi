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

    [SerializeField] private Cost _cost;
    [SerializeField] private Vector2Int[] _takenPoints;
    [SerializeField] private int _yOffset;
    [SerializeField] private Material _good;
    [SerializeField] private Material _bad;
    [SerializeField] private Material _standart;

    private Vector2Int[] _clamedPoints;
    private Renderer _renderer;
    private Collider _collider;
    private NavMeshObstacle _navMeshObstacle;

    public Vector2Int[] GetClamedPoints()
    {
        SetArrayToDefault();

        for (int i = 0; i < _clamedPoints.Length; i++)
        {
            _clamedPoints[i] = new Vector2Int(_clamedPoints[i].x + (int)transform.position.x, _clamedPoints[i].y + (int)transform.position.z);
        }

        return _clamedPoints;
    }

    public int GetYOffset()
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

    public void SetVisualMode(bool isAvaible)
    {
        if (_state == _states.Placed)
        {
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            _collider.enabled = true;
            _navMeshObstacle.enabled = true;
            _renderer.material = _standart;
        }
        else
        {
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            _collider.enabled = false;
            _navMeshObstacle.enabled = false;

            if (isAvaible)
            {
                _renderer.material = _good;
            }
            else
            {
                _renderer.material = _bad;
            }
        }
    }

    private void SetArrayToDefault()
    {
        //for (int i = 0; i < _takenPoints.Length; i++)
        //{
        //    //_clamedPoints[i] = _takenPoints[i];

        //}
        _takenPoints.CopyTo(_clamedPoints, 0);
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _state = _states.Ghost;

        _clamedPoints = new Vector2Int[_takenPoints.Length];
    }

    
}

[System.Serializable]

public class Cost
{
    [SerializeField] public int organic;
    [SerializeField] public int metal;
}