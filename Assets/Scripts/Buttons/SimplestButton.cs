using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SimplestButton : MonoBehaviour
{
    public UnityEvent onEnterDo;
 
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Toucher")
            onEnterDo.Invoke();
    }
}
