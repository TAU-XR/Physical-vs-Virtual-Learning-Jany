using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// disable toucher collider if hand is gone out of sight.
public class ToucherAppearence : MonoBehaviour
{
    [SerializeField] GameObject relatedHand;
    BoxCollider selfCollider;

    private void Start()
    {
        selfCollider = transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        selfCollider.enabled = relatedHand.activeSelf;
    }

    // called to know if hand is tracking or not;
    public bool isAppeared()
    {
        return selfCollider.enabled;
    }
}
