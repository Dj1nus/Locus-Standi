using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    private const float GRID_OFFSET = 0.5f;
    private const float HEIGHT = -0.4f;

    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;
    
    private LineRenderer _lineRenderer;
    private int i = 0; //Простите за такую херню

    private void DrawBox(float x, float y)
    {
        _lineRenderer.SetPosition(i, new Vector3(x, HEIGHT, y));
        i++;
        _lineRenderer.SetPosition(i, new Vector3(x + 1, HEIGHT, y));
        i++;
        _lineRenderer.SetPosition(i, new Vector3(x + 1, HEIGHT, y + 1));
        i++;
        _lineRenderer.SetPosition(i, new Vector3(x, HEIGHT, y + 1));
    }

    private void ChangeTransparent(Level._states state)
    {
        if (state == Level._states.building)
            gameObject.SetActive(true);

        else
            gameObject.SetActive(false);
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _sizeX * _sizeY * 12;

        GlobalEventManager.OnVisualModeChanged.AddListener(ChangeTransparent);
    }

    void Start()
    {
        for (int x = 0; x < _sizeX * 2; x++)
        {
            for (int y = 0; y < _sizeY * 2; y++)
            {
                DrawBox(x - GRID_OFFSET, y - GRID_OFFSET);
            }
        }

        gameObject.SetActive(false);
    }
}
