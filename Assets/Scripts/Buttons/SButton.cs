using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SBtnState { Inactive, Active, Pressed };

// TODO: better count touchersInside. some events like getting in while inactive and getting out while active can mess up the count.
// make sure buttons' front is indeed like its front axis.
public class SButton : MonoBehaviour
{
    SBtnState currentState = SBtnState.Inactive;

    [SerializeField] float alignmentThresh = 0f;        // how aligned infront buttons' forward should toucher be to count pressing? (1- directly above. 0- will touch also from the sides.)
    SBtnCollider sBtnCollider;                          // the collider responsible to detect onEnter and onExit events.
    SBtnAction[] actions;                               // what will btn do after triggered?
    // public int touchersInside = 0;                   // how many touchers are pressing button at the moment


    public SBtnState BtnState => currentState;

    // public int TouchersInside => touchersInside;
    public SBtnCollider GetBtnCollider => sBtnCollider;

    void Start()
    {
        // Init();
    }


    public virtual void Init()
    {
        Debug.Log("Button Initiated");
        sBtnCollider = GetComponentInChildren<SBtnCollider>();
        sBtnCollider.Init(this);
        actions = GetComponents<SBtnAction>();
        // SetState(SBtnState.Active);
    }

    public void SetState(SBtnState state)
    {
        switch (state)
        {
            case SBtnState.Active:
                // enable collider
                // play appearence animation.
                if (currentState == SBtnState.Inactive)
                    Init();
                StopAllCoroutines();
                StartCoroutine(setActive());

                break;
            case SBtnState.Inactive:
                // disable collider
                // play inactive animation.
                if (currentState == SBtnState.Active)
                {
                    StopAllCoroutines();
                    StartCoroutine(setInactive());
                }

                break;
            case SBtnState.Pressed:
                // play pressed animation.

                break;
        }
        currentState = state;
    }


    public virtual void OnEnter(Collider col)
    {

    }

    public virtual void OnExit(Collider col)
    {

    }

    public virtual IEnumerator setActive()
    {
        yield return null;
    }

    public virtual IEnumerator setPressed()
    {
        yield return null;
    }

    public virtual IEnumerator setInactive()
    {
        yield return null;
    }

    public virtual void LaunchActions()
    {
        foreach (SBtnAction action in actions)
            action.LaunchAction();
    }

    // TODO: because its getting collider's location result might be wrong if toucher moved very fast and made it to the other side of the collider until the calculation took place. Can be solved with measuring toucher before touching
    // returns true if colliders position upon touch is above alignment threshold with button.
    public bool isToucherAligned(Vector3 touchPos)
    {
        Vector3 diffVec = touchPos - transform.position;
        return (Vector3.Dot(transform.forward, diffVec) >= alignmentThresh);
    }

}
