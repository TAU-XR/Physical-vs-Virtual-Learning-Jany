using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningLevel : Level
{
    public bool bNext;
    /*
        [SerializeField] LearningStep welcomeStep;
        [SerializeField] LearningStep handsStep;
        [SerializeField] LearningStep goalStep;
        [SerializeField] LearningStep shapeSelectionStep;
        [SerializeField] LearningStep deleteStep;
        [SerializeField] LearningStep submitAllThreeStep;
        [SerializeField] LearningStep submitSeqStep;*/

    [SerializeField] TestInfo learningTest;
    [SerializeField] LearningStep[] steps;
    [SerializeField] int stepIndex;
    [SerializeField] GameObject table;
    [SerializeField] CanvasManager canvas;
    [SerializeField] GameObject nextBtn;

    private void Update()
    {
        if (bNext)
        {
            bNext = false;
            NextStep();
        }
    }

    public override void LaunchLevel()
    {
        GameManager.Instance.SetState(GameState.Learning);

        TrialManager.Instance.OnSequenceCheck += skipTutorial;

        stepIndex = 0;
        table.SetActive(false);
        nextBtn.SetActive(true);
        canvas.gameObject.SetActive(true);
        disableAllSteps();
        steps[0].SetActive(true);
    }

    // if player submits it skips to pre-test level directly
    void skipTutorial(bool b)
    {
        TrialManager.Instance.OnSequenceCheck -= skipTutorial;
        foreach (LearningStep s in steps)
            s.SetActive(false);
        TestManager.Instance.EndTest();
        Debug.Log("Learning level is over.");
        GameManager.Instance.NextLevel();
    }

    // called from the nextBtn object in scene
    public void NextStep()
    {
        if (stepIndex + 1 < steps.Length)
        {
            steps[stepIndex].SetActive(false);
            stepIndex++;
            steps[stepIndex].SetActive(true);
        }
        else
        {
            steps[stepIndex].SetActive(false);
            TestManager.Instance.EndTest();
            Debug.Log("Learning level is over.");
            GameManager.Instance.NextLevel();
        }
    }

    public void SkipToStep(int skipToIndex)
    {
        for (int i = stepIndex; i < skipToIndex; i++)
        {
            steps[stepIndex].SetActive(false);
        }
        stepIndex = skipToIndex;
        steps[stepIndex].SetActive(true);
    }

    void disableAllSteps()
    {
        foreach (LearningStep s in steps)
            s.SetActive(false);
    }

    public void StartLearningTest()
    {
        TestManager.Instance.StartNewTest(learningTest);
    }
}
