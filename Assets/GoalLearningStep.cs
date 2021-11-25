using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLearningStep : LearningStep
{
    [SerializeField] GameObject displayer;
    LearningLevel fatherlvl;

    private void OnEnable()
    {
        displayer.SetActive(true);
        fatherlvl = GetComponentInParent<LearningLevel>();
        fatherlvl.StartLearningTest();
    }
    private void Update() {
        if(TrialManager.Instance.GetTestState() == TrialState.SubmittingShapes)
            fatherlvl.NextStep();
    }
}
