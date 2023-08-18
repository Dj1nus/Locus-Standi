using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseLevelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private WindowsController.Types _type;
    [SerializeField] private WindowsController _controller;

    public void OnPointerClick(PointerEventData eventData)
    {
        _controller.ChangeVisibility(_type);
    }
}
