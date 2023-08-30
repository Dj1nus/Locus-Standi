using UnityEngine;
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
    private AudioPlayer _audioPlayer;

    public float GetYOffset() => _yOffset;

    public _states GetState() => _state;

    public Cost GetCost() => _cost;

    public void SetState(bool isPlaced)
    {
        if (isPlaced)
        {
            _state = _states.Placed;
            GlobalEventManager.SendBuildingPlaced(_cost);

            if (_audioPlayer != null)
            {
                _audioPlayer.Play(SoundTypes.Init);
            }
        }
        else
        {
            _state = _states.Ghost;
        }
        SetVisualMode(isPlaced);
    }

    public virtual void SetVisualMode(bool isAvaible)
    {
        var isShadowCast = isAvaible ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
        bool isEnabled = _state == _states.Placed;

        _renderer.shadowCastingMode = isShadowCast;
        _collider.enabled = isEnabled;

        if (_state == _states.Placed)
        {
            _renderer.material = _standart;

            BuildingPlaced?.Invoke();
        }
        else
        {
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
        _audioPlayer = GetComponentInChildren<AudioPlayer>();

        _state = _states.Ghost;
    }
}

