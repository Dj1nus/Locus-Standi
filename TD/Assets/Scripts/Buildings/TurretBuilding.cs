using UnityEngine;

public class TurretBuilding : Building
{
    private BaseTurretStateMachine _baseTurretStateMachine;

    public override void SetVisualMode(bool isAvaible)
    {
        base.SetVisualMode(isAvaible);

        if (_state == _states.Placed) 
        { 
            _baseTurretStateMachine.enabled = true;
        }

        else
        {
            _baseTurretStateMachine.enabled = false;
        }

    }

    public override void Init()
    {
        base.Init();
        GetComponentInChildren<PovVisualizer>().Init();
        _baseTurretStateMachine = GetComponent<BaseTurretStateMachine>();
    }

    private void Start()
    {
        Init();
    }
}
