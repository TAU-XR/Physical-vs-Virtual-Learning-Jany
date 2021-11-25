using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBtnA_SubmitShape : SBtnAction
{
    [SerializeField] Shape selfShape;

    public override void LaunchAction()
    {
        ShapeInputManager.Instance.SubmitShape(selfShape);
    }
}
