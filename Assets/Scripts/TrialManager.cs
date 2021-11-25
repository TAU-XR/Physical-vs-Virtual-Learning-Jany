using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TrialState
{
    SequenceDisplayed,           // riddle shapes are displayed. Waiting for player to touch ReadyButton.
    SubmittingShapes,         // player is inputing shapes.
    DisplayAnswer,   // player touched the green submit btn, now can choose whether to submit or go back.
}

/*
1. Generate random sequences.
2. Check input sequence.
3. Display Answer.
4. Loop through trial states.
*/

public class TrialManager : MonoBehaviour
{
    #region Singelton Decleration
    private static TrialManager _instance;

    public static TrialManager Instance { get { return _instance; } }


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


    TrialState currentState;

    Shape[] generatedSequence;                                                  // current generated shape sequence player has to reconstruct
    int sequenceLength;                                                         // how many shapes are in the generatedSequence

    public TrialState GetTestState() => currentState;
    public void SetTestState(TrialState state) => currentState = state;

    [SerializeField] GameObject readyForSubmittionBtn;                          // button for player to use after momorized the shape order.
    [SerializeField] GameObject nextTrialBtn;                                   // button for players to start next trial.
    [SerializeField] ShapeDisplayer displayer;                                  // the displayer for the generated sequence.

    [SerializeField] AudioSource failureSound;
    [SerializeField] AudioSource successSound;

    bool bTrialEnded = false;

    public Action<bool> OnSequenceCheck;            // invokes on sequence check. holds true or false corelating to player's success.
    public Action<int> OnSequenceDisplayed;            // called when new sequence is generated.
    public Action OnPlayerReadyToSubmit;            // called when player touches blue cube. after memorized shapes.

    // called from test manager.
    public void StartNewTrial(int seqLength)
    {
        bTrialEnded = false;
        generateAndDisplaySeq(seqLength);

        // update input manager
        ShapeInputManager.Instance.Init(seqLength);
        ShapeInputManager.Instance.SetInputEnabled(false);

        //show ready to start btn
        readyForSubmittionBtn.SetActive(true);
        nextTrialBtn.SetActive(false);

        OnSequenceDisplayed?.Invoke(seqLength);
        SetTestState(TrialState.SequenceDisplayed);
    }

    void generateAndDisplaySeq(int seqLength)
    {
        sequenceLength = seqLength;
        // generateNewSequence
        generatedSequence = new Shape[seqLength];

        for (int i = 0; i < seqLength; i++)
            generatedSequence[i] = getShapeFromNumber(UnityEngine.Random.Range((int)0, (int)4));

        // send to displayer
        displayer.DisplayShapes(generatedSequence);
    }

    // called from startSubmitting button. Triggered by Subject after they memorized.
    public void StartShapeSubmission()
    {
        // does the same except that during the learning phase it won't hide the answer.
        switch (GameManager.Instance.State)
        {
            case GameState.Learning:
                // tell input manage to begin
                ShapeInputManager.Instance.SetInputEnabled(true);
                readyForSubmittionBtn.SetActive(false);

                SetTestState(TrialState.SubmittingShapes);
                break;

            default:
                displayer.SetShapesVisibility(false);
                // tell input manage to begin
                ShapeInputManager.Instance.SetInputEnabled(true);
                readyForSubmittionBtn.SetActive(false);

                SetTestState(TrialState.SubmittingShapes);
                OnPlayerReadyToSubmit?.Invoke();
                break;
        }

    }

    // return true if seq equals generatedSequence
    public void CheckSequence(Shape[] submittedSeq)
    {
        SetTestState(TrialState.DisplayAnswer);
        // show test shapes
        displayer.SetShapesVisibility(true);
        // disable subject shape input
        ShapeInputManager.Instance.SetInputEnabled(false);
        // check submitted sequence
        for (int i = 0; i < generatedSequence.Length; i++)
            if (submittedSeq[i] != generatedSequence[i])
            {
                OnSequenceCheck?.Invoke(false);
                sequenceFailed();
                return;
            }

        // if code execute this line it means bot sequence match.
        sequenceSuccess();
        OnSequenceCheck?.Invoke(true);
    }

    public void EndTrial()
    {
        bTrialEnded = true;
        displayer.SetShapesVisibility(false);
        readyForSubmittionBtn.SetActive(false);
        nextTrialBtn.SetActive(false);
        ShapeInputManager.Instance.EndTrial();
    }

    void sequenceFailed()
    {
        if (!bTrialEnded)
        {
            Debug.Log("Sequence doesn't match - Failed :(");
            failureSound.Play();
            nextTrialBtn.SetActive(true);
        }
    }

    void sequenceSuccess()
    {
        if (!bTrialEnded)
        {
            Debug.Log("Sequence match! - Succeess!");
            successSound.Play();
            nextTrialBtn.SetActive(true);
        }
    }

    Shape getShapeFromNumber(int num)
    {
        switch (num)
        {
            case 0:
                return Shape.circle;
            case 1:
                return Shape.rectange;
            case 2:
                return Shape.triangle;
            case 3:
                return Shape.star;
        }

        Debug.Log("no shape to this number");
        return Shape.none;
    }
}
