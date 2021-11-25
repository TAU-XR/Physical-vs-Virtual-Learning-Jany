using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Calibration, Learning, PreTesting, Testing, Finished };

public class GameManager : MonoBehaviour
{
    GameState currentGameState;
    public GameState State => currentGameState;
    public void SetState(GameState st) => currentGameState = st;

    [SerializeField] float waitBeforeStart = 0f;

    [SerializeField] int levelIndex;
    [SerializeField] Level[] levels;

    #region Singelton Decleration
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


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

    // Start is called before the first frame update
    void Start()
    {
        // calibrationState = GetComponent<GS_Calibration>();

        // launch calibration state.
        // calibrationState.LaunchState();
        StartGame();

    }


    public void NextLevel()
    {
        if (levelIndex + 1 >= levels.Length)
            EndGame();
        else
        {
            levelIndex++;
            levels[levelIndex].LaunchLevel();
        }
    }

    public void StartGame()
    {
        levels[levelIndex].LaunchLevel();
        // StartCoroutine(startGame());
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(waitBeforeStart);

    }

    public void TestEnded()
    {
        Debug.Log("Test ended.");
        if(currentGameState == GameState.Testing)
            NextLevel();
    }

    public void EndGame()
    {
        Debug.Log("Experiment is over. Thanks you for participating");
    }
}
