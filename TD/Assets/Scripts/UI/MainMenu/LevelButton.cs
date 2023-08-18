using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private WindowsController _controller;
    [SerializeField] private int _levelindex;
    [SerializeField] private GameObject _image;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
        {
            _controller.ChoosedLevelIndex = _levelindex;

            _controller.ChangeVisibility(WindowsController.Types.choose);
        }
        
    }

    private void Start()
    {
        if (Progress.Instance.CurrentLevel < _levelindex)
        {
            GetComponent<Button>().interactable = false;
            _image.SetActive(true);
        }

        else
        {
            GetComponent<Button>().interactable = true;
            _image.SetActive(false);
        }
    }
}
