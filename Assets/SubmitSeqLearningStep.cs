using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitSeqLearningStep : LearningStep
{
    LearningLevel fatherlvl;

    private void OnEnable()
    {
        fatherlvl = GetComponentInParent<LearningLevel>();
        ShapeInputManager.Instance.PlayerSubmittedSequence += nextStep;
    }

    void nextStep()
    {
        ShapeInputManager.Instance.PlayerSubmittedSequence -= nextStep;
        fatherlvl.NextStep();
    }
}
