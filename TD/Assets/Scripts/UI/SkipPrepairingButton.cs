using System;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class SkipPrepairingButton : MonoBehaviour
{
    public Action OnButtonClick;
    
    public void ButtonClicked()
    {
        OnButtonClick?.Invoke();

        GetComponent<Button>().interactable = false;
    }

}
