using UnityEngine;

[System.Serializable]

public class Cost
{
    [SerializeField] public int organic;
    [SerializeField] public int metal;
    [SerializeField] public int score;

    public int GetTotalCost()
    {
        return organic + metal;
    }
}