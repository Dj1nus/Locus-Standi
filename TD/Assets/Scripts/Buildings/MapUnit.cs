using UnityEngine;

public class MapUnit : MonoBehaviour
{
    public enum TurretTypes
    {
        turret,
        deposit,
        townhall,
        miner,
        wall
    }

    [SerializeField] private TurretTypes _type;

    [SerializeField] protected Vector2Int[] TakenPoints;

    private Vector2Int[] _clamedPoints;

    public TurretTypes GetUnitType()
    {
        return _type;
    }

    public void SetArrayToDefault()
    {
        TakenPoints.CopyTo(_clamedPoints, 0);
    }

    public Vector2Int[] GetClamedPoints()
    {
        SetArrayToDefault();

        for (int i = 0; i < _clamedPoints.Length; i++)
        {
            _clamedPoints[i] = new Vector2Int(_clamedPoints[i].x + (int)transform.position.x, _clamedPoints[i].y + (int)transform.position.z);
        }

        return _clamedPoints;
    }

    public virtual void Init() 
    {
        _clamedPoints = new Vector2Int[TakenPoints.Length];
    }
}
