using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSubmitCollider : MonoBehaviour
{
    [SerializeField] Shape selfShape;
    AudioSource btnSound;

    private void Start() {
        btnSound = transform.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Toucher")
        {
            if (isToucherAbove(other))
            {
                ShapeInputManager.Instance.SubmitShape(selfShape);
                btnSound.Stop();
                btnSound.Play();
            }
        }
    }

    bool isToucherAbove(Collider col)
    {
        return (col.transform.position.y > transform.position.y);
    }
}
