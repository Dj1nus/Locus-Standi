using UnityEngine;

public class TurretBuilding : Building
{
    private BaseTurretStateMachine _baseTurretStateMachine;
    private EnemiesTrigger _trigger;
    private SphereCollider _sphereCollider;

    public override void SetVisualMode(bool isAvaible)
    {
        base.SetVisualMode(isAvaible);

        if (_state == _states.Placed) 
        { 
            _baseTurretStateMachine.enabled = true;
            _trigger.enabled = true;
            _sphereCollider.enabled = true;
        }

        else
        {
            _sphereCollider.enabled = false;
            _trigger.enabled = false;
            _baseTurretStateMachine.enabled = false;
        }

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
