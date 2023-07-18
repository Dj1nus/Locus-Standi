using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipPrepairing : MonoBehaviour
{
    public Action OnStartSpawning;

    [SerializeField] private SkipPrepairingButton _button;
    [SerializeField] private PrepairingTimer _timer;

    private void SendStart()
    {
        OnStartSpawning?.Invoke();

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        _button.OnButtonClick += SendStart;
        _timer.OnTimesLeft += SendStart;
    }

    private void OnDisable()
    {
        _button.OnButtonClick -= SendStart;
        _timer.OnTimesLeft -= SendStart;
    }
}
