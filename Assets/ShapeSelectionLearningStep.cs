using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSelectionLearningStep : LearningStep
{
    [SerializeField] GameObject table;

    private void OnEnable()
    {
        table.SetActive(true);
    }
}
