using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : Level
{
    [SerializeField] GameObject table;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject finishTxt;

    public override void LaunchLevel()
    {
        table.SetActive(false);
        canvas.SetActive(true);
        finishTxt.SetActive(true);
    }

}
