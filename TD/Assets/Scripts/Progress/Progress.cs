using System;
using UnityEngine;
using System.Collections.Generic;

public enum BuildingTypes
{
    machinegun,
    rapidfire,
    shotgun,
    grenade,
    rocket,
    miner
}

public class Progress : MonoBehaviour
{
    [NonSerialized] public int CurrentLevel = 1;
    [NonSerialized] public int Money = 100;

    public static Action<int> OnMoneyValueChanged;
    public static Action<BuildingTypes> OnMaxLevelReached;
   
    public static Progress Instance;

    private Dictionary<int, BuildingTypes> _turretOpenOrder = new() 
    {
        {2, BuildingTypes.miner },
        {3, BuildingTypes.rapidfire},
        {5, BuildingTypes.shotgun},
        {8, BuildingTypes.grenade}
    };

    private Dictionary<BuildingTypes, int> _turretLevels = new()
    {
        { BuildingTypes.machinegun, 0 },
        { BuildingTypes.miner, -1 },
        { BuildingTypes.rapidfire, -1 },
        { BuildingTypes.shotgun, -1 },
        { BuildingTypes.grenade, -1 },
        { BuildingTypes.rocket, -1}
    };

    private Dictionary<BuildingTypes, int> _maxLevels = new()
    {
        {BuildingTypes.machinegun, 6},
        { BuildingTypes.miner, 5},
        { BuildingTypes.rapidfire, 5},
        { BuildingTypes.shotgun, 6},
        { BuildingTypes.grenade, 5},
        { BuildingTypes.rocket, 5}
    };

    private Dictionary<BuildingTypes, int[]> _UpradeCost = new() 
    {
        { BuildingTypes.machinegun, new[]{500, 2000, 4000, 7500, 10000} },
        { BuildingTypes.miner, new[]{ 900, 3000, 7500, 15000 } },
        { BuildingTypes.rapidfire, new[]{ 2500, 6000, 12000, 20000} },
        { BuildingTypes.shotgun, new[]{ 3000, 7000, 13500, 19000, 25000 } },
        { BuildingTypes.grenade, new[] { 4000, 8500, 16000, 30000 } },
        { BuildingTypes.rocket, new[] { 2000, 3500, 5000, 7000 } }
    };


    [NonSerialized] public Building[] ChoosedBuildings;

    public void InitArray(int size)
    {
        ChoosedBuildings = new Building[size];
    }

    public void IncreaseLevel(BuildingTypes type)
    {
        _turretLevels[type]++;

        if (_turretLevels[type] + 1 == _maxLevels[type])
            OnMaxLevelReached?.Invoke(type);
    }

    public int GetMaxLevel(BuildingTypes building)
    {
        return _maxLevels[building];
    }

    public int GetLevel(BuildingTypes type)
    {
        return _turretLevels[type];
    }

    public int GetUpgradeCost(BuildingTypes type)
    {
        int level = GetLevel(type);
        
        if (level != GetMaxLevel(type) - 1)
        {
            return _UpradeCost[type][GetLevel(type)];
        }

        OnMaxLevelReached?.Invoke(type);

        return 0;
    }

    public void IncreaseMoneyValue(int value)
    {
        Money += value;

        OnMoneyValueChanged?.Invoke(Money);
    }

    public void DecreaseMoneyValue(int value)
    {
        if (Money - value >= 0)
        {
            Money -= value;
        }

        OnMoneyValueChanged?.Invoke(Money);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    private void Start()
    {
        UpdateOpenedTurrets();

        CheckMaxLevelOnStart();

        OnMoneyValueChanged?.Invoke(Money);
    }

    private void CheckMaxLevelOnStart()
    {
        foreach (var (building,  level) in _turretLevels)
        {
            if (level == _maxLevels[building])
                OnMaxLevelReached?.Invoke(building);
        }
    }

    private void UpdateOpenedTurrets()
    {
        foreach (var pair in _turretOpenOrder)
        {
            if (CurrentLevel >= pair.Key)
            {
                BuildingTypes t = pair.Value;

                if (_turretLevels[t] < 0)
                {


                    _turretLevels[t] = 0;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            print(_turretLevels[BuildingTypes.machinegun]);
        }
    }

    public void OnLevelComplition(int index)
    {
        if (index == CurrentLevel)
        {
            CurrentLevel = index + 1;
        }

        UpdateOpenedTurrets();
        OnMoneyValueChanged?.Invoke(Money);
    }
}
