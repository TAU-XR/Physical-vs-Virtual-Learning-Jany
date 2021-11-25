using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBtnA_DeleteLastShape : SBtnAction
{
    public override void LaunchAction()
    {
        ShapeInputManager.Instance.DeleteLastShape();
    }
}
