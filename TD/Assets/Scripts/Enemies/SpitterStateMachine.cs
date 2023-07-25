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
        if (_target != null && 
            Vector3.Distance(transform.position, _target.transform.position) 
            <= _target.DistanceToDamage + _attackDistance)
        {
            _state = _states.Attack;
        }
    }

    private bool isTooClose(Entity target)
    {


        return false;
    }

    public override void StateMachine()
    {
        IsCanAttack();

        switch (_state)
        {
            case _states.Move:
                _targetSelector.ChooseTarget();
                break;

            case _states.Attack:
                if (isTooClose(_target))
                {

                }

                else
                {
                    if (_target != null)
                    {
                        _shooter.Shoot(_target, _entity.GetDamage(), _entity.GetCoolDown());
                    }
                    _targetSelector.StopMoving();
                }
                break;
        }
    }

    void Update()
    {
        StateMachine();
    }
}
