using UnityEngine;

public class ChangeTileCollor : MonoBehaviour
{
    [SerializeField] public bool _isWall = false;
    [SerializeField] private Material _goodMaterial;
    [SerializeField] private Material _badMaterial;

    private MeshRenderer _meshRenderer;
    private Collider _coll;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _goodMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {

        _meshRenderer.material = _badMaterial;
        _coll = other;
        
    }

    private void OnTriggerExit(Collider other)
    {
        _meshRenderer.material = _goodMaterial;
    }

    private void Update()
    {
        if (_coll == null)
            _meshRenderer.material = _goodMaterial;
        
    }
}
