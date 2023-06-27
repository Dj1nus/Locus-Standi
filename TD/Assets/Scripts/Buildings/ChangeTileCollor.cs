using UnityEngine;

public class ChangeTileCollor : MonoBehaviour
{
    [SerializeField] private Material _goodMaterial;
    [SerializeField] private Material _badMaterial;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _goodMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        _meshRenderer.material = _badMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        _meshRenderer.material = _goodMaterial;
    }
}
