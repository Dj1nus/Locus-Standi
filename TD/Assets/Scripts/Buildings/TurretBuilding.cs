using UnityEngine;

public class TurretBuilding : Building
{
    private BaseTurretStateMachine _baseTurretStateMachine;
    private EnemiesTrigger _trigger;
    private SphereCollider _sphereCollider;

    public override void SetVisualMode(bool isAvaible)
    {
        base.SetVisualMode(isAvaible);

        bool isEnabled = _state == _states.Placed;

        _baseTurretStateMachine.enabled = isEnabled;
        _trigger.enabled = isEnabled;
        _sphereCollider.enabled = isEnabled;
    }

    public override void Init()
    {
        base.Init();
        GetComponentInChildren<PovVisualizer>().Init();
        _baseTurretStateMachine = GetComponent<BaseTurretStateMachine>();
        _trigger = GetComponentInChildren<EnemiesTrigger>();
        _sphereCollider = GetComponentInChildren<SphereCollider>();

        _sphereCollider.enabled = false;
        _trigger.enabled = false;
    }

    private void Start()
    {
        Init();
    }
}
