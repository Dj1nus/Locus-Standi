using UnityEngine;
using UnityEngine.UI;

public class ResourcesTitels : MonoBehaviour
{
    [SerializeField] private Text _metalText;
    [SerializeField] private Text _organicText;
    [SerializeField] private PlayersResources _playersResources;

    private void ChangeResourcesValue()
    {
        _metalText.text = _playersResources.metal.ToString();
        _organicText.text = _playersResources.organic.ToString();
    }

    void Start()
    {
        GlobalEventManager.OnResourceValueChanged.AddListener(ChangeResourcesValue);
        _metalText.text = _playersResources.metal.ToString();
        _organicText.text = _playersResources.organic.ToString();
    }
}
