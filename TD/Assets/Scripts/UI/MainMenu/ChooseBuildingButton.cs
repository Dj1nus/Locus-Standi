using UnityEngine;
using UnityEngine.UI;

public class ChooseBuildingButton : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [SerializeField] private Progress.Types _type;
    [SerializeField] private Image _background;
    [SerializeField] private Button _button;
    [SerializeField] private ChooseTurretsMenu _menu;
    [SerializeField] private Building _building;

    private bool _isSelected = false;
    private bool _isArrayFull = false;

    private void ChangeVisibility(bool flag)
    {
        //print("chichichel");

        _isArrayFull = flag;

        if (!_isSelected)
        {
            if (flag)
            {
                _button.GetComponent<Image>().color = Color.gray;
                _button.interactable = false;
            }
            else
            {
                _button.GetComponent<Image>().color = Color.white;
                _button.interactable = true;
            }
        }
    }

    public void OnClick()
    {
        if (_isActive)
        {
            if (!_isSelected && !_isArrayFull)
            {
                _isSelected = true;
                _menu.AddBuilding(_building);
                _background.enabled = true;
            }

            else if (_isSelected)
            {
                _isSelected = false;
                _menu.DeleteBuilding(_building);

                _background.enabled = false;
            }
        }
    }

    private void Start()
    {
        if (_menu != null)
        {
            _menu.OnArrayFullOrEmpty += ChangeVisibility;

            if (Progress.Instance.GetLevel(_type) < 0)
            {
                _isActive = false;
                _menu.OnArrayFullOrEmpty -= ChangeVisibility;
                _button.interactable = false;
                _background.color = Color.gray;
            }
        }

        
    }

    private void OnDestroy()
    {
        if (_menu != null)
        {
            _menu.OnArrayFullOrEmpty -= ChangeVisibility;
        }
    }
}
