using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBtnA_ConfirmCalibration : SBtnAction
{
    public override void LaunchAction()
    {
        base.LaunchAction();
        Debug.Log("Calibration Confirmed!");
    }
}
