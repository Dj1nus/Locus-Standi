using UnityEngine;

public class MapVisual : MonoBehaviour
{
    public enum states
    {
        starting,
        building,
        defending
    }

    public states State;

    public states GetState()
    {
        return State;
    }

    public void ChangeState()
    {
        if (State == states.building) 
            State = states.defending;
        else
            State = states.building;

        GlobalEventManager.ChangeVisualMode(State);
    }

    private void Start()
    {
        State = states.defending;
    }
}
