using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
        Building[] tmp = _turretsMenu.GetSortedArray();

        for (int i = 0; i < _turretsMenu.GetSize(); i++)
        {
            if (tmp[i] != null)
            {
                Progress.Instance.ChoosedBuildings[i] = tmp[i];
            }
        }
    }

    private void Start()
    {
        _turretsMenu = GetComponentInParent<ChooseTurretsMenu>();
    }
}
