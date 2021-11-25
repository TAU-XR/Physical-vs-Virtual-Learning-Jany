using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBtnA_Recalibrate : SBtnAction
{
    public override void LaunchAction()
    {
        base.LaunchAction();
        Debug.Log("Calibration is not good enough. Recalibrating");
        // GameManager.Instance.LaunchState(GSType.Calibration);
    }
}
