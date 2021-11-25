using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CanvasText{HandsExplenation, GameGoal, PressButtons, Delete, Submit, LearningEnding, GameEnding}
public class CanvasManager : MonoBehaviour
{
    Canvas canvas;
    [SerializeField] GameObject[] CanvasTexts;

    public void Init()
    {
        canvas = GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(CanvasText txt)
    {
        switch(txt)
        {
            case CanvasText.HandsExplenation:
            
            break;
        }
    }
}
