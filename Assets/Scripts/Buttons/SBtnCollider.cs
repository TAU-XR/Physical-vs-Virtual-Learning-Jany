using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SBtnCollider : MonoBehaviour
{
    SButton fatherBtn;

    public void Init(SButton b)
    {
        fatherBtn = b;
    }

    private void OnTriggerEnter(Collider other)
    {
        fatherBtn.OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        fatherBtn.OnExit(other);
    }
}
