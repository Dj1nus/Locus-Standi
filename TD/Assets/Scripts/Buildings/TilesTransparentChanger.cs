using UnityEngine;

public class TilesTransparentChanger : MonoBehaviour
{
    private void ChangeTilesTransparent(MapVisual._states state)
    {
        if (state == MapVisual._states.building)
            gameObject.SetActive(true);

        else
            gameObject.SetActive(false);
    }


    void Start()
    {
        GlobalEventManager.OnVisualModeChanged.AddListener(ChangeTilesTransparent);
    }
}
