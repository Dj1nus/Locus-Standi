using UnityEngine;

public class BaseEnemyStateMachine : MonoBehaviour
{
    protected enum States
    {
        Move,
        MoveToTurret,
        Spawn,
        Attack
    }

    protected States State;
    protected EnemyTargetSelector TargetSelector;
    protected Entity EntityComponent;
    protected Entity Target;

    public virtual void Init()
    {
        State = States.Move;
        TargetSelector = GetComponent<EnemyTargetSelector>();
        EntityComponent = GetComponent<Entity>();

        TargetSelector.OnBuildingDetected += OnTurretDetected;
    }

    private void OnDestroy()
    {
        TargetSelector.OnBuildingDetected -= OnTurretDetected;
    }

    public void OnTurretDetected(Entity target)
    {
        if (target is TurretEntity)
        {
            TurretEntity t = target as TurretEntity;
            t.OnDestroy += OnTurretDestroy;
        }

        Target = target;

        State = States.MoveToTurret;
    }

    private void OnTurretDestroy(Entity target)
    {
        if (target is TurretEntity)
        {
            TurretEntity t = Target as TurretEntity;
            t.OnDestroy -= OnTurretDestroy;
        }

        Target = TargetSelector.GetMainBase();

        TargetSelector.SetTargetToNull(target);

        State = States.Move;
    }

    protected virtual void IsCanAttack()
    {
        if (Target != null && Vector3.Distance(transform.position, Target.transform.position) <= Target.GetDistanceToDamage())
        {
            State = States.Attack;
        }
    }

    public void OnEnemyDied()
    {
        State = States.Move;
    }

    public virtual void StateMachine()
    {
        IsCanAttack();

        switch (State)
        {
            case States.Move:
                TargetSelector.ChooseTarget();
                break;

            case States.Attack:
                if (Target != null)
                {
                    EntityComponent.Attack(Target);
                }
                TargetSelector.StopMoving();
                break;
        }
    }
}
