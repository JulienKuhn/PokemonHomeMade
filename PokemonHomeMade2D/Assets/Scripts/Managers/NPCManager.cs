using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    private List<int> hiddenNPCs = new List<int>();
    public Action<int, bool> OnNPCStatusChanged;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeNPCHiddenStatus(int npcID, bool show)
    {
        if (show && hiddenNPCs.Contains(npcID))
        {
            hiddenNPCs.Remove(npcID);
        }
        else if(!show && !hiddenNPCs.Contains(npcID))
        {
            hiddenNPCs.Add(npcID);
        }

        OnNPCStatusChanged?.Invoke(npcID,show);
    }

    public bool GetNPCVisibility(int npcID)
    {
        return !hiddenNPCs.Contains(npcID);
    }
}
