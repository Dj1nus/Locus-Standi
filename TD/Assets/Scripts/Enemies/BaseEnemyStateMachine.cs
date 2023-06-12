using UnityEngine;

public class BaseEnemyStateMachine : MonoBehaviour
{
    private enum _states
    {
        Move,
        Attack
    }

    private _states _state;

    private BaseEnemyLogic _logic;
    private Entity _entity;


    public void Init()
    {
        //_logic.Init();

        _state = _states.Move;
        _logic = gameObject.GetComponent<BaseEnemyLogic>();
        _entity = gameObject.GetComponent<Entity>();
    }

    private void CheckState()
    {
        if (Vector3.Distance(transform.position, _logic.GetTarget().transform.position) <= 5f)
        {
            _state = _states.Attack;
            return;
        }
        _state = _states.Move;
    }

    public void StateMachine()
    {
        print(_state);
        _entity.CheckHp();

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

    //private void Start()
    //{
    //    Init();
    //}

    //private void Update()
    //{
    //    StateMachine();
    //}
}
