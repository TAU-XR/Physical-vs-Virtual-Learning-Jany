using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBtn_Hold : SButton
{
    bool isLaunched = false;                         // true if button was holded enough time.

    [SerializeField] MeshController touchMesh;
    [SerializeField] Color meshActiveColor;

    [SerializeField] float holdDuration = 1.5f;            // how long should Toucher be inside button to activate?
    float holdCounter;                              // for how long is toucher inside?

    public override void Init()
    {
        base.Init();
        touchMesh.SetColor(meshActiveColor);
    }

    private void Update()
    {
        // only check if button actions are not launched yet.
        if (!isLaunched)
        {
            // currently, isPressed doesn't count number of touchers inside collider and WILL NOT WORK IF TOUCHED WITH BOTH HANDS.
            if (BtnState == SBtnState.Pressed)
            {
                holdCounter += Time.deltaTime;
                touchMesh.SetAlpha(Mathf.Lerp(0.5f, 1f, holdCounter / holdDuration));

                if (holdCounter >= holdDuration)
                {
                    isLaunched = true;
                    LaunchActions();
                }
            }
            else
            {
                holdCounter = 0;
                touchMesh.SetAlpha(0.5f);
            }
        }
    }
}
