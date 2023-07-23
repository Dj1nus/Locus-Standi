using UnityEngine;

public class HideUIAtEnd : MonoBehaviour
{
    [SerializeField] private GameObject _buildingMenu;
    [SerializeField] private GameObject _resources;
    [SerializeField] private GameObject _buildingMenuButton;

    private void OnGameEnd(bool isWin)
    {
        _buildingMenu.SetActive(false);
        _resources.SetActive(false);
        _buildingMenuButton.SetActive(false);
    }

    private void OnEnable()
    {
        GlobalEventManager.OnGameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnGameEnd -= OnGameEnd;
    }
}
