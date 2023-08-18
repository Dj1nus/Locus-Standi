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

    [SerializeField] private GameObject _levelWindow;
    [SerializeField] private GameObject _upgradeWindow;
    [SerializeField] private GameObject _chooseWindow;
    [SerializeField] private GameObject _shopWindow;

    [NonSerialized] public int ChoosedLevelIndex;
    private PlayButton _play;

    private RectTransform _transform;

    public void ChangeVisibility(Types type)
    {
        switch (type)
        {
            case Types.level:
                _levelWindow.SetActive(!_levelWindow.activeInHierarchy);

                _upgradeWindow.SetActive(false);
                _chooseWindow.SetActive(false);
                //_shopWindow.SetActive(false);
                break;

            case Types.upgrade:
                _upgradeWindow.SetActive(!_upgradeWindow.activeInHierarchy);

                _chooseWindow.SetActive(false);
                //_shopWindow.SetActive(false);
                _levelWindow.SetActive(false);
                break;

            case Types.choose:
                _chooseWindow.SetActive(!_chooseWindow.activeInHierarchy);

                //_shopWindow.SetActive(false);
                _levelWindow.SetActive(false);
                _upgradeWindow.SetActive(false);

                break;

            case Types.shop:
                _shopWindow.SetActive(!_shopWindow.activeInHierarchy);

                _levelWindow.SetActive(false);
                _upgradeWindow.SetActive(false);
                _chooseWindow.SetActive(false);
                break;
            
            default:
                break;
        }
    }


    private void Start()
    {
        _levelWindow.SetActive(false);
        _upgradeWindow.SetActive(false);
        _chooseWindow.SetActive(false);

        _play = GetComponentInChildren<PlayButton>();

        _transform = GetComponent<RectTransform>();
        //_shopWindow.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _transform.DOLocalRotate(Vector3.zero, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _transform.DOLocalRotate(new Vector3(0, 5f, 0), 0.4f);
    }
}
