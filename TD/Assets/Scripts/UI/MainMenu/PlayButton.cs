using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private WindowsController _controller;
    private ChooseTurretsMenu _turretsMenu;

    public void OnPointerClick(PointerEventData eventData)
    {
        int sizze = _turretsMenu.GetSize();

        if ( sizze > 0 )
        {
            Progress.Instance.InitArray(_turretsMenu.GetSize());

            //_turretsMenu.GetSortedArray().CopyTo(Progress.Instance.ChoosedBuildings, 0);

            CopyArray();

            SceneManager.LoadScene(_controller.ChoosedLevelIndex);
        }
    }

    private void CopyArray()
    {
        List<Building> choosedBuildings = _turretsMenu.GetSortedArray();

        for (int i = 0; i < _turretsMenu.GetSize(); i++)
        {
            if (choosedBuildings[i] != null)
            {
                Progress.Instance.ChoosedBuildings[i] = choosedBuildings[i];
            }
        }
    }

    private void Start()
    {
        _turretsMenu = GetComponentInParent<ChooseTurretsMenu>();
    }
}
