using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_BackToAnswers : MonoBehaviour
{
    [SerializeField] GameObject readyToSubmitBtn;
    // AudioSource btnSound;

    private void Start()
    {
        // btnSound = transform.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Toucher")
        {
            if (isToucherAbove(other))
            {
                readyToSubmitBtn.SetActive(false);
                ShapeInputManager.Instance.SetInputEnabled(true);
                TrialManager.Instance.SetTestState(TrialState.SubmittingShapes);
                // gameObject.SetActive(false);
            }

        }
    }

    bool isToucherAbove(Collider col)
    {
        return (col.transform.position.y > transform.position.y);
    }
}
