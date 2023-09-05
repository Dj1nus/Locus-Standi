using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class WindowsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum Types
    {
        level,
        upgrade,
        choose,
        shop,
        closeAll
    }

    [SerializeField] private MenuWindows[] _windows;

    [SerializeField] private GameObject _levelWindow;
    [SerializeField] private GameObject _upgradeWindow;
    [SerializeField] private GameObject _chooseWindow;
    [SerializeField] private GameObject _shopWindow;

    [NonSerialized] public int ChoosedLevelIndex;

    private RectTransform _transform;

    private void Start()
    {
        for (int i = 0; i < _windows.Length; i++)
        {
            _windows[i].ChangeVisibility(false);
        }

        _transform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _transform.DOLocalRotate(Vector3.zero, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _transform.DOLocalRotate(new Vector3(0, 5f, 0), 0.4f);
    }

    public void ChangeActiveWindow(MenuWindows activeWindow)
    {
        bool isWasActiveBeforeClick = activeWindow.IsActive;

        for (int i = 0; i < _windows.Length; i++)
        {
            _windows[i].ChangeVisibility(false);
        }

        activeWindow.ChangeVisibility(!isWasActiveBeforeClick);

    }
}
