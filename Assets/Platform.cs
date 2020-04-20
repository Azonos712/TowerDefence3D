using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Color emissionColor;

    private Renderer r;
    private Color startColor;
    private void Start()
    {
        r = GetComponent<Renderer>();
        startColor = r.material.color;
    }
    private void OnMouseEnter()
    {
        r.material.color = emissionColor;
    }
    private void OnMouseExit()
    {
        r.material.color = startColor;
    }
}
