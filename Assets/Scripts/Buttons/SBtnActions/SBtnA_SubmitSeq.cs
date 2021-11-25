using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBtnA_SubmitSeq : SBtnAction
{
    [SerializeField] GameObject startAgainBtn;
    
    public override void LaunchAction()
    {
        ShapeInputManager.Instance.SubmitSequence();
    }

}
