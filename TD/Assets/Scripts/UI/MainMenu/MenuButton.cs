using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Vector3 _size;
    [SerializeField] private Vector3 _rotation;

    private AudioPlayer _player;
    private bool _isBig;
    private RectTransform _transform;

    public void OnPointerClick(PointerEventData eventData)
    {
        _player.Play("Click", Random.Range(0.95f, 1.05f));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _player.Play("Enter", Random.Range(0.9f, 1.1f));

        _transform.DOLocalRotate(_rotation, 0.2f);
        _transform.DOScale(_size, 0.17f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _player.Play("Exit", Random.Range(0.9f, 1.1f));

        _transform.DOLocalRotate(Vector3.zero, 0.17f);
        _transform.DOScale(Vector3.one, 0.15f);
    }

    IEnumerator ClickSizeChanger()
    {
        yield return null;
    }

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
        _player = GetComponentInChildren<AudioPlayer>();
    }
}
