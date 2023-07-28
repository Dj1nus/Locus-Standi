using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BuildingCostPanel : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private TMP_Text _metalText;
    [SerializeField] private TMP_Text _organicText;
    
    public void SetCostText(Cost cost)
    {
        _metalText.text = cost.metal.ToString();
        _organicText.text = cost.organic.ToString();
    }

    public void ShowPanel()
    {

        gameObject.SetActive(true);
    }

    public void HidePanel()
    {

        gameObject.SetActive(false);
    }

    public void ChangeMetalColor(bool isAble)
    {
        if (isAble)
            _metalText.color = Color.white;
        
        else
            _metalText.color = Color.red;

    }

    public void ChangeOrganicColor(bool isAble)
    {
        if (isAble)
            _organicText.color = Color.white;

        else
            _organicText.color = Color.red;
    }
    
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
