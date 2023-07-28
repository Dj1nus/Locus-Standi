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
        {
            int[] tmpArray = new int[_takenPoints.Length];

            for (int i = 0; i < _takenPoints.Length; i++)
            {
                tmpArray[i] = _takenPoints[i].y;
                _takenPoints[i].y = _takenPoints[i].x;
                _takenPoints[i].x = tmpArray[i];
            }

            Array.Clear(tmpArray, 0, tmpArray.Length);
            tmpArray = null;
        }

        base.Init();
    }

    private void Awake()
    {
        Init();
    }
}
