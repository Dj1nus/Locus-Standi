using UnityEngine;

public class MenuWindows : MonoBehaviour
{
    private bool _isActive = false;

    public bool IsActive => _isActive;

    //private void Start()
    //{
    //    print("Start");
    //    ChangeVisibility(false);
    //}

    public void ChangeVisibility(bool isActive)
    {
        gameObject.SetActive(isActive);
        _isActive = isActive;
    }
}
