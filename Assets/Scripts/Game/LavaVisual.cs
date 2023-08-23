using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaVisual : MonoBehaviour
{
    [SerializeField] private Material _lavaMaterial;
    [SerializeField] private MeshRenderer _lavaPlane;

    private void Start()
    {
        _lavaPlane.SetMaterials(new List<Material>() { _lavaMaterial });
    }
}
