using UnityEngine;

public class TilesTransparentChanger : MonoBehaviour
{
    private void ChangeTilesTransparent(MapVisual.states state)
    {
        if (state == MapVisual.states.building)
            gameObject.SetActive(true);

        else
            gameObject.SetActive(false);
    }


    void Start()
    {
        GlobalEventManager.OnVisualModeChanged += ChangeTilesTransparent;

        gameObject.SetActive(true ? FindObjectOfType<MapVisual>().State == MapVisual.states.building : false);
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnVisualModeChanged -= ChangeTilesTransparent;
    }
}
