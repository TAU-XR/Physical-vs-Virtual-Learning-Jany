using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationLevel : Level
{
    bool isCalibrated = false;
    [SerializeField] GameObject calibrationButtons;                   // parent object of 2 buttons that allows to either re-calibrate or start game.

    // to check before buttons
    public bool bRecalibrate;
    public bool bContinue;

    // only to check
    private void Update()
    {
        if (bRecalibrate)
        {
            bRecalibrate = false;
            LaunchLevel();
        }
    }

    public override void LaunchLevel()
    {
        isCalibrated = false;
        calibrationButtons.SetActive(false);
        StartCoroutine(launchLevel());
    }

    IEnumerator launchLevel()
    {
        CalibrationManager.Instance.StartCalibration();

        // wait until table is calibrated.
        while (!isCalibrated)
            yield return null;

        // summon calibration buttons.
        calibrationButtons.SetActive(true);

        // calibration buttons will either call "Launch Level" again to re-calibrate or "Calibration Complete" to start game.
    }

    // called from calibrationManager upon table inital calibration.
    public void TableCalibrated()
    {
        isCalibrated = true;
    }

    public void CalibrationComplete()
    {
        calibrationButtons.SetActive(false);
        GameManager.Instance.NextLevel();
    }

    public void ReCalibrate()
    {
        LaunchLevel();
    }
}
