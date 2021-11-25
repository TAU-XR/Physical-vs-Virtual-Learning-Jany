using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmittingShapesLearningStep : LearningStep
{
    LearningLevel fatherlvl;
    private void OnEnable()
    {
        fatherlvl = GetComponentInParent<LearningLevel>();
        ShapeInputManager.Instance.PlayerSubmittedEnoughShapes += playerSubmittedEnough;
    }

    void playerSubmittedEnough()
    {
        ShapeInputManager.Instance.PlayerSubmittedEnoughShapes -= playerSubmittedEnough;
        fatherlvl.NextStep();
    }

}
