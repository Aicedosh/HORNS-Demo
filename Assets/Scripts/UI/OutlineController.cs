using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineController : MonoBehaviour
{
    private bool _selected;
    public bool Selected
    {
        set
        {
            _selected = value;
            UpdateOutline();
        }
    }

    public bool Hoverable = true;

    private bool _hovered;
    public bool Hovered
    {
        set
        {
            _hovered = value;
            UpdateOutline();
        }
    }

    private Outline _outline;

    private void UpdateOutline()
    {
        if (_selected)
        {
            _outline.enabled = true;
            Color color = _outline.OutlineColor;
            color.a = 1f;
            _outline.OutlineColor = color;
        }
        else if (_hovered)
        {
            _outline.enabled = true;
            Color color = _outline.OutlineColor;
            color.a = 0.3f;
            _outline.OutlineColor = color;
        }
        else
        {
            _outline.enabled = false;
        }
    }

    void OnMouseOver()
    {
        if (Hoverable && !_hovered)
        {
            Hovered = true;
        }
    }

    void OnMouseExit()
    {
        if(Hoverable)
        {
            Hovered = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _outline = GetComponentInParent<Outline>();
        _outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
