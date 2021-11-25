using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="New Test Info", menuName ="Test Info")]
public class TestInfo : ScriptableObject
{
    public RoundInfo[] Rounds;
}
[Serializable]
public struct RoundInfo
{
    public int TrialPerSession;
    public int SessionsPerRound;
    public int SequenceLength;
}