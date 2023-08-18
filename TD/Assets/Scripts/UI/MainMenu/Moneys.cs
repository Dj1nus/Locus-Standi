using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Moneys : MonoBehaviour
{
    private TMP_Text _text;

    private void ChangeMoneyValue(int value)
    {
        _text.text = value.ToString();
    }

    private void Start()
    {
        _text = GetComponent<TMP_Text>();

        _text.text = Progress.Instance.Money.ToString();

        Progress.OnMoneyValueChanged += ChangeMoneyValue;
    }

    private void OnDestroy()
    {
        Progress.OnMoneyValueChanged -= ChangeMoneyValue;
    }

}
