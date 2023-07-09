using UnityEngine;

public class BuyTurretButton : BuyButton
{
    [SerializeField] TurretBuilding _turret;
    
    public override void Init()
    {
        base.Init();

        _building = _turret;
    }
}
