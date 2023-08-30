using System;
using UnityEngine;

public class Wall : MapUnit
{
    private enum orientations
    {
        horizontal,
        vertical
    }

    [SerializeField] orientations orientation = orientations.vertical;

    public override void Init()
    {
        if (orientation == orientations.horizontal)
            for (int i = 0; i < TakenPoints.Length; i++)
                (TakenPoints[i].x, TakenPoints[i].y) = (TakenPoints[i].y, TakenPoints[i].x);
        
        base.Init();
    }

    private void Awake()
    {
        Init();
    }
}
