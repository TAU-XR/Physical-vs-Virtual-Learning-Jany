using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Submit : MonoBehaviour
{
    [SerializeField] GameObject startAgainBtn;
    [SerializeField] GameObject backBtn;
    AudioSource btnSound;

    private void Start()
    {
        btnSound = transform.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Toucher")
        {
            if (isToucherAbove(other))
            {
                // readyToSubmitBtn.SetActive(true);
                // ShapeTestManager.Instance.SetTestState(TestState.ReadyToSubmit);
                // backBtn.SetActive(true);
                btnSound.Stop();
                btnSound.Play();
                startAgainBtn.SetActive(true);
                ShapeInputManager.Instance.SubmitSequence();
            }

        }
    }

    bool isToucherAbove(Collider col)
    {
        return (col.transform.position.y > transform.position.y);
    }
}
