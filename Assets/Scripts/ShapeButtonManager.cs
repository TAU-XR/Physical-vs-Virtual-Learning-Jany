using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeButtonManager : MonoBehaviour
{
    [SerializeField] MeshRenderer shapeRenderer;
    BoxCollider[] colliders;

    void Start()
    {
        ShapeInputManager.Instance.InputEnabled += setShapeEnable;
        colliders = GetComponentsInChildren<BoxCollider>();
        setShapeEnable(false);
    }

    void setShapeEnable(bool state)
    {
        shapeRenderer.enabled = state;
        foreach(BoxCollider col in colliders)
            col.enabled = state;
    }
}
