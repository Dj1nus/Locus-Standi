using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradesButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MenuWindows _window; 
    [SerializeField] private WindowsController.Types _type;
    [SerializeField] private WindowsController _controller;

    public void OnPointerClick(PointerEventData eventData)
    {
        _controller.ChangeActiveWindow(_window);

        //_controller.ChangeVisibility(_type);
    }
}
