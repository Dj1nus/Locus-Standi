using System.Collections;
using UnityEngine;

public class MetalMiner : Building
{
    [SerializeField] private float _miningSpeed;
    [SerializeField] private int _quantility;

    private bool _isCoroutineStarget = false;
    private PlayersResources _resources;

    IEnumerator Mining()
    {
        _isCoroutineStarget = true;

        while (true)
        {
            yield return new WaitForSeconds(_miningSpeed);

            _resources.IncreaseMetalValue(_quantility);
        }
    }

    private void MinerPlaced()
    {
        if (!_isCoroutineStarget)
        {
            StartCoroutine(Mining());
        }
    }

    public override void Init()
    {
        base.Init();

        BuildingPlaced.AddListener(MinerPlaced);
        _resources = FindObjectOfType<PlayersResources>();
    }
}
