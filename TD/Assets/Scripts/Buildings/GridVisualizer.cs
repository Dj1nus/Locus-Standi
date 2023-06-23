using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    //[SerializeField] private BuildingGrid _buildingGrid;
    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;
    private float _height = -0.4f;
    private LineRenderer _lineRenderer;
    private int i = 0; //Простите за такую херню

    private void DrawBox(float x, float y)
    {
        _lineRenderer.SetPosition(i, new Vector3(x, _height, y));
        i++;
        _lineRenderer.SetPosition(i, new Vector3(x + 1, _height, y));
        i++;
        _lineRenderer.SetPosition(i, new Vector3(x + 1, _height, y + 1));
        i++;
        _lineRenderer.SetPosition(i, new Vector3(x, _height, y + 1));
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        
        _lineRenderer.positionCount = _sizeX * 2 * _sizeY * 2 * 3;
    }

    void Start()
    {
        for (int x = 0; x < _sizeX * 2; x++)
        {
            for (int y = 0; y < _sizeY * 2; y++)
            {
                DrawBox(x - 0.5f, y - 0.5f);
            }
        }

        _lineRenderer.Simplify(0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
