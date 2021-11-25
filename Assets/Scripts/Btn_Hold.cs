using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Btn_Hold : MonoBehaviour
{
    public UnityEvent OnTriggeredDo;

    [SerializeField] MeshRenderer buttonMesh;
    Color oColor;
    [SerializeField] float holdingDuration;
    float holdingCounter = 0;

    void OnEnable()
    {
        oColor = buttonMesh.material.color;
        oColor.a = 0.5f;
        buttonMesh.material.color = oColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Toucher")
            holdingCounter = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Toucher")
        {
            holdingCounter += Time.deltaTime;
            oColor.a = Mathf.Lerp(0.5f, 1f, holdingCounter / holdingDuration);
            buttonMesh.material.color = oColor;

            if (holdingCounter > holdingDuration)
                OnTriggeredDo.Invoke();
        }
        
    }
}
