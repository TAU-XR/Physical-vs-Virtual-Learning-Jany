using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBtnA_PlaySound : SBtnAction
{
    [SerializeField] AudioSource[] sounds;
    
    public override void LaunchAction()
    {
        foreach (AudioSource a in sounds)
        {
            a.Stop();
            a.Play();
        }
    }
}
