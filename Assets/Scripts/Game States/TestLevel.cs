using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : Level
{
    [SerializeField] TestInfo testInfo;

    public override void LaunchLevel()
    {
        GameManager.Instance.SetState(GameState.Testing);
        
       TestManager.Instance.StartNewTest(testInfo);
    }
}
