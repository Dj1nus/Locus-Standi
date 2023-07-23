using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour 
{
    [SerializeField] private StatisticsCollector _statistics;

    [SerializeField] private string _winText = "Победа!";
    [SerializeField] private string _looseText = "Поражение!";

    [SerializeField] private Button _toMenuButton;
    [SerializeField] private TMP_Text _winOrLooseText;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _enemyKilled;
    [SerializeField] private TMP_Text _resourcesSpend;
    [SerializeField] private TMP_Text _buildingsConstructed;
    [SerializeField] private TMP_Text _buildingsDestroyed;

    private EndGamePanel _panel;

    private string _resourcesSpendText;

    public void Init(bool isWin)
    {
        _resourcesSpendText = "Потрачено ресурсов:\nМеталл - " +
            _statistics.MetalSpend.ToString() +
            "\nОрганика - " + _statistics.OrganicSpend.ToString();

        if (isWin)
        {
            _winOrLooseText.text = _winText;
        }

        else
        {
            _winOrLooseText.text = _looseText;
        }

        _score.text += _statistics.Score.ToString();
        _enemyKilled.text += _statistics.EnemiesKilled.ToString();
        _resourcesSpend.text = _resourcesSpendText;
        _buildingsConstructed.text += _statistics.BuildingsConstructed.ToString();
        _buildingsDestroyed.text += _statistics.BuildingsDestroyed.ToString();
    }

    void Start()
    {
        _panel = GetComponent<EndGamePanel>();

        _panel.OnScreenShow += Init;
    }

    private void OnDisable()
    {
        _panel.OnScreenShow -= Init;
    }
}
