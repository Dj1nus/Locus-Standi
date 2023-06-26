using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersResources : MonoBehaviour
{
    private int _metal = 100;
    private int _organic = 100;

    public void IncreaseMetalValue(int metal)
    {
        _metal += metal;
    }

    public void DecreaseMetalValue(int metal)
    {
        _metal -= metal;
    }

    public void IncreaseOrganicValue(int organic) 
    { 
        _organic += organic;
    }

    public void DecreaseOrganicValue(int organic)
    {
        _organic -= organic;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            _metal += 100;
            _organic += 100;
        }
    }
}
