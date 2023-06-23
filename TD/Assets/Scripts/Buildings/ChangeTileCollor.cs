using System.Collections;
using System.Collections.Generic;
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
        //print(1);
        _meshRenderer.material = _badMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        //print(2);
        _meshRenderer.material = _goodMaterial;
    }
}
