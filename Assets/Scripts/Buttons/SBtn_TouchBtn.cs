using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// launch actions upon OnEnter.
public class SBtn_TouchBtn : SButton
{
    [SerializeField] MeshController touchMesh;
    [SerializeField] Color meshActiveColor;
    [SerializeField] Color meshDisabledColor = new Color(0.75f, 0.75f, 0.75f, 1);
    float activationDuration = 1f;

    public override void Init()
    {
        base.Init();
        touchMesh.SetColor(meshActiveColor);
    }

    // launch actions when first toucher enters
    public override void OnEnter(Collider col)
    {
        if (BtnState == SBtnState.Active)
        {
            if (col.tag == "Toucher")
                if (isToucherAligned(col.transform.position))
                {
                    SetState(SBtnState.Pressed);
                    LaunchActions();
                }
        }
    }

    public override void OnExit(Collider col)
    {
        if (BtnState == SBtnState.Pressed) // && NO TOUCHERS INSIDE!!!!
        {
            if (col.tag == "Toucher")
                SetState(SBtnState.Active);
        }
    }

    public override IEnumerator setActive()
    {
        float lerpTime = 0;
        Color oColor = touchMesh.GetColor();

        while (lerpTime < activationDuration)
        {
            lerpTime += Time.deltaTime;
            float t = lerpTime / activationDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            // change touchMesh color
            touchMesh.SetColor(Color.Lerp(oColor, meshActiveColor, t));

            yield return null;
        }
    }

    public override IEnumerator setInactive()
    {
        float lerpTime = 0;
        Color oColor = touchMesh.GetColor();

        while (lerpTime < activationDuration)
        {
            lerpTime += Time.deltaTime;
            float t = lerpTime / activationDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            // change touchMesh color
            touchMesh.SetColor(Color.Lerp(oColor, meshDisabledColor, t));

            yield return null;
        }
    }
}
