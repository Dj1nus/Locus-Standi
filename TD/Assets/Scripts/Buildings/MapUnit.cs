using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnit : MonoBehaviour
{
    public enum _types
    {
        turret,
        deposit,
        townhall,
        miner,
        wall
    }

    [SerializeField] private _types _type;
    [SerializeField] protected Vector2Int[] _takenPoints;

    private Vector2Int[] _clamedPoints;

    public _types GetUnitType()
    {
        return _type;
    }

    public void SetArrayToDefault()
    {
        _takenPoints.CopyTo(_clamedPoints, 0);
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
        _clamedPoints = new Vector2Int[_takenPoints.Length];
    }
}
