using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// learning level is a bunch of steps- game objects with childs and linked to a relevant text mesh in canvas 
public class LearningStep : MonoBehaviour
{
    [SerializeField] GameObject stepTxt;

    public void SetActive(bool state)
    {
        stepTxt.SetActive(state);
        gameObject.SetActive(state);
    }
}
