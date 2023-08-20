using UnityEngine;

public class SpitterStateMachine : BaseEnemyStateMachine
{
    [SerializeField] float _attackDistance;

    private SpitterShooter _shooter;

    private void Awake()
    {
        _shooter = GetComponent<SpitterShooter>();
    }

    protected override void IsCanAttack()
    {
        if (Target != null && 
            Vector3.Distance(transform.position, Target.transform.position) 
            <= Target.GetDistanceToDamage() + _attackDistance)
        {
            State = States.Attack;
        }
    }

    private bool isTooClose(Entity target)
    {


        return false;
    }

    public override void StateMachine()
    {
        IsCanAttack();

        switch (State)
        {
            case States.Move:
                TargetSelector.ChooseTarget();
                break;

            case States.Attack:
                if (isTooClose(Target))
                {

                }

                else
                {
                    if (Target != null)
                    {
                        _shooter.Shoot(Target, EntityComponent.GetDamage(), EntityComponent.GetCoolDown());
                    }
                    TargetSelector.StopMoving();
                }
                break;
        }
    }

    void FixedUpdate()
    {
        StateMachine();
    }
}
