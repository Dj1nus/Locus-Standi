using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class IncreaseLevelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Progress.Types _type;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _cost;

    [SerializeField] private int _maxLevel;
    [SerializeField] private int[] costs;

    private int _currentCost;
    private bool _isMaxLevel;

    public void OnPointerClick(PointerEventData eventData)
    {

        if (!_isMaxLevel && Progress.Instance.Money >= _currentCost)
        {
            Progress.Instance.IncreaseLevel(_type);
            
            Progress.Instance.DecreaseMoneyValue(_currentCost);

            int level = Progress.Instance.GetLevel(_type);

            CheckMaxLevel(level);
            ChangeValues(level);
            IsInteracteble();
        }
    }

    private bool IsInteracteble()
    {
        if (_isMaxLevel)
        {
            GetComponentInParent<Image>().color = Color.black;
            GetComponent<Button>().interactable = false;

            return false;
        }

        if (Progress.Instance.Money < _currentCost)
        {
            GetComponent<Button>().interactable = false;
            _cost.color = Color.red;
            return false;
        }

        GetComponent<Button>().interactable = true;
        _cost.color = Color.white;
        return true;
    }

    private void CheckMaxLevel(int level)
    {
        if (level + 1 >= _maxLevel)
        {
            _isMaxLevel = true;
        }
    }

    private void ChangeValues(int currentLevel)
    {
        _level.text = "Уровень " + (currentLevel+1).ToString();

        if (_isMaxLevel)
        {
            _cost.text = "Макс. уровень";
        }
        else
        {
            _currentCost = costs[currentLevel];
            _cost.text = "Цена " + _currentCost.ToString();
        }
    }

    private void Start()
    {
        int level = Progress.Instance.GetLevel(_type);

        if (level >= 0)
        {
            CheckMaxLevel(level);
            ChangeValues(level);
            IsInteracteble();
        }

        else
        {
            GetComponent<Button>().interactable = false;
            transform.parent.gameObject.SetActive(false);
        }

    }
}
