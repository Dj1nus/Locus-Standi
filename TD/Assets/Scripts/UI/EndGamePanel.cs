using UnityEngine;
using DG.Tweening;
using System;

public class EndGamePanel : MonoBehaviour
{
    public Action<bool> OnScreenShow;

    private const int END_POSITION = 0;

    private RectTransform _transform;

    [SerializeField] private float _animationDuration;
    [SerializeField] private GameEndChecker _checker;

    private void OnEnable()
    {
        _transform = GetComponent<RectTransform>();
        GlobalEventManager.OnGameEnd += ShowPanel;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnGameEnd -= ShowPanel;
    }

    private void ShowPanel(bool isWin)
    {
        _transform.DOLocalMoveX(END_POSITION, _animationDuration).SetEase(Ease.Linear);

        OnScreenShow?.Invoke(isWin);
        
    }

}
