using UnityEngine;

public class PovVisualizer : MonoBehaviour
{
    [SerializeField] private int _detalisation;
    
    private LineRenderer _lineRenderer;
    private float _radius;

    private void DrawCircle()
    {
        _lineRenderer.positionCount = _detalisation;

        for (int currentStep = 0; currentStep < _detalisation; currentStep++)
        {
            float circumferenceProgress = (float)currentStep/_detalisation;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            
            float x = Mathf.Cos(currentRadian) * _radius;
            float y = Mathf.Sin(currentRadian) * _radius;

            Vector3 currentPosition = new Vector3(x, -0.4f, y);

            _lineRenderer.SetPosition(currentStep, currentPosition);
        }
    }

    private void ChangeTransparent(MapVisual.states state)
    {
        if (_lineRenderer != null)
        {
            if (state == MapVisual.states.building)
                _lineRenderer.enabled = true;

            else
                _lineRenderer.enabled = false;
        }
    }

    public void Init()
    {
        GlobalEventManager.OnVisualModeChanged += ChangeTransparent;

        _lineRenderer = GetComponent<LineRenderer>();
        _radius = GetComponent<SphereCollider>().radius;

        DrawCircle();

        //_lineRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnVisualModeChanged -= ChangeTransparent;
    }
}
