using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesTransparentChanger : MonoBehaviour
{

    private void ChangeTilesTransparent(Level._states state)
    {
        if (state == Level._states.building)
            gameObject.SetActive(true);

        else
            gameObject.SetActive(false);
    }


    void Start()
    {
        GlobalEventManager.OnVisualModeChanged.AddListener(ChangeTilesTransparent);
    }
}
