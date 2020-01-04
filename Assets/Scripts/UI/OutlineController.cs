using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool _hovered;
    public bool Hovered
    {
        set
        {
            _hovered = value;
            UpdateOutline();
        }
    }

    public Outline Outline;

    private void UpdateOutline()
    {
        if (_selected)
        {
            Outline.enabled = true;
            Color color = Outline.OutlineColor;
            color.a = 1f;
            Outline.OutlineColor = color;
        }
        else if (_hovered)
        {
            Outline.enabled = true;
            Color color = Outline.OutlineColor;
            color.a = 0.3f;
            Outline.OutlineColor = color;
        }
        else
        {
            Outline.enabled = false;
        }
    }

    void OnMouseOver()
    {
        Hovered = true;
    }

    void OnMouseExit()
    {
        Hovered = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
