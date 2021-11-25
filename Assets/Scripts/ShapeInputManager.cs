using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Shape { rectange, circle, triangle, star, none }

// getting player shape selection into a shape array.
public class ShapeInputManager : MonoBehaviour
{
    #region Singelton Decleration
    private static ShapeInputManager _instance;

    public static ShapeInputManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    [SerializeField] ShapeDisplayer displayer;           // Scoreboard- where subject answers are displayed?
    int currentShapeIndex = 0;                           // on which position should shape be displayed?
    Shape[] submittedSequence;                           // the actual answer of the subject.
    bool inputEnabled = false;                           // if true- subject can choose shapes. if false- shape selection is not available.
    [SerializeField] SButton[] tableButtons;
    float sequenceLength;

    public event Action<bool> InputEnabled;
    public event Action PlayerSubmittedEnoughShapes;
    public event Action PlayerSubmittedSequence;

    public bool isInputEnabled() => inputEnabled;       // if true- shape buttons are exposed and interactible.
    public void SetInputEnabled(bool state)
    {
        inputEnabled = state;
        if (state)
            foreach (SButton b in tableButtons)
                b.SetState(SBtnState.Active);
        else
            foreach (SButton b in tableButtons)
                b.SetState(SBtnState.Inactive);

        InputEnabled?.Invoke(state);
        // make shapes appear or disappear.
    }

    
    // called from ShapeTestManager once it init
    public void Init(int seqLength)
    {
        sequenceLength = seqLength;
        submittedSequence = new Shape[seqLength];
        displayer.SetDisplayPositions(seqLength);
        displayer.ResetShapesArray(seqLength);
        currentShapeIndex = 0;
        displayer.SetShapesVisibility(true);
    }

    public void SubmitShape(Shape selectedShape)
    {
        if (currentShapeIndex < submittedSequence.Length)
        {
            displayer.SubmitShape(selectedShape, currentShapeIndex);
            submittedSequence[currentShapeIndex] = selectedShape;
            currentShapeIndex++;
        }
        else
            Debug.Log("Player tried to submit but exceeded total shapes number");

        if (currentShapeIndex == submittedSequence.Length)
            PlayerSubmittedEnoughShapes?.Invoke();

    }

    public void DeleteLastShape()
    {
        if (currentShapeIndex > 0)
        {
            currentShapeIndex--;
            displayer.DeleteShape(currentShapeIndex);
        }
        else
            Debug.Log("Subject tried to delete more than submitted shapes");
    }

    public void SubmitSequence()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.Learning:
                if (submittedSequence.Length != sequenceLength)
                {
                    Debug.Log("Not enough shapes");
                    // tell current level about that
                }
                else
                {
                    SetInputEnabled(false);
                    TrialManager.Instance.CheckSequence(submittedSequence);
                    PlayerSubmittedSequence?.Invoke();
                }
                break;

            default:
                SetInputEnabled(false);
                TrialManager.Instance.CheckSequence(submittedSequence);
                PlayerSubmittedSequence?.Invoke();
                break;
        }
    }

    public void EndTrial()
    {
        ShapeInputManager.Instance.SetInputEnabled(false);
        displayer.SetShapesVisibility(false);
    }
}
