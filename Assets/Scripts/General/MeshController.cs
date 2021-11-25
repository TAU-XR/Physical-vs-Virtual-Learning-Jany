using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr=transform.GetComponent<MeshRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnabled(bool state) => mr.enabled = state;

    public Color GetColor() => mr.material.color;
    public void SetColor(Color c) => mr.material.color = c;

    public void SetAlpha(float a)
    {
        Color currentColor = GetColor();
        currentColor.a = a;
        SetColor(currentColor);
    }
}
