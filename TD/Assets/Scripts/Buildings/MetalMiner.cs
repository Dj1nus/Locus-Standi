using System.Collections;
using UnityEngine;

public class MetalMiner : Building
{
    [SerializeField] private float _baseQuantility;
    [SerializeField] private int _addQuantility;
    [SerializeField] private float _multiplier;

    [SerializeField] private int _quantility;
    [SerializeField] private float _miningSpeed;

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

        _quantility = Mathf.RoundToInt(_baseQuantility + _multiplier * 
            Progress.Instance.GetLevel(Progress.Types.miner) * _addQuantility);

        BuildingPlaced.AddListener(MinerPlaced);
        _resources = FindObjectOfType<PlayersResources>();
    }
}
