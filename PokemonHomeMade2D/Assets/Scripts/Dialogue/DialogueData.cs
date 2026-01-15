using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "DialogueData")]
public class DialogueData : ScriptableObject
{
    public bool isTalkingHead;
    public List<DialogueLine> Lines;
}

[Serializable]
public class DialogueLine
{
    public string SpeakerName;
    public string Line;
}
