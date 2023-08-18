using System;
using UnityEngine;
using System.Collections.Generic;

public class Progress : MonoBehaviour
{
    [NonSerialized] public int CurrentLevel = 1;
    [NonSerialized] public int Money = 0;
    [NonSerialized] public int MinerLevel = -1;
    [NonSerialized] public int MachinegunLevel = 0;
    [NonSerialized] public int RapidLevel = -1;
    [NonSerialized] public int ShotgunLevel = -1;
    [NonSerialized] public int GrenadeLevel = -1;
    [NonSerialized] public int RocketLevel = -1;


    public static Action<int> OnMoneyValueChanged;

    public enum Types
    {
        machinegun,
        rapidfire,
        shotgun,
        grenade,
        rocket,
        miner
    }

    private Dictionary<int, Types> _turretProgression = new Dictionary<int, Types>() 
    {
        {2, Types.miner },
        {3, Types.rapidfire},
        {5, Types.shotgun},
        {8, Types.grenade}
    };

    public static Progress Instance;

    [NonSerialized] public Building[] ChoosedBuildings;

    public void InitArray(int size)
    {
        ChoosedBuildings = new Building[size];
    }

    public void IncreaseLevel(Types type)
    {
        switch (type)
        {
            case Types.machinegun:
                MachinegunLevel++;
                break;

            case Types.rapidfire:
                RapidLevel++;
                break;

            case Types.shotgun:
                ShotgunLevel++;
                break;

            case Types.grenade:
                GrenadeLevel++;
                break;

            case Types.rocket:
                RocketLevel++;
                break;

            case Types.miner:
                MinerLevel++;
                break;
        }
    }

    public int GetLevel(Types type)
    {
        switch (type)
        {
            case Types.machinegun:
                return MachinegunLevel;

            case Types.rapidfire:
                return RapidLevel;

            case Types.shotgun:
                return ShotgunLevel;

            case Types.grenade:
                return GrenadeLevel;

            case Types.rocket:
                return RocketLevel;

            case Types.miner:
                return MinerLevel;
        }

        return 1;
    }

    public void IncreaseMoneyValue(int value)
    {
        Money += value;

        OnMoneyValueChanged?.Invoke(Money);
    }

    public void DecreaseMoneyValue(int value)
    {
        if (Money - value < 0)
        {
            Money = 0;
        }
        else 
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

        UpdateOpenedTurrets();
    }

    private void UpdateOpenedTurrets()
    {
        foreach (var pair in _turretProgression)
        {
            if (CurrentLevel >= pair.Key)
            {
                Types t = pair.Value;

                switch (t)
                {
                    case Types.miner:
                        if (MinerLevel < 0) MinerLevel = 0;
                        break;

                    case Types.rapidfire:
                        if (RapidLevel < 0) RapidLevel = 0;
                        break;

                    case Types.shotgun:
                        if (ShotgunLevel < 0) ShotgunLevel = 0;
                        break;

                    case Types.grenade:
                        if (GrenadeLevel < 0) GrenadeLevel = 0;
                        break;

                    case Types.rocket:
                        if (RocketLevel < 0) RocketLevel = 0;
                        break;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            print(CurrentLevel);
        }
    }

    public void OnLevelComplition(int index)
    {
        if (index == CurrentLevel)
        {
            CurrentLevel = index + 1;
        }

        UpdateOpenedTurrets();
    }
}
