using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] Transform buttonMesh;
    [SerializeField] Transform buttonDownHolder;
    [SerializeField] float buttonPressDuration;
    [SerializeField] ButtonAction action;

    Vector3 buttonUpPos;
    Vector3 buttonDownPos;

    void Start()
    {
        // work with localPosition so that button can move around after Start.
        buttonUpPos = buttonMesh.localPosition;
        buttonDownPos = buttonDownHolder.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Toucher")
        {
            // start coroutine -> button mesh down
            StopAllCoroutines();
            StartCoroutine(buttonMeshDown());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Toucher")
        {
            // start coroutine -> button mesh up
            StopAllCoroutines();
            StartCoroutine(buttonMeshUp());
            // button action -> enabled
            action.RunAction();
        }
    }

    IEnumerator buttonMeshDown()
    {
        float lerpTime = 0;

        while (lerpTime < buttonPressDuration)
        {
            lerpTime += Time.deltaTime;

            float t = lerpTime / buttonPressDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            // move button mesh
            buttonMesh.localPosition = Vector3.Lerp(buttonUpPos, buttonDownPos, t);

            yield return null;
        }
    }

    IEnumerator buttonMeshUp()
    {
        float lerpTime = 0;

        while (lerpTime < buttonPressDuration)
        {
            lerpTime += Time.deltaTime;

            float t = lerpTime / buttonPressDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            // move button mesh
            buttonMesh.localPosition = Vector3.Lerp(buttonDownPos, buttonUpPos, t);

            yield return null;
        }
    }
}
