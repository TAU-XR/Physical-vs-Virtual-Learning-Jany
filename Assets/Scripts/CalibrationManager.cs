using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// responsible to track hand position and in the right time- calibrate table position accordingly.
public class CalibrationManager : MonoBehaviour
{
    public Action TableCalibrated;
    [SerializeField] CalibrationLevel calibrationLevel;

    public bool skipCalib;

    bool isCalibrated;
    [SerializeField] Transform rHand;
    [SerializeField] Transform lHand;
    [SerializeField] Transform table;
    [SerializeField] Transform scene;
    public float stillnessThreshold = 0.0005f;              // how stable should hand be for time to count towards calibration confirm?
    float stillnessDuration = 2f;                           // for how long should hand be stable so that calibration will occur?

    [SerializeField] MeshRenderer stillnessMesh;            // the sphere around rHand that signs the progress of calibration timer.
    Color stillnessMeshColor;

    Vector3 handPosLastFrame;                               // saving last position of hand to measure stillness value.
    float stillnessCounter;                                 // how long is hand still?

    #region Singelton Decleration
    private static CalibrationManager _instance;

    public static CalibrationManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public void StartCalibration()
    {
        isCalibrated = false;

        // reset stillness counter and hand delta pos
        handPosLastFrame = rHand.position;
        stillnessCounter = 0;

        // set stillnesMesh alpha to zero.
        stillnessMesh.enabled = true;
        stillnessMeshColor = stillnessMesh.material.color;
        stillnessMeshColor.a = 0;
        stillnessMesh.material.color = stillnessMeshColor;

        // hide table
        table.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCalibrated)
        {
            if (bHandsTracking())
            {
                if (isRightHandStillEnough())
                {
                    table.gameObject.SetActive(true);

                    // set table pivot to r hand and its rotation to be the line between hands.
                    scene.position = rHand.position + new Vector3(0, 0.01f, 0);
                    scene.rotation = Quaternion.LookRotation(lHand.position - rHand.position, table.up);
                    Vector3 tempRot = table.eulerAngles;
                    tempRot.x = 0; tempRot.z = 0;
                    scene.eulerAngles = tempRot;
                    isCalibrated = true;

                    calibrationLevel.TableCalibrated();
                }
            }
        }

        if (skipCalib)
        {
            skipCalib = false;
            GameManager.Instance.StartGame();
        }
    }

    // return true if both hands are currently tracking via leap motion.
    bool bHandsTracking()
    {
        return (rHand.GetComponent<ToucherAppearence>().isAppeared() && lHand.GetComponent<ToucherAppearence>().isAppeared());
    }

    // counts the time hand movement delta is below stillnessThreshold and return true if it does.
    bool isRightHandStillEnough()
    {
        // if hand delta movement is still enough
        if (Vector3.Distance(rHand.position, handPosLastFrame) < stillnessThreshold)
        {
            // count time
            stillnessCounter += Time.deltaTime;

            // increase stillness mesh appearence
            stillnessMeshColor.a = Mathf.Lerp(0, 1, stillnessCounter / stillnessDuration);
            stillnessMesh.material.color = stillnessMeshColor;

            // if hand stillness time is enough
            if (stillnessCounter >= stillnessDuration)
            {
                // disappear stillnessMesh and return true.
                stillnessMesh.enabled = false;
                return true;
            }
            else
            {
                // update delta position and return false.
                handPosLastFrame = rHand.position;
                return false;
            }
        }
        else
        {
            // if hand delta movement is too fast - update delta position but do not count time.
            handPosLastFrame = rHand.position;
            stillnessCounter = 0;
            stillnessMeshColor.a = 0;
            stillnessMesh.material.color = stillnessMeshColor;
            return false;
        }
    }
}
