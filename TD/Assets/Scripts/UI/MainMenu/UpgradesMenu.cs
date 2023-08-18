using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesMenu : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ChangeVisibility()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
