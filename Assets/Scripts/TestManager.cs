using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
1. Get TestInfo.
2. Starting new trials (manages Trial Manager).
3. Changing trial conditions according to current session and round.
*/
public class TestManager : MonoBehaviour
{
    #region Singelton Decleration
    private static TestManager _instance;

    public static TestManager Instance { get { return _instance; } }


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

    RoundInfo[] testRounds;

    // rounds    
    public int roundsInTest;
    public int currentRound = 0;
    // sessions
    public int sessionsInRound = 2;
    public int currentSession = 0;
    // trials
    public int trialsPerSession;
    public int currentTrial = 0;

    public int currentSequenceLength;

    // called from GameManager or TestLevel
    public void StartNewTest(TestInfo testInfo)
    {
        initTestParameters(testInfo);
        TrialManager.Instance.StartNewTrial(currentSequenceLength);
    }

    public void StartNextTrial()
    {
        if (currentTrial < trialsPerSession - 1)
        {
            currentTrial++;
            //start trial with same conditions
            TrialManager.Instance.StartNewTrial(currentSequenceLength);
        }
        else
            nextSession();
    }

    void initTestParameters(TestInfo testInfo)
    {
        testRounds = testInfo.Rounds;
        roundsInTest = testInfo.Rounds.Length;

        sessionsInRound = testInfo.Rounds[currentRound].SessionsPerRound;
        trialsPerSession = testInfo.Rounds[currentRound].TrialPerSession;
        currentSequenceLength = testInfo.Rounds[currentRound].SequenceLength;
    }

    void nextSession()
    {
        // will need to add a screen here to pause and wait for experimenter to change physical conditions.
        if (currentSession < sessionsInRound - 1)
        {
            // 1st session ended -> launch a new session with same conditions.
            currentSession++;
            currentTrial = 0;
            TrialManager.Instance.StartNewTrial(currentSequenceLength);
        }
        else
            nextRound();

    }

    void nextRound()
    {
        if (currentRound < roundsInTest - 1)
        {
            currentRound++;

            // change round conditions.
            currentSequenceLength = testRounds[currentRound].SequenceLength;
            trialsPerSession = testRounds[currentRound].TrialPerSession;
            currentTrial = 0;
            currentSession = 0;

            TrialManager.Instance.StartNewTrial(currentSequenceLength);
        }
        else
        {
            // test ended
            EndTest();
        }
    }

    public void EndTest()
    {
        TrialManager.Instance.EndTrial();
        GameManager.Instance.TestEnded();
    }
}
