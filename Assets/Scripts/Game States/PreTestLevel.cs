using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// wait for successThreshold consecutive successes and then passing on to the real test.
public class PreTestLevel : Level
{
    [SerializeField] TestInfo preTestInfo;

    [SerializeField] int successThreshold;
    public int currentSuccessRate;

    [SerializeField] GameObject startPreTestBtn;
    [SerializeField] GameObject startRealTestBtn;

    [SerializeField] GameObject introTxt;
    [SerializeField] GameObject readyTxt;
    [SerializeField] GameObject inTestTxt;
    [SerializeField] GameObject endTxt;

    [SerializeField] GameObject canvas;

    bool didEndPreTest = false;

    public override void LaunchLevel()
    {
        GameManager.Instance.SetState(GameState.PreTesting);

        startPreTestBtn.SetActive(true);

        TrialManager.Instance.OnPlayerReadyToSubmit += displayInTestTxt;
        TrialManager.Instance.OnSequenceCheck += countSubmition;

        introTxt.SetActive(true);
        inTestTxt.SetActive(false);
        readyTxt.SetActive(false);
        endTxt.SetActive(false);

    }

    public void StartPreTest()
    {
        startPreTestBtn.SetActive(false);
        TestManager.Instance.StartNewTest(preTestInfo);
        introTxt.SetActive(false);
        readyTxt.SetActive(true);
    }

    void displayInTestTxt()
    {
        TrialManager.Instance.OnPlayerReadyToSubmit -= displayInTestTxt;
        inTestTxt.SetActive(true);
        readyTxt.SetActive(false);
        endTxt.SetActive(false);
    }

    void endLevel()
    {
        TrialManager.Instance.OnSequenceCheck -= countSubmition;
        TestManager.Instance.EndTest();

        introTxt.SetActive(false);
        readyTxt.SetActive(false);
        inTestTxt.SetActive(false);
        endTxt.SetActive(true);
        startRealTestBtn.SetActive(true);
    }

    public void MoveToNextLevel()
    {
        endTxt.SetActive(false);

        startRealTestBtn.SetActive(false);
        GameManager.Instance.NextLevel();
    }

    void countSubmition(bool didSucceed)
    {
        if (didSucceed)
        {
            currentSuccessRate++;
            Debug.Log("Success rate is: " + currentSuccessRate);
            if (currentSuccessRate == successThreshold)
            {
                endLevel();
                return;
            }
        }
        else
            currentSuccessRate = 0;
    }
}
