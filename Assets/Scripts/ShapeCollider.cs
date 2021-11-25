using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// highlights selected shape
// signals ShapeInputManager of that shape

public class ShapeCollider : MonoBehaviour
{
    [SerializeField] GameObject highlighter;
    [SerializeField] Shape colliderShape;
    AudioSource btnSound;


    private void Start()
    {
        highlighter.SetActive(false);
        btnSound = transform.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Toucher")
        {
            highlighter.SetActive(true);
            btnSound.Stop();
            btnSound.Play();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Toucher")
        {
            highlighter.SetActive(false);
            // ShapeInputManager.Instance.SetSelectedShape(Shape.none);
        }

    }

    public Shape GetColliderShape()
    {
        return colliderShape;
    }
}
