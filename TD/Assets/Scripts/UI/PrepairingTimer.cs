using System;
using TMPro;
using UnityEngine;

public class PrepairingTimer : MonoBehaviour
{
    public Action OnTimesLeft;

    private TMP_Text _text;
    private int _totalTime = 20;
    private float _currentTime;

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _totalTime + 0.5f;

        _text = GetComponent<TMP_Text>();
        _text.text = _currentTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;

        _text.text = Mathf.RoundToInt(_currentTime).ToString();

        if (Mathf.RoundToInt(_currentTime) <= 0)
        {
            OnTimesLeft?.Invoke();
        }
        else if (Mathf.RoundToInt(_currentTime) <= 3) 
        { 
            _text.color = Color.red;
        }
    }
}
