using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[ExecuteInEditMode]
public class ColorMaterial : MonoBehaviour
{
    public Color Color;

    private Renderer renderer;
    private MaterialPropertyBlock propBlock;

    void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        renderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("_Color", Color);
        renderer.SetPropertyBlock(propBlock);
    }
}
