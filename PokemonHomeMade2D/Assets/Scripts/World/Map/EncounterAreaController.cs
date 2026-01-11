using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EncounterAreaController : MonoBehaviour
{
    [SerializeField] private List<EncounterArea> areas;
    [SerializeField] private float flatEncounterProbabilityPercentage;

    private void Start()
    {
        foreach (EncounterArea area in areas) 
        {
            area.OnAreaEnter += this.OnAreaEnter;
        }
    }

    public void OnAreaEnter()
    {
        Debug.Log("IN");
        int rand = Random.Range(0, 100);
        if(rand <= flatEncounterProbabilityPercentage)
        {
            Debug.Log("Encounter !");
        }
    }
}
