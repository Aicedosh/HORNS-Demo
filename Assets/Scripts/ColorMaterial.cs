using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[ExecuteInEditMode]
public class ColorMaterial : MonoBehaviour
{
    public Color Color;

    private Renderer _renderer;
    private MaterialPropertyBlock propBlock;

    void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if(propBlock == null)
        {
            return;
        }
        _renderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("_Color", Color);
        _renderer.SetPropertyBlock(propBlock);
    }
}
