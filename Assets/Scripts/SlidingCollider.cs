using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// upon collision:
// calculate the number of self position Z - collider position Z as touchVector;
// calculate delta value of current collider position with its position on the previous frame along the sliding axis - this is the deltaPos
// as long as deltaPos * touchVector > 0 -> it means hand is moving in the same direction-> hand is pushing -> move cube the same delta pos.
// if cube got to the end point OR toucher is no longer moving in the same position OR onTriggerExit -> stop movement.
public class SlidingCollider : MonoBehaviour
{
    [SerializeField] Transform cube;                        // Jany's cube.
    float toucherZLastFrame;                        // Z position of toucher in the last frame.
    float deltaVector;                              // current toucher Z minus its Z last frame.
    float toucherZUponTouch;                        // toucher Z when touched cube.
    float totalPushDistance;                        // how much toucher pushed cube.
    float touchVector = 0;                          // gets the number of transform.position.z - Collider.position.z upon TriggerEnter.
    Vector3 cubePositionUponTouch;                          // its obvious.

    int frameCounter = 0;
    [SerializeField] int frameLoop;                 // once in how many frames should we update toucher last frame

    int numberOfColliders = 0;
    Collider activeCollider;

    private void Update()
    {
        if (activeCollider != null)
        {
            // update delta calc
            deltaVector = activeCollider.transform.position.z - toucherZLastFrame;
            updateToucherLastFrame(activeCollider.transform.position.z);

            if (isActiveColliderPushing())
            {
                // move cube
                totalPushDistance = activeCollider.transform.position.z - toucherZUponTouch;
                cube.transform.position = cubePositionUponTouch + new Vector3(0, 0, totalPushDistance);
            }

            // detecting when leap motion is out of sight and makes collider dissappear.
            if (!activeCollider.GetComponent<BoxCollider>().enabled)
            {
                Debug.Log("Active Collider disappeared!");
                numberOfColliders--;
                activeCollider = null;
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Toucher")
        {
            numberOfColliders++;
            // if is used to prevent occasion when player uses his second hand
            if (numberOfColliders == 1)
                SetActiveCollider(other);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Toucher")
        {// means player started to push with X hand, got Y hand in, then took X hand off and started to push other direction without leaving collider.
            if (numberOfColliders == 1 && other.name != activeCollider.name)
            {
                Debug.Log("Changed hands without leaving collider. Switching Active Collider");
                SetActiveCollider(other);
            }
        }

    }

    // fix this to handle cases of more than one toucher (2 hands)
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Toucher")
        {
            numberOfColliders--;
            if (numberOfColliders == 0)
                activeCollider = null;
        }
    }



    private bool isActiveColliderPushing() => (touchVector * deltaVector > 0);

    private void updateToucherLastFrame(float currentToucherZ)
    {
        frameCounter++;
        if (frameCounter >= frameLoop)
        {
            toucherZLastFrame = currentToucherZ;
            frameCounter = 0;
        }
    }

    private void SetActiveCollider(Collider other)
    {
        activeCollider = other;

        cubePositionUponTouch = cube.transform.position;
        toucherZUponTouch = other.transform.position.z;

        // save direction of touch by subtracting self Z pos from Collider Z pos (Z as the sliding axis)
        touchVector = transform.position.z - other.transform.position.z;

        toucherZLastFrame = toucherZUponTouch;
        deltaVector = 0;
        totalPushDistance = 0;
    }
}
