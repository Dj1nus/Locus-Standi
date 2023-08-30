using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class IncreaseLevelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingTypes _type;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _cost;

    [SerializeField] private int _maxLevel;
    [SerializeField] private int[] costs;

    private Image _buttonBackground;
    private Button _button;
    private int _currentCost;
    private bool _isMaxLevel;
    private bool _isClickable = true;

    private void MaxLevelReached(BuildingTypes type)
    {
        if (type == _type)
        {
            _isMaxLevel = true;
            _cost.text = "Макс. уровень";
            _level.text = "Уровень " + Progress.Instance.GetMaxLevel(_type);

            _buttonBackground.color = Color.black;
            _button.interactable = false;
            _isClickable = false;
        }
    }

    private void CheckMoney(int value)
    {
        bool isInteracteble = value >= Progress.Instance.GetUpgradeCost(_type);
        Color costColor = isInteracteble ? Color.white : Color.red;

        _button.interactable = isInteracteble;
        _isClickable = isInteracteble;
        _cost.color = costColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClickable && !_isMaxLevel)
        {
            Progress.Instance.DecreaseMoneyValue(_currentCost);
            Progress.Instance.IncreaseLevel(_type);

            ChangeValues();
        }
    }

    private void ChangeValues()
    {
        if (!_isMaxLevel)
        {
            _level.text = "Уровень " + (Progress.Instance.GetLevel(_type) + 1).ToString();

            _currentCost = Progress.Instance.GetUpgradeCost(_type);
            _cost.text = "Цена " + _currentCost.ToString();
        }
    }

    private void Start()
    {
        if (Progress.Instance.GetLevel(_type) != -1)
        {
            Progress.OnMaxLevelReached += MaxLevelReached;
            Progress.OnMoneyValueChanged += CheckMoney;

            _button = GetComponent<Button>();
            _buttonBackground = GetComponentInParent<Image>();

            _currentCost = Progress.Instance.GetUpgradeCost(_type);

            ChangeValues();
        }

        else
        {
            Destroy(transform.parent.gameObject);
        }
        
    }

    private void OnDestroy()
    {
        Progress.OnMaxLevelReached -= MaxLevelReached;
        Progress.OnMoneyValueChanged -= CheckMoney;
    }
}
