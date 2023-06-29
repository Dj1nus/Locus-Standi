using UnityEngine;

public class BaseEnemyStateMachine : MonoBehaviour
{
    private enum _states
    {
        Move,
        Attack
    }
    private _states _state;

    private EnemyTargetSelector _logic;
    private Entity _entity;

    public void Init()
    {
        _state = _states.Move;
        _logic = GetComponent<EnemyTargetSelector>();
        _entity = GetComponent<Entity>();
    }

    private void CheckState()
    {
        if (_logic.GetTarget() != null && Vector3.Distance(transform.position, _logic.GetTarget().transform.position) <= 5f)
        {
            _state = _states.Attack;
            return;
        }
        _state = _states.Move;
    }

    public void StateMachine()
    {
        CheckState();
        switch (_state)
        {
            case _states.Move:
                _logic.ChooseTarget();
                break;

            case _states.Attack:
                _entity.Attack(_logic.GetTarget());
                break;
        }
    }
}
